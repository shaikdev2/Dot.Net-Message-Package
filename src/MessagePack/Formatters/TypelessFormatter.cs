﻿#if !UNITY

using System;
using System.Buffers;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using MessagePack.Internal;

namespace MessagePack.Formatters
{
    /// <summary>
    /// For `object` field that holds derived from `object` value, ex: var arr = new object[] { 1, "a", new Model() };
    /// </summary>
    public sealed class TypelessFormatter : IMessagePackFormatter<object>
    {
        public const sbyte ExtensionTypeCode = 100;

        static readonly Regex SubtractFullNameRegex = new Regex(@", Version=\d+.\d+.\d+.\d+, Culture=\w+, PublicKeyToken=\w+", RegexOptions.Compiled);

        delegate int SerializeMethod(object dynamicContractlessFormatter, ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver);
        delegate object DeserializeMethod(object dynamicContractlessFormatter, ref MessagePackReader reader, IFormatterResolver resolver);

        readonly ThreadsafeTypeKeyHashTable<KeyValuePair<object, SerializeMethod>> serializers = new ThreadsafeTypeKeyHashTable<KeyValuePair<object, SerializeMethod>>();
        readonly ThreadsafeTypeKeyHashTable<KeyValuePair<object, DeserializeMethod>> deserializers = new ThreadsafeTypeKeyHashTable<KeyValuePair<object, DeserializeMethod>>();
        readonly ThreadsafeTypeKeyHashTable<byte[]> typeNameCache = new ThreadsafeTypeKeyHashTable<byte[]>();
        readonly AsymmetricKeyHashTable<byte[], ArraySegment<byte>, Type> typeCache = new AsymmetricKeyHashTable<byte[], ArraySegment<byte>, Type>(new StringArraySegmentByteAscymmetricEqualityComparer());

        static readonly HashSet<string> blacklistCheck = new HashSet<string>
        {
            "System.CodeDom.Compiler.TempFileCollection",
            "System.IO.FileSystemInfo",
            "System.Management.IWbemClassObjectFreeThreaded"
        };
        static readonly HashSet<Type> useBuiltinTypes = new HashSet<Type>
        {
            typeof(Boolean),
            // typeof(Char),
            typeof(SByte),
            typeof(Byte),
            typeof(Int16),
            typeof(UInt16),
            typeof(Int32),
            typeof(UInt32),
            typeof(Int64),
            typeof(UInt64),
            typeof(Single),
            typeof(Double),
            typeof(string),
            typeof(byte[]),

            // array should save there types.
            //typeof(Boolean[]),
            //typeof(Char[]),
            //typeof(SByte[]),
            //typeof(Int16[]),
            //typeof(UInt16[]),
            //typeof(Int32[]),
            //typeof(UInt32[]),
            //typeof(Int64[]),
            //typeof(UInt64[]),
            //typeof(Single[]),
            //typeof(Double[]),
            //typeof(string[]),

            typeof(Boolean?),
            // typeof(Char?),
            typeof(SByte?),
            typeof(Byte?),
            typeof(Int16?),
            typeof(UInt16?),
            typeof(Int32?),
            typeof(UInt32?),
            typeof(Int64?),
            typeof(UInt64?),
            typeof(Single?),
            typeof(Double?),
        };

        public Func<string, Type> BindToType { get; set; } = (string typeName) => Type.GetType(typeName, false);

        // mscorlib or System.Private.CoreLib
        static readonly bool isMscorlib = typeof(int).AssemblyQualifiedName.Contains("mscorlib");

        /// <summary>
        /// Gets or sets a value indicating whether to exclude assembly qualifiers from type names.
        /// </summary>
        /// <value>The default value is <c>true</c>.</value>
        public bool RemoveAssemblyVersion { get; set; } = true;

        public TypelessFormatter()
        {
            serializers.TryAdd(typeof(object), _ => new KeyValuePair<object, SerializeMethod>(null, (object p1, ref byte[] p2, int p3, object p4, IFormatterResolver p5) => 0));
            deserializers.TryAdd(typeof(object), _ => new KeyValuePair<object, DeserializeMethod>(null, (object p1, ref MessagePackReader p2, IFormatterResolver p3) => new object()));
        }

