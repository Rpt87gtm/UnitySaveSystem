using NUnit.Framework;
using SaveSystem.GameData;
using System;
using System.Collections.Generic;

namespace SaveSystem.Tests.GameData.BaseClasses
{
    [TestFixture]
    public class ShortDataTests
    {
        [Test]
        public void Test_Constructor_WithDictionary()
        {
            // Arrange
            var initialData = new Dictionary<string, short> { { "key1", 1 }, { "key2", 2 } };

            // Act
            var shortData = new ShortData(initialData);

            // Assert
            Assert.AreEqual(1, shortData.Data("key1"));
            Assert.AreEqual(2, shortData.Data("key2"));
        }

        [Test]
        public void Test_Constructor_WithoutDictionary()
        {
            // Act
            var shortData = new ShortData();

            // Assert
            Assert.IsNotNull(shortData);
            Assert.IsEmpty(shortData.GetAllKeys());
        }

        [Test]
        public void Test_SetData_AddsNewKey()
        {
            // Arrange
            var shortData = new ShortData();

            // Act
            shortData.SetData("key1", 1);

            // Assert
            Assert.IsTrue(shortData.ContainsKey("key1"));
            Assert.AreEqual(1, shortData.Data("key1"));
        }

        [Test]
        public void Test_SetData_UpdatesExistingKey()
        {
            // Arrange
            var shortData = new ShortData();
            shortData.SetData("key1", 1);

            // Act
            shortData.SetData("key1", 2);

            // Assert
            Assert.AreEqual(2, shortData.Data("key1"));
        }

        [Test]
        public void Test_Data_ThrowsKeyNotFoundException()
        {
            // Arrange
            var shortData = new ShortData();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => shortData.Data("nonexistentKey"));
        }

        [Test]
        public void Test_ContainsKey_ReturnsTrueForExistingKey()
        {
            // Arrange
            var shortData = new ShortData();
            shortData.SetData("key1", 1);

            // Act
            bool containsKey = shortData.ContainsKey("key1");

            // Assert
            Assert.IsTrue(containsKey);
        }

        [Test]
        public void Test_ContainsKey_ReturnsFalseForNonExistingKey()
        {
            // Arrange
            var shortData = new ShortData();

            // Act
            bool containsKey = shortData.ContainsKey("nonexistentKey");

            // Assert
            Assert.IsFalse(containsKey);
        }

        [Test]
        public void Test_GetAllKeys_ReturnsAllKeys()
        {
            // Arrange
            var shortData = new ShortData();
            shortData.SetData("key1", 1);
            shortData.SetData("key2", 2);

            // Act
            var keys = shortData.GetAllKeys();

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { "key1", "key2" }, keys);
        }

        [Test]
        public void Test_Equals_SameData_ReturnsTrue()
        {
            // Arrange
            var data1 = new ShortData(new Dictionary<string, short> { { "key1", 1 }, { "key2", 2 } });
            var data2 = new ShortData(new Dictionary<string, short> { { "key1", 1 }, { "key2", 2 } });

            // Act & Assert
            Assert.IsTrue(data1.Equals(data2));
        }

        [Test]
        public void Test_Equals_DifferentData_ReturnsFalse()
        {
            // Arrange
            var data1 = new ShortData(new Dictionary<string, short> { { "key1", 1 }, { "key2", 2 } });
            var data2 = new ShortData(new Dictionary<string, short> { { "key1", 1 }, { "key2", 3 } });

            // Act & Assert
            Assert.IsFalse(data1.Equals(data2));
        }

        [Test]
        public void Test_GetHashCode_SameData_ReturnsSameHashCode()
        {
            // Arrange
            var data1 = new ShortData(new Dictionary<string, short> { { "key1", 1 }, { "key2", 2 } });
            var data2 = new ShortData(new Dictionary<string, short> { { "key1", 1 }, { "key2", 2 } });

            // Act & Assert
            Assert.AreEqual(data1.GetHashCode(), data2.GetHashCode());
        }

        [Test]
        public void Test_GetHashCode_DifferentData_ReturnsDifferentHashCode()
        {
            // Arrange
            var data1 = new ShortData(new Dictionary<string, short> { { "key1", 1 }, { "key2", 2 } });
            var data2 = new ShortData(new Dictionary<string, short> { { "key1", 1 }, { "key2", 3 } });

            // Act & Assert
            Assert.AreNotEqual(data1.GetHashCode(), data2.GetHashCode());
        }
        [Test]
        public void Test_RemoveData_RemovesExistingKey()
        {
            // Arrange
            var shortData = new ShortData();
            shortData.SetData("key1", 231);

            // Act
            shortData.RemoveData("key1");

            // Assert
            Assert.IsFalse(shortData.ContainsKey("key1"));
        }

        [Test]
        public void Test_RemoveData_ThrowsKeyNotFoundExceptionForNonExistingKey()
        {
            // Arrange
            var shortData = new ShortData();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => shortData.RemoveData("nonexistentKey"));
        }

        [Test]
        public void Test_RemoveData_ThrowsArgumentExceptionForNullKey()
        {
            // Arrange
            var shortData = new ShortData();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => shortData.RemoveData(null));
        }

        [Test]
        public void Test_RemoveData_ThrowsArgumentExceptionForEmptyKey()
        {
            // Arrange
            var shortData = new ShortData();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => shortData.RemoveData(""));
        }
    }
}