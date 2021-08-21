﻿// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MessagePack.Resolvers;
using Xunit;
using Xunit.Abstractions;

namespace MessagePack.Tests
{
#if !ENABLE_IL2CPP
    public class DataContractTest
    {
        private readonly ITestOutputHelper logger;

        public DataContractTest(ITestOutputHelper logger)
        {
            this.logger = logger;
        }

        [DataContract]
        public class MyClass
        {
            [DataMember(Order = 0)]
            public int MyProperty1 { get; set; }

            [DataMember(Order = 1)]
            public string MyProperty2;
        }

        [DataContract]
        public class MyClass1
        {
            [DataMember(Name = "mp1")]
            public int MyProperty1 { get; set; }

            [DataMember(Name = "mp2")]
            public string MyProperty2;
        }

        [DataContract]
        public class MyClass2
        {
            [DataMember]
            public int MyProperty1 { get; set; }

            [DataMember]
            public string MyProperty2;
        }

        [DataContract]
        public class ClassWithPublicMembersWithoutAttributes
        {
            [DataMember]
            public int AttributedProperty { get; set; }

            public int UnattributedProperty { get; set; }

            [IgnoreDataMember]
            public int IgnoredProperty { get; set; }

            [DataMember]
            public int AttributedField;

            public int UnattributedField;

            [IgnoreDataMember]
            public int IgnoredField;
        }

        [DataContract]
        public class ClassWithPrivateReadonlyDictionary
        {
            [DataMember(Name = "Key", Order = 0, EmitDefaultValue = false)]
            private readonly Guid? key;

            [DataMember(Name = "Body", Order = 1, EmitDefaultValue = false)]
            private readonly Dictionary<string, object> body = new Dictionary<string, object>();

            public ClassWithPrivateReadonlyDictionary(Guid? key)
            {
                this.key = key;
            }

            internal Dictionary<string, object> GetBody() => this.body;
        }

        [Fact]
        public void SerializeOrder()
        {
            var mc = new MyClass { MyProperty1 = 100, MyProperty2 = "foobar" };

            var bin = MessagePackSerializer.Serialize(mc);
            MyClass mc2 = MessagePackSerializer.Deserialize<MyClass>(bin);

            mc.MyProperty1.Is(mc2.MyProperty1);
            mc.MyProperty2.Is(mc2.MyProperty2);

            MessagePackSerializer.ConvertToJson(bin).Is(@"[100,""foobar""]");
        }

        [Fact]
        public void SerializeName()
        {
            var mc = new MyClass1 { MyProperty1 = 100, MyProperty2 = "foobar" };

            var bin = MessagePackSerializer.Serialize(mc);

            MessagePackSerializer.ConvertToJson(bin).Is(@"{""mp1"":100,""mp2"":""foobar""}");

            MyClass1 mc2 = MessagePackSerializer.Deserialize<MyClass1>(bin);

            mc.MyProperty1.Is(mc2.MyProperty1);
            mc.MyProperty2.Is(mc2.MyProperty2);
        }

        [Fact]
        public void Serialize()
        {
            var mc = new MyClass2 { MyProperty1 = 100, MyProperty2 = "foobar" };

            var bin = MessagePackSerializer.Serialize(mc);
            MyClass2 mc2 = MessagePackSerializer.Deserialize<MyClass2>(bin);

            mc.MyProperty1.Is(mc2.MyProperty1);
            mc.MyProperty2.Is(mc2.MyProperty2);

            MessagePackSerializer.ConvertToJson(bin).Is(@"{""MyProperty1"":100,""MyProperty2"":""foobar""}");
        }

        [Fact]
        public void Serialize_WithVariousAttributes()
        {
            var mc = new ClassWithPublicMembersWithoutAttributes
            {
                AttributedProperty = 1,
                UnattributedProperty = 2,
                IgnoredProperty = 3,
                AttributedField = 4,
                UnattributedField = 5,
                IgnoredField = 6,
            };

            var bin = MessagePackSerializer.Serialize(mc);
            ClassWithPublicMembersWithoutAttributes mc2 = MessagePackSerializer.Deserialize<ClassWithPublicMembersWithoutAttributes>(bin);

            mc2.AttributedProperty.Is(mc.AttributedProperty);
            mc2.AttributedField.Is(mc.AttributedField);

            mc2.UnattributedProperty.Is(0);
            mc2.IgnoredProperty.Is(0);
            mc2.UnattributedField.Is(0);
            mc2.IgnoredField.Is(0);

            MessagePackSerializer.ConvertToJson(bin).Is(@"{""AttributedProperty"":1,""AttributedField"":4}");
        }

        [Fact]
        public void DeserializeTypeWithPrivateReadonlyDictionary()
        {
            var before = new ClassWithPrivateReadonlyDictionary(Guid.NewGuid());
            before.GetBody()["name"] = "my name";
            byte[] bytes = MessagePackSerializer.Serialize(before, StandardResolverAllowPrivate.Options);
            string json = MessagePackSerializer.ConvertToJson(bytes); // just for check that json has _body' values.
            this.logger.WriteLine(json);

            var after = MessagePackSerializer.Deserialize<ClassWithPrivateReadonlyDictionary>(bytes, StandardResolverAllowPrivate.Options);
            Assert.Equal("my name", after.GetBody()["name"]);
        }

        [Fact]
        public void DeserializeTypeWithPrivateReadonlyDictionary_DCS()
        {
            var before = new ClassWithPrivateReadonlyDictionary(Guid.NewGuid());
            before.GetBody()["name"] = "my name";
            DataContractSerializer dcs = new DataContractSerializer(typeof(ClassWithPrivateReadonlyDictionary));
            using var ms = new MemoryStream();
            dcs.WriteObject(ms, before);
            ms.Position = 0;
            var after = (ClassWithPrivateReadonlyDictionary)dcs.ReadObject(ms);
            Assert.Equal("my name", after.GetBody()["name"]);
        }
    }

#endif
}