        // see:http://msdn.microsoft.com/en-us/library/w3f99sx1.aspx
        // subtract Version, Culture and PublicKeyToken from AssemblyQualifiedName 
        private string BuildTypeName(Type type)
        {
            if (RemoveAssemblyVersion)
            {
                string full = type.AssemblyQualifiedName;

                var shortened = SubtractFullNameRegex.Replace(full, "");
                if (Type.GetType(shortened, false) == null)
                {
                    // if type cannot be found with shortened name - use full name
                    shortened = full;
                }

                return shortened;
            }
            else
            {
                return type.AssemblyQualifiedName;
            }
        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return MessagePackBinary.WriteNil(ref bytes, offset);
            }

            var type = value.GetType();

            byte[] typeName;
            if (!typeNameCache.TryGetValue(type, out typeName))
            {
                if (blacklistCheck.Contains(type.FullName))
                {
                    throw new InvalidOperationException("Type is in blacklist:" + type.FullName);
                }

                var ti = type.GetTypeInfo();
                if (ti.IsAnonymous() || useBuiltinTypes.Contains(type))
                {
                    typeName = null;
                }
                else
                {
                    typeName = StringEncoding.UTF8.GetBytes(BuildTypeName(type));
                }
                typeNameCache.TryAdd(type, typeName);
            }

            if (typeName == null)
            {
                return Resolvers.TypelessFormatterFallbackResolver.Instance.GetFormatter<object>().Serialize(ref bytes, offset, value, formatterResolver);
            }

            // don't use GetOrAdd for avoid closure capture.
            KeyValuePair<object, SerializeMethod> formatterAndDelegate;
            if (!serializers.TryGetValue(type, out formatterAndDelegate))
            {
                lock (serializers) // double check locking...
                {
                    if (!serializers.TryGetValue(type, out formatterAndDelegate))
                    {
                        var ti = type.GetTypeInfo();

                        var formatter = formatterResolver.GetFormatterDynamic(type);
                        if (formatter == null)
                        {
                            throw new FormatterNotRegisteredException(type.FullName + " is not registered in this resolver. resolver:" + formatterResolver.GetType().Name);
                        }

                        var formatterType = typeof(IMessagePackFormatter<>).MakeGenericType(type);
                        var param0 = Expression.Parameter(typeof(object), "formatter");
                        var param1 = Expression.Parameter(typeof(byte[]).MakeByRefType(), "bytes");
                        var param2 = Expression.Parameter(typeof(int), "offset");
                        var param3 = Expression.Parameter(typeof(object), "value");
                        var param4 = Expression.Parameter(typeof(IFormatterResolver), "formatterResolver");

                        var serializeMethodInfo = formatterType.GetRuntimeMethod("Serialize", new[] { typeof(byte[]).MakeByRefType(), typeof(int), type, typeof(IFormatterResolver) });

                        var body = Expression.Call(
                            Expression.Convert(param0, formatterType),
                            serializeMethodInfo,
                            param1,
                            param2,
                            ti.IsValueType ? Expression.Unbox(param3, type) : Expression.Convert(param3, type),
                            param4);

                        var lambda = Expression.Lambda<SerializeMethod>(body, param0, param1, param2, param3, param4).Compile();

                        formatterAndDelegate = new KeyValuePair<object, SerializeMethod>(formatter, lambda);
                        serializers.TryAdd(type, formatterAndDelegate);
                    }
                }
            }

            // mark as extension with code 100
            var startOffset = offset;
            offset += 6; // mark will be written at the end, when size is known
            offset += MessagePackBinary.WriteStringBytes(ref bytes, offset, typeName);
            offset += formatterAndDelegate.Value(formatterAndDelegate.Key, ref bytes, offset, value, formatterResolver);
            MessagePackBinary.WriteExtensionFormatHeaderForceExt32Block(ref bytes, startOffset, (sbyte)TypelessFormatter.ExtensionTypeCode, offset - startOffset - 6);
            return offset - startOffset;
        }

