using NUnit.Framework;
using SaveSystem.GameData;
using System.Collections.Generic;

namespace SaveSystem.Tests.GameData.BaseClasses
{
    [TestFixture]
    public class StringDataTests
    {
        [Test]
        public void Test_Constructor_WithDictionary()
        {
            // Arrange
            var initialData = new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } };

            // Act
            var stringData = new StringData(initialData);

            // Assert
            Assert.AreEqual("value1", stringData.Data("key1"));
            Assert.AreEqual("value2", stringData.Data("key2"));
        }

        [Test]
        public void Test_Constructor_WithoutDictionary()
        {
            // Act
            var stringData = new StringData();

            // Assert
            Assert.IsNotNull(stringData);
            Assert.IsEmpty(stringData.GetAllKeys());
        }

        [Test]
        public void Test_SetData_AddsNewKey()
        {
            // Arrange
            var stringData = new StringData();

            // Act
            stringData.SetData("key1", "value1");

            // Assert
            Assert.IsTrue(stringData.ContainsKey("key1"));
            Assert.AreEqual("value1", stringData.Data("key1"));
        }

        [Test]
        public void Test_SetData_UpdatesExistingKey()
        {
            // Arrange
            var stringData = new StringData();
            stringData.SetData("key1", "value1");

            // Act
            stringData.SetData("key1", "value2");

            // Assert
            Assert.AreEqual("value2", stringData.Data("key1"));
        }

        [Test]
        public void Test_Data_ThrowsKeyNotFoundException()
        {
            // Arrange
            var stringData = new StringData();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => stringData.Data("nonexistentKey"));
        }

        [Test]
        public void Test_ContainsKey_ReturnsTrueForExistingKey()
        {
            // Arrange
            var stringData = new StringData();
            stringData.SetData("key1", "value1");

            // Act
            bool containsKey = stringData.ContainsKey("key1");

            // Assert
            Assert.IsTrue(containsKey);
        }

        [Test]
        public void Test_ContainsKey_ReturnsFalseForNonExistingKey()
        {
            // Arrange
            var stringData = new StringData();

            // Act
            bool containsKey = stringData.ContainsKey("nonexistentKey");

            // Assert
            Assert.IsFalse(containsKey);
        }

        [Test]
        public void Test_GetAllKeys_ReturnsAllKeys()
        {
            // Arrange
            var stringData = new StringData();
            stringData.SetData("key1", "value1");
            stringData.SetData("key2", "value2");

            // Act
            var keys = stringData.GetAllKeys();

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { "key1", "key2" }, keys);
        }

        [Test]
        public void Test_Equals_SameData_ReturnsTrue()
        {
            // Arrange
            var data1 = new StringData(new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } });
            var data2 = new StringData(new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } });

            // Act & Assert
            Assert.IsTrue(data1.Equals(data2));
        }

        [Test]
        public void Test_Equals_DifferentData_ReturnsFalse()
        {
            // Arrange
            var data1 = new StringData(new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } });
            var data2 = new StringData(new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value3" } });

            // Act & Assert
            Assert.IsFalse(data1.Equals(data2));
        }

        [Test]
        public void Test_GetHashCode_SameData_ReturnsSameHashCode()
        {
            // Arrange
            var data1 = new StringData(new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } });
            var data2 = new StringData(new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } });

            // Act & Assert
            Assert.AreEqual(data1.GetHashCode(), data2.GetHashCode());
        }

        [Test]
        public void Test_GetHashCode_DifferentData_ReturnsDifferentHashCode()
        {
            // Arrange
            var data1 = new StringData(new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } });
            var data2 = new StringData(new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value3" } });

            // Act & Assert
            Assert.AreNotEqual(data1.GetHashCode(), data2.GetHashCode());
        }
    }
}