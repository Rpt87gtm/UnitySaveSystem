using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SaveSystem.Tests.JsonConverter
{
    [TestFixture]
    public class JsonConverterTests
    {
        protected IEnumerable<IJsonConverter> _converters;
        protected IEnumerable<object> _dataTypes;

        [OneTimeSetUp]
        public virtual void Init()
        {
            _converters = ConverterConfiguration.GetConverters();
            _dataTypes = ConverterConfiguration.GetDataTypes();
        }
        [Test]
        public void ToObject_ShouldHandleNullJson()
        {
            foreach (var converter in _converters)
            {
                object obj = null;

                Assert.Throws<ArgumentNullException>(() => converter.ToJson(obj));
            }
        }

        [Test]
        public void ToJson_ShouldThrowExceptionForNullObject()
        {
            foreach (var converter in _converters)
            {
                object obj = null;

                Assert.Throws<ArgumentNullException>(() => converter.ToJson(obj));
            }
        }
        [Test]
        public void ToJson_ShouldThrowExceptionForNullJson()
        {
            foreach (var converter in _converters)
            {
                string json = null;

                Assert.Throws<ArgumentNullException>(() => converter.ToObject<Object>(json));
            }
        }

        [Test]
        public void ToObject_ShouldThrowExceptionForInvalidJson()
        {
            foreach (var converter in _converters)
            {
                string invalidJson = "invalid json";

                Assert.Throws<ArgumentException>(() => converter.ToObject<object>(invalidJson));
            }
        }


        [Test]
        public void AllConvertersAndDataTypes_ShouldSerializeAndDeserializeCorrectly()
        {
            foreach (var converter in _converters)
            {
                foreach (var data in _dataTypes)
                {
                    var dataType = data.GetType();

                    // Test serialization
                    string json = converter.ToJson(data);
                    Assert.IsNotNull(json, $"Serialization failed for data type {dataType.Name} using converter {converter.GetType().Name}");
                    Assert.IsNotEmpty(json);

                    // Test deserialization
                    // Use reflection to call ToObject<T> for each data type
                    MethodInfo method = typeof(IJsonConverter).GetMethod(nameof(IJsonConverter.ToObject), new[] { typeof(string) })
                        .MakeGenericMethod(dataType);

                    var deserialized = method.Invoke(converter, new object[] { json });

                    Assert.IsNotNull(deserialized, $"Deserialization failed for data type {dataType.Name} using converter {converter.GetType().Name}");
                    Assert.AreEqual(dataType, deserialized.GetType(), $"Type mismatch after deserialization for data type {dataType.Name} using converter {converter.GetType().Name}");
                    Assert.AreEqual(data, deserialized);
                }
            }
        }
    }
}