        public object Deserialize(ref MessagePackReader reader, IFormatterResolver resolver)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            if (reader.NextMessagePackType == MessagePackType.Extension)
            {
                var ext = reader.ReadExtensionFormatHeader();
                if (ext.TypeCode == TypelessFormatter.ExtensionTypeCode)
                {
                    // it has type name serialized
                    var typeName = reader.ReadStringSegment();
                    ArraySegment<byte> typeNameArraySegment;
                    byte[] rented = null;
                    if (!typeName.IsSingleSegment || !MemoryMarshal.TryGetArray(typeName.First, out typeNameArraySegment))
                    {
                        rented = ArrayPool<byte>.Shared.Rent((int)typeName.Length);
                        typeName.CopyTo(rented);
                        typeNameArraySegment = new ArraySegment<byte>(rented, 0, (int)typeName.Length);
                    }

                    var result = DeserializeByTypeName(typeNameArraySegment, ref reader, resolver);

                    if (rented != null)
                    {
                        ArrayPool<byte>.Shared.Return(rented);
                    }

                    return result;
                }
            }

            // fallback
            return Resolvers.TypelessFormatterFallbackResolver.Instance.GetFormatter<object>().Deserialize(ref reader, resolver);
        }

        /// <summary>
        /// Does not support deserializing of anonymous types
        /// Type should be covered by preceeding resolvers in complex/standard resolver
        /// </summary>
        private object DeserializeByTypeName(ArraySegment<byte> typeName, ref MessagePackReader byteSequence, IFormatterResolver resolver)
        {
            // try get type with assembly name, throw if not found
            Type type;
            if (!typeCache.TryGetValue(typeName, out type))
            {
                var buffer = new byte[typeName.Count];
                Buffer.BlockCopy(typeName.Array, typeName.Offset, buffer, 0, buffer.Length);
                var str = StringEncoding.UTF8.GetString(buffer);
                type = BindToType(str);
                if (type == null)
                {
                    if (isMscorlib && str.Contains("System.Private.CoreLib"))
                    {
                        str = str.Replace("System.Private.CoreLib", "mscorlib");
                        type = Type.GetType(str, true); // throw
                    }
                    else if (!isMscorlib && str.Contains("mscorlib"))
                    {
                        str = str.Replace("mscorlib", "System.Private.CoreLib");
                        type = Type.GetType(str, true); // throw
                    }
                    else
                    {
                        type = Type.GetType(str, true); // re-throw
                    }
                }
                typeCache.TryAdd(buffer, type);
            }

            KeyValuePair<object, DeserializeMethod> formatterAndDelegate;
            if (!deserializers.TryGetValue(type, out formatterAndDelegate))
            {
                lock (deserializers)
                {
                    if (!deserializers.TryGetValue(type, out formatterAndDelegate))
                    {
                        var ti = type.GetTypeInfo();

                        var formatter = resolver.GetFormatterDynamic(type);
                        if (formatter == null)
                        {
                            throw new FormatterNotRegisteredException(type.FullName + " is not registered in this resolver. resolver:" + resolver.GetType().Name);
                        }

                        var formatterType = typeof(IMessagePackFormatter<>).MakeGenericType(type);
                        var param0 = Expression.Parameter(typeof(object), "formatter");
                        var param1 = Expression.Parameter(typeof(MessagePackReader).MakeByRefType(), "reader");
                        var param2 = Expression.Parameter(typeof(IFormatterResolver), "resolver");

                        var deserializeMethodInfo = formatterType.GetRuntimeMethod("Deserialize", new[] { typeof(MessagePackReader).MakeByRefType(), typeof(IFormatterResolver) });

                        var deserialize = Expression.Call(
                            Expression.Convert(param0, formatterType),
                            deserializeMethodInfo,
                            param1,
                            param2);

                        Expression body = deserialize;
                        if (ti.IsValueType)
                        {
                            body = Expression.Convert(deserialize, typeof(object));
                        }

                        var lambda = Expression.Lambda<DeserializeMethod>(body, param0, param1, param2).Compile();

                        formatterAndDelegate = new KeyValuePair<object, DeserializeMethod>(formatter, lambda);
                        deserializers.TryAdd(type, formatterAndDelegate);
                    }
                }
            }

            return formatterAndDelegate.Value(formatterAndDelegate.Key, ref byteSequence, resolver);
        }
    }

}

#endif