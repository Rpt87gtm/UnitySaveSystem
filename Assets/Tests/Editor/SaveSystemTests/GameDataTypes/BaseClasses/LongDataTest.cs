using NUnit.Framework;
using SaveSystem.GameData;
using System;
using System.Collections.Generic;

namespace SaveSystem.Tests.GameData.BaseClasses
{
    [TestFixture]
    public class LongDataTests
    {
        [Test]
        public void Test_Constructor_WithDictionary()
        {
            // Arrange
            var initialData = new Dictionary<string, long> { { "key1", 1L }, { "key2", 2L } };

            // Act
            var longData = new LongData(initialData);

            // Assert
            Assert.AreEqual(1L, longData.Data("key1"));
            Assert.AreEqual(2L, longData.Data("key2"));
        }

        [Test]
        public void Test_Constructor_WithoutDictionary()
        {
            // Act
            var longData = new LongData();

            // Assert
            Assert.IsNotNull(longData);
            Assert.IsEmpty(longData.GetAllKeys());
        }

        [Test]
        public void Test_SetData_AddsNewKey()
        {
            // Arrange
            var longData = new LongData();

            // Act
            longData.SetData("key1", 1L);

            // Assert
            Assert.IsTrue(longData.ContainsKey("key1"));
            Assert.AreEqual(1L, longData.Data("key1"));
        }

        [Test]
        public void Test_SetData_UpdatesExistingKey()
        {
            // Arrange
            var longData = new LongData();
            longData.SetData("key1", 1L);

            // Act
            longData.SetData("key1", 2L);

            // Assert
            Assert.AreEqual(2L, longData.Data("key1"));
        }

        [Test]
        public void Test_Data_ThrowsKeyNotFoundException()
        {
            // Arrange
            var longData = new LongData();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => longData.Data("nonexistentKey"));
        }

        [Test]
        public void Test_ContainsKey_ReturnsTrueForExistingKey()
        {
            // Arrange
            var longData = new LongData();
            longData.SetData("key1", 1L);

            // Act
            bool containsKey = longData.ContainsKey("key1");

            // Assert
            Assert.IsTrue(containsKey);
        }

        [Test]
        public void Test_ContainsKey_ReturnsFalseForNonExistingKey()
        {
            // Arrange
            var longData = new LongData();

            // Act
            bool containsKey = longData.ContainsKey("nonexistentKey");

            // Assert
            Assert.IsFalse(containsKey);
        }

        [Test]
        public void Test_GetAllKeys_ReturnsAllKeys()
        {
            // Arrange
            var longData = new LongData();
            longData.SetData("key1", 1L);
            longData.SetData("key2", 2L);

            // Act
            var keys = longData.GetAllKeys();

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { "key1", "key2" }, keys);
        }

        [Test]
        public void Test_Equals_SameData_ReturnsTrue()
        {
            // Arrange
            var data1 = new LongData(new Dictionary<string, long> { { "key1", 1L }, { "key2", 2L } });
            var data2 = new LongData(new Dictionary<string, long> { { "key1", 1L }, { "key2", 2L } });

            // Act & Assert
            Assert.IsTrue(data1.Equals(data2));
        }

        [Test]
        public void Test_Equals_DifferentData_ReturnsFalse()
        {
            // Arrange
            var data1 = new LongData(new Dictionary<string, long> { { "key1", 1L }, { "key2", 2L } });
            var data2 = new LongData(new Dictionary<string, long> { { "key1", 1L }, { "key2", 3L } });

            // Act & Assert
            Assert.IsFalse(data1.Equals(data2));
        }

        [Test]
        public void Test_GetHashCode_SameData_ReturnsSameHashCode()
        {
            // Arrange
            var data1 = new LongData(new Dictionary<string, long> { { "key1", 1L }, { "key2", 2L } });
            var data2 = new LongData(new Dictionary<string, long> { { "key1", 1L }, { "key2", 2L } });

            // Act & Assert
            Assert.AreEqual(data1.GetHashCode(), data2.GetHashCode());
        }

        [Test]
        public void Test_GetHashCode_DifferentData_ReturnsDifferentHashCode()
        {
            // Arrange
            var data1 = new LongData(new Dictionary<string, long> { { "key1", 1L }, { "key2", 2L } });
            var data2 = new LongData(new Dictionary<string, long> { { "key1", 1L }, { "key2", 3L } });

            // Act & Assert
            Assert.AreNotEqual(data1.GetHashCode(), data2.GetHashCode());
        }
        [Test]
        public void Test_RemoveData_RemovesExistingKey()
        {
            // Arrange
            var longData = new LongData();
            longData.SetData("key1", 23L);

            // Act
            longData.RemoveData("key1");

            // Assert
            Assert.IsFalse(longData.ContainsKey("key1"));
        }

        [Test]
        public void Test_RemoveData_ThrowsKeyNotFoundExceptionForNonExistingKey()
        {
            // Arrange
            var longData = new LongData();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => longData.RemoveData("nonexistentKey"));
        }

        [Test]
        public void Test_RemoveData_ThrowsArgumentExceptionForNullKey()
        {
            // Arrange
            var longData = new LongData();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => longData.RemoveData(null));
        }

        [Test]
        public void Test_RemoveData_ThrowsArgumentExceptionForEmptyKey()
        {
            // Arrange
            var longData = new LongData();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => longData.RemoveData(""));
        }
    }
}