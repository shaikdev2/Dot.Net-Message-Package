// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

#pragma warning disable SA1200 // Using directives should be placed correctly
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Resolvers
{
    using System;

    public class GeneratedResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new GeneratedResolver();

        private GeneratedResolver()
        {
        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.Formatter;
        }

        private static class FormatterCache<T>
        {
            internal static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> Formatter;

            static FormatterCache()
            {
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    Formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        private static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(13)
            {
                { typeof(global::System.Collections.Generic.List<global::TestData2.A>), 0 },
                { typeof(global::System.Collections.Generic.List<global::TestData2.B>), 1 },
                { typeof(global::TestData2.Nest1.Id), 2 },
                { typeof(global::TestData2.Nest2.Id), 3 },
                { typeof(global::TestData2.A), 4 },
                { typeof(global::TestData2.B), 5 },
                { typeof(global::TestData2.C), 6 },
                { typeof(global::TestData2.Nest1), 7 },
                { typeof(global::TestData2.Nest1.IdType), 8 },
                { typeof(global::TestData2.Nest2), 9 },
                { typeof(global::TestData2.Nest2.IdType), 10 },
                { typeof(global::TestData2.PropNameCheck1), 11 },
                { typeof(global::TestData2.PropNameCheck2), 12 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key))
            {
                return null;
            }

            switch (key)
            {
                case 0: return new global::MessagePack.Formatters.ListFormatter<global::TestData2.A>();
                case 1: return new global::MessagePack.Formatters.ListFormatter<global::TestData2.B>();
                case 2: return new MessagePack.Formatters.TestData2.Nest1_IdFormatter();
                case 3: return new MessagePack.Formatters.TestData2.Nest2_IdFormatter();
                case 4: return new MessagePack.Formatters.TestData2.AFormatter();
                case 5: return new MessagePack.Formatters.TestData2.BFormatter();
                case 6: return new MessagePack.Formatters.TestData2.CFormatter();
                case 7: return new MessagePack.Formatters.TestData2.Nest1Formatter();
                case 8: return new MessagePack.Formatters.TestData2.Nest1_IdTypeFormatter();
                case 9: return new MessagePack.Formatters.TestData2.Nest2Formatter();
                case 10: return new MessagePack.Formatters.TestData2.Nest2_IdTypeFormatter();
                case 11: return new MessagePack.Formatters.TestData2.PropNameCheck1Formatter();
                case 12: return new MessagePack.Formatters.TestData2.PropNameCheck2Formatter();
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1200 // Using directives should be placed correctly
#pragma warning restore SA1649 // File name should match first type name


// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

#pragma warning disable SA1200 // Using directives should be placed correctly
#pragma warning disable SA1403 // File may only contain a single namespace
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Formatters.TestData2
{
    using System;
    using System.Buffers;
    using MessagePack;

    public sealed class Nest1_IdFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::TestData2.Nest1.Id>
    {
        public void Serialize(ref MessagePackWriter writer, global::TestData2.Nest1.Id value, global::MessagePack.MessagePackSerializerOptions options)
        {
            writer.Write((Int32)value);
        }

        public global::TestData2.Nest1.Id Deserialize(ref MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            return (global::TestData2.Nest1.Id)reader.ReadInt32();
        }
    }

    public sealed class Nest2_IdFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::TestData2.Nest2.Id>
    {
        public void Serialize(ref MessagePackWriter writer, global::TestData2.Nest2.Id value, global::MessagePack.MessagePackSerializerOptions options)
        {
            writer.Write((Int32)value);
        }

        public global::TestData2.Nest2.Id Deserialize(ref MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            return (global::TestData2.Nest2.Id)reader.ReadInt32();
        }
    }
}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1200 // Using directives should be placed correctly
#pragma warning restore SA1403 // File may only contain a single namespace
#pragma warning restore SA1649 // File name should match first type name



// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

#pragma warning disable SA1129 // Do not use default value type constructor
#pragma warning disable SA1200 // Using directives should be placed correctly
#pragma warning disable SA1309 // Field names should not begin with underscore
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1403 // File may only contain a single namespace
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Formatters.TestData2
{
    using System;
    using System.Buffers;
    using System.Runtime.InteropServices;
    using MessagePack;

    public sealed class AFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::TestData2.A>
    {
        // a
        private static global::System.ReadOnlySpan<byte> GetSpan_a() => new byte[1 + 1] { 161, 97 };
        // bs
        private static global::System.ReadOnlySpan<byte> GetSpan_bs() => new byte[1 + 2] { 162, 98, 115 };
        // c
        private static global::System.ReadOnlySpan<byte> GetSpan_c() => new byte[1 + 1] { 161, 99 };

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::TestData2.A value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNil();
                return;
            }

            IFormatterResolver formatterResolver = options.Resolver;
            writer.WriteMapHeader(3);
            writer.WriteRaw(GetSpan_a());
            writer.Write(value.a);
            writer.WriteRaw(GetSpan_bs());
            formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::TestData2.B>>().Serialize(ref writer, value.bs, options);
            writer.WriteRaw(GetSpan_c());
            formatterResolver.GetFormatterWithVerify<global::TestData2.C>().Serialize(ref writer, value.c, options);
        }

        public global::TestData2.A Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            IFormatterResolver formatterResolver = options.Resolver;
            var length = reader.ReadMapHeader();
            var __a__ = default(int);
            var __bs__ = default(global::System.Collections.Generic.List<global::TestData2.B>);
            var __c__ = default(global::TestData2.C);

            for (int i = 0; i < length; i++)
            {
                ReadOnlySpan<byte> stringKey = global::MessagePack.Internal.CodeGenHelpers.ReadStringSpan(ref reader);
                switch (stringKey.Length)
                {
                    default:
                    FAIL:
                      reader.Skip();
                      continue;
                    case 1:
                        switch (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey))
                        {
                            default: goto FAIL;
                            case 97UL:
                                __a__ = reader.ReadInt32();
                                continue;
                            case 99UL:
                                __c__ = formatterResolver.GetFormatterWithVerify<global::TestData2.C>().Deserialize(ref reader, options);
                                continue;
                        }
                    case 2:
                        if (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey) != 29538UL) { goto FAIL; }

                        __bs__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::TestData2.B>>().Deserialize(ref reader, options);
                        continue;

                }
            }

            var ____result = new global::TestData2.A()
            {
                a = __a__,
                bs = __bs__,
                c = __c__,
            };

            reader.Depth--;
            return ____result;
        }
    }

    public sealed class BFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::TestData2.B>
    {
        // ass
        private static global::System.ReadOnlySpan<byte> GetSpan_ass() => new byte[1 + 3] { 163, 97, 115, 115 };
        // c
        private static global::System.ReadOnlySpan<byte> GetSpan_c() => new byte[1 + 1] { 161, 99 };
        // a
        private static global::System.ReadOnlySpan<byte> GetSpan_a() => new byte[1 + 1] { 161, 97 };

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::TestData2.B value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNil();
                return;
            }

            IFormatterResolver formatterResolver = options.Resolver;
            writer.WriteMapHeader(3);
            writer.WriteRaw(GetSpan_ass());
            formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::TestData2.A>>().Serialize(ref writer, value.ass, options);
            writer.WriteRaw(GetSpan_c());
            formatterResolver.GetFormatterWithVerify<global::TestData2.C>().Serialize(ref writer, value.c, options);
            writer.WriteRaw(GetSpan_a());
            writer.Write(value.a);
        }

        public global::TestData2.B Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            IFormatterResolver formatterResolver = options.Resolver;
            var length = reader.ReadMapHeader();
            var __ass__ = default(global::System.Collections.Generic.List<global::TestData2.A>);
            var __c__ = default(global::TestData2.C);
            var __a__ = default(int);

            for (int i = 0; i < length; i++)
            {
                ReadOnlySpan<byte> stringKey = global::MessagePack.Internal.CodeGenHelpers.ReadStringSpan(ref reader);
                switch (stringKey.Length)
                {
                    default:
                    FAIL:
                      reader.Skip();
                      continue;
                    case 3:
                        if (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey) != 7566177UL) { goto FAIL; }

                        __ass__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::TestData2.A>>().Deserialize(ref reader, options);
                        continue;
                    case 1:
                        switch (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey))
                        {
                            default: goto FAIL;
                            case 99UL:
                                __c__ = formatterResolver.GetFormatterWithVerify<global::TestData2.C>().Deserialize(ref reader, options);
                                continue;
                            case 97UL:
                                __a__ = reader.ReadInt32();
                                continue;
                        }

                }
            }

            var ____result = new global::TestData2.B()
            {
                ass = __ass__,
                c = __c__,
                a = __a__,
            };

            reader.Depth--;
            return ____result;
        }
    }

    public sealed class CFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::TestData2.C>
    {
        // b
        private static global::System.ReadOnlySpan<byte> GetSpan_b() => new byte[1 + 1] { 161, 98 };
        // a
        private static global::System.ReadOnlySpan<byte> GetSpan_a() => new byte[1 + 1] { 161, 97 };

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::TestData2.C value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNil();
                return;
            }

            IFormatterResolver formatterResolver = options.Resolver;
            writer.WriteMapHeader(2);
            writer.WriteRaw(GetSpan_b());
            formatterResolver.GetFormatterWithVerify<global::TestData2.B>().Serialize(ref writer, value.b, options);
            writer.WriteRaw(GetSpan_a());
            writer.Write(value.a);
        }

        public global::TestData2.C Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            IFormatterResolver formatterResolver = options.Resolver;
            var length = reader.ReadMapHeader();
            var __b__ = default(global::TestData2.B);
            var __a__ = default(int);

            for (int i = 0; i < length; i++)
            {
                ReadOnlySpan<byte> stringKey = global::MessagePack.Internal.CodeGenHelpers.ReadStringSpan(ref reader);
                switch (stringKey.Length)
                {
                    default:
                    FAIL:
                      reader.Skip();
                      continue;
                    case 1:
                        switch (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey))
                        {
                            default: goto FAIL;
                            case 98UL:
                                __b__ = formatterResolver.GetFormatterWithVerify<global::TestData2.B>().Deserialize(ref reader, options);
                                continue;
                            case 97UL:
                                __a__ = reader.ReadInt32();
                                continue;
                        }

                }
            }

            var ____result = new global::TestData2.C()
            {
                b = __b__,
                a = __a__,
            };

            reader.Depth--;
            return ____result;
        }
    }

    public sealed class Nest1Formatter : global::MessagePack.Formatters.IMessagePackFormatter<global::TestData2.Nest1>
    {
        // EnumId
        private static global::System.ReadOnlySpan<byte> GetSpan_EnumId() => new byte[1 + 6] { 166, 69, 110, 117, 109, 73, 100 };
        // ClassId
        private static global::System.ReadOnlySpan<byte> GetSpan_ClassId() => new byte[1 + 7] { 167, 67, 108, 97, 115, 115, 73, 100 };

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::TestData2.Nest1 value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNil();
                return;
            }

            IFormatterResolver formatterResolver = options.Resolver;
            writer.WriteMapHeader(2);
            writer.WriteRaw(GetSpan_EnumId());
            formatterResolver.GetFormatterWithVerify<global::TestData2.Nest1.Id>().Serialize(ref writer, value.EnumId, options);
            writer.WriteRaw(GetSpan_ClassId());
            formatterResolver.GetFormatterWithVerify<global::TestData2.Nest1.IdType>().Serialize(ref writer, value.ClassId, options);
        }

        public global::TestData2.Nest1 Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            IFormatterResolver formatterResolver = options.Resolver;
            var length = reader.ReadMapHeader();
            var __EnumId__ = default(global::TestData2.Nest1.Id);
            var __ClassId__ = default(global::TestData2.Nest1.IdType);

            for (int i = 0; i < length; i++)
            {
                ReadOnlySpan<byte> stringKey = global::MessagePack.Internal.CodeGenHelpers.ReadStringSpan(ref reader);
                switch (stringKey.Length)
                {
                    default:
                    FAIL:
                      reader.Skip();
                      continue;
                    case 6:
                        if (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey) != 110266531802693UL) { goto FAIL; }

                        __EnumId__ = formatterResolver.GetFormatterWithVerify<global::TestData2.Nest1.Id>().Deserialize(ref reader, options);
                        continue;
                    case 7:
                        if (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey) != 28228257876896835UL) { goto FAIL; }

                        __ClassId__ = formatterResolver.GetFormatterWithVerify<global::TestData2.Nest1.IdType>().Deserialize(ref reader, options);
                        continue;

                }
            }

            var ____result = new global::TestData2.Nest1()
            {
                EnumId = __EnumId__,
                ClassId = __ClassId__,
            };

            reader.Depth--;
            return ____result;
        }
    }

    public sealed class Nest1_IdTypeFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::TestData2.Nest1.IdType>
    {

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::TestData2.Nest1.IdType value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNil();
                return;
            }

            writer.WriteMapHeader(0);
        }

        public global::TestData2.Nest1.IdType Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            reader.Skip();
            var ____result = new global::TestData2.Nest1.IdType();
            return ____result;
        }
    }

    public sealed class Nest2Formatter : global::MessagePack.Formatters.IMessagePackFormatter<global::TestData2.Nest2>
    {
        // EnumId
        private static global::System.ReadOnlySpan<byte> GetSpan_EnumId() => new byte[1 + 6] { 166, 69, 110, 117, 109, 73, 100 };
        // ClassId
        private static global::System.ReadOnlySpan<byte> GetSpan_ClassId() => new byte[1 + 7] { 167, 67, 108, 97, 115, 115, 73, 100 };

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::TestData2.Nest2 value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNil();
                return;
            }

            IFormatterResolver formatterResolver = options.Resolver;
            writer.WriteMapHeader(2);
            writer.WriteRaw(GetSpan_EnumId());
            formatterResolver.GetFormatterWithVerify<global::TestData2.Nest2.Id>().Serialize(ref writer, value.EnumId, options);
            writer.WriteRaw(GetSpan_ClassId());
            formatterResolver.GetFormatterWithVerify<global::TestData2.Nest2.IdType>().Serialize(ref writer, value.ClassId, options);
        }

        public global::TestData2.Nest2 Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            IFormatterResolver formatterResolver = options.Resolver;
            var length = reader.ReadMapHeader();
            var __EnumId__ = default(global::TestData2.Nest2.Id);
            var __ClassId__ = default(global::TestData2.Nest2.IdType);

            for (int i = 0; i < length; i++)
            {
                ReadOnlySpan<byte> stringKey = global::MessagePack.Internal.CodeGenHelpers.ReadStringSpan(ref reader);
                switch (stringKey.Length)
                {
                    default:
                    FAIL:
                      reader.Skip();
                      continue;
                    case 6:
                        if (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey) != 110266531802693UL) { goto FAIL; }

                        __EnumId__ = formatterResolver.GetFormatterWithVerify<global::TestData2.Nest2.Id>().Deserialize(ref reader, options);
                        continue;
                    case 7:
                        if (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey) != 28228257876896835UL) { goto FAIL; }

                        __ClassId__ = formatterResolver.GetFormatterWithVerify<global::TestData2.Nest2.IdType>().Deserialize(ref reader, options);
                        continue;

                }
            }

            var ____result = new global::TestData2.Nest2()
            {
                EnumId = __EnumId__,
                ClassId = __ClassId__,
            };

            reader.Depth--;
            return ____result;
        }
    }

    public sealed class Nest2_IdTypeFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::TestData2.Nest2.IdType>
    {

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::TestData2.Nest2.IdType value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNil();
                return;
            }

            writer.WriteMapHeader(0);
        }

        public global::TestData2.Nest2.IdType Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            reader.Skip();
            var ____result = new global::TestData2.Nest2.IdType();
            return ____result;
        }
    }

    public sealed class PropNameCheck1Formatter : global::MessagePack.Formatters.IMessagePackFormatter<global::TestData2.PropNameCheck1>
    {
        // MyProperty1
        private static global::System.ReadOnlySpan<byte> GetSpan_MyProperty1() => new byte[1 + 11] { 171, 77, 121, 80, 114, 111, 112, 101, 114, 116, 121, 49 };
        // MyProperty2
        private static global::System.ReadOnlySpan<byte> GetSpan_MyProperty2() => new byte[1 + 11] { 171, 77, 121, 80, 114, 111, 112, 101, 114, 116, 121, 50 };

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::TestData2.PropNameCheck1 value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNil();
                return;
            }

            IFormatterResolver formatterResolver = options.Resolver;
            writer.WriteMapHeader(2);
            writer.WriteRaw(GetSpan_MyProperty1());
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.MyProperty1, options);
            writer.WriteRaw(GetSpan_MyProperty2());
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.MyProperty2, options);
        }

        public global::TestData2.PropNameCheck1 Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            IFormatterResolver formatterResolver = options.Resolver;
            var length = reader.ReadMapHeader();
            var __MyProperty1__ = default(string);
            var __MyProperty2__ = default(string);

            for (int i = 0; i < length; i++)
            {
                ReadOnlySpan<byte> stringKey = global::MessagePack.Internal.CodeGenHelpers.ReadStringSpan(ref reader);
                switch (stringKey.Length)
                {
                    default:
                    FAIL:
                      reader.Skip();
                      continue;
                    case 11:
                        switch (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey))
                        {
                            default: goto FAIL;
                            case 8243118316933118285UL:
                                switch (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey))
                                {
                                    default: goto FAIL;
                                    case 3242356UL:
                                        __MyProperty1__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                                        continue;
                                    case 3307892UL:
                                        __MyProperty2__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                                        continue;
                                }

                        }

                }
            }

            var ____result = new global::TestData2.PropNameCheck1()
            {
                MyProperty1 = __MyProperty1__,
                MyProperty2 = __MyProperty2__,
            };

            reader.Depth--;
            return ____result;
        }
    }

    public sealed class PropNameCheck2Formatter : global::MessagePack.Formatters.IMessagePackFormatter<global::TestData2.PropNameCheck2>
    {
        // MyProperty1
        private static global::System.ReadOnlySpan<byte> GetSpan_MyProperty1() => new byte[1 + 11] { 171, 77, 121, 80, 114, 111, 112, 101, 114, 116, 121, 49 };
        // MyProperty2
        private static global::System.ReadOnlySpan<byte> GetSpan_MyProperty2() => new byte[1 + 11] { 171, 77, 121, 80, 114, 111, 112, 101, 114, 116, 121, 50 };

        public void Serialize(ref global::MessagePack.MessagePackWriter writer, global::TestData2.PropNameCheck2 value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNil();
                return;
            }

            IFormatterResolver formatterResolver = options.Resolver;
            writer.WriteMapHeader(2);
            writer.WriteRaw(GetSpan_MyProperty1());
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.MyProperty1, options);
            writer.WriteRaw(GetSpan_MyProperty2());
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.MyProperty2, options);
        }

        public global::TestData2.PropNameCheck2 Deserialize(ref global::MessagePack.MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            IFormatterResolver formatterResolver = options.Resolver;
            var length = reader.ReadMapHeader();
            var __MyProperty1__ = default(string);
            var __MyProperty2__ = default(string);

            for (int i = 0; i < length; i++)
            {
                ReadOnlySpan<byte> stringKey = global::MessagePack.Internal.CodeGenHelpers.ReadStringSpan(ref reader);
                switch (stringKey.Length)
                {
                    default:
                    FAIL:
                      reader.Skip();
                      continue;
                    case 11:
                        switch (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey))
                        {
                            default: goto FAIL;
                            case 8243118316933118285UL:
                                switch (global::MessagePack.Internal.AutomataKeyGen.GetKey(ref stringKey))
                                {
                                    default: goto FAIL;
                                    case 3242356UL:
                                        __MyProperty1__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                                        continue;
                                    case 3307892UL:
                                        __MyProperty2__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                                        continue;
                                }

                        }

                }
            }

            var ____result = new global::TestData2.PropNameCheck2()
            {
                MyProperty1 = __MyProperty1__,
                MyProperty2 = __MyProperty2__,
            };

            reader.Depth--;
            return ____result;
        }
    }
}

