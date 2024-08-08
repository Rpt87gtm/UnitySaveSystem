using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SaveSystem.Tests
{
    [TestFixture]
    public class JsonConverterTests
    {
        [Test]
        public void AllConvertersAndDataTypes_ShouldSerializeAndDeserializeCorrectly()
        {
            // Get converters and data types from configuration
            var converters = ConverterConfiguration.GetConverters();
            var dataTypes = ConverterConfiguration.GetDataTypes();

            foreach (var converter in converters)
            {
                foreach (var data in dataTypes)
                {
                    var dataType = data.GetType();

                    // Test serialization
                    string json = converter.ToJson(data);
                    Assert.IsNotNull(json, $"Serialization failed for data type {dataType.Name} using converter {converter.GetType().Name}");
                    Assert.IsNotEmpty(json);

                    // Test deserialization
                    // Use reflection to call ToObject<T> for each data type
                    MethodInfo method = typeof(IJsonConverter).GetMethod(nameof(IJsonConverter.ToObject))
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
