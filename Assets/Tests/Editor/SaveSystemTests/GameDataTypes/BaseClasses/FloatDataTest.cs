using NUnit.Framework;
using SaveSystem.GameData;
using System.Collections.Generic;

namespace SaveSystem.Tests.GameData.BaseClasses
{
    [TestFixture]
    public class FloatDataTests
    {
        [Test]
        public void Test_Constructor_WithDictionary()
        {
            // Arrange
            var initialData = new Dictionary<string, float> { { "key1", 1.1f }, { "key2", 2.2f } };

            // Act
            var floatData = new FloatData(initialData);

            // Assert
            Assert.AreEqual(1.1f, floatData.Data("key1"));
            Assert.AreEqual(2.2f, floatData.Data("key2"));
        }

        [Test]
        public void Test_Constructor_WithoutDictionary()
        {
            // Act
            var floatData = new FloatData();

            // Assert
            Assert.IsNotNull(floatData);
            Assert.IsEmpty(floatData.GetAllKeys());
        }

        [Test]
        public void Test_SetData_AddsNewKey()
        {
            // Arrange
            var floatData = new FloatData();

            // Act
            floatData.SetData("key1", 1.1f);

            // Assert
            Assert.IsTrue(floatData.ContainsKey("key1"));
            Assert.AreEqual(1.1f, floatData.Data("key1"));
        }

        [Test]
        public void Test_SetData_UpdatesExistingKey()
        {
            // Arrange
            var floatData = new FloatData();
            floatData.SetData("key1", 1.1f);

            // Act
            floatData.SetData("key1", 2.2f);

            // Assert
            Assert.AreEqual(2.2f, floatData.Data("key1"));
        }

        [Test]
        public void Test_Data_ThrowsKeyNotFoundException()
        {
            // Arrange
            var floatData = new FloatData();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => floatData.Data("nonexistentKey"));
        }

        [Test]
        public void Test_ContainsKey_ReturnsTrueForExistingKey()
        {
            // Arrange
            var floatData = new FloatData();
            floatData.SetData("key1", 1.1f);

            // Act
            bool containsKey = floatData.ContainsKey("key1");

            // Assert
            Assert.IsTrue(containsKey);
        }

        [Test]
        public void Test_ContainsKey_ReturnsFalseForNonExistingKey()
        {
            // Arrange
            var floatData = new FloatData();

            // Act
            bool containsKey = floatData.ContainsKey("nonexistentKey");

            // Assert
            Assert.IsFalse(containsKey);
        }

        [Test]
        public void Test_GetAllKeys_ReturnsAllKeys()
        {
            // Arrange
            var floatData = new FloatData();
            floatData.SetData("key1", 1.1f);
            floatData.SetData("key2", 2.2f);

            // Act
            var keys = floatData.GetAllKeys();

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { "key1", "key2" }, keys);
        }

        [Test]
        public void Test_Equals_SameData_ReturnsTrue()
        {
            // Arrange
            var data1 = new FloatData(new Dictionary<string, float> { { "key1", 1.1f }, { "key2", 2.2f } });
            var data2 = new FloatData(new Dictionary<string, float> { { "key1", 1.1f }, { "key2", 2.2f } });

            // Act & Assert
            Assert.IsTrue(data1.Equals(data2));
        }

        [Test]
        public void Test_Equals_DifferentData_ReturnsFalse()
        {
            // Arrange
            var data1 = new FloatData(new Dictionary<string, float> { { "key1", 1.1f }, { "key2", 2.2f } });
            var data2 = new FloatData(new Dictionary<string, float> { { "key1", 1.1f }, { "key2", 3.3f } });

            // Act & Assert
            Assert.IsFalse(data1.Equals(data2));
        }

        [Test]
        public void Test_GetHashCode_SameData_ReturnsSameHashCode()
        {
            // Arrange
            var data1 = new FloatData(new Dictionary<string, float> { { "key1", 1.1f }, { "key2", 2.2f } });
            var data2 = new FloatData(new Dictionary<string, float> { { "key1", 1.1f }, { "key2", 2.2f } });

            // Act & Assert
            Assert.AreEqual(data1.GetHashCode(), data2.GetHashCode());
        }

        [Test]
        public void Test_GetHashCode_DifferentData_ReturnsDifferentHashCode()
        {
            // Arrange
            var data1 = new FloatData(new Dictionary<string, float> { { "key1", 1.1f }, { "key2", 2.2f } });
            var data2 = new FloatData(new Dictionary<string, float> { { "key1", 1.1f }, { "key2", 3.3f } });

            // Act & Assert
            Assert.AreNotEqual(data1.GetHashCode(), data2.GetHashCode());
        }
    }
}