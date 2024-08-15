using NUnit.Framework;
using SaveSystem.GameData;
using System;
using System.Collections.Generic;

namespace SaveSystem.Tests.GameData.BaseClasses
{
    [TestFixture]
    public class ByteDataTests
    {
        [Test]
        public void Test_Constructor_WithDictionary()
        {
            // Arrange
            var initialData = new Dictionary<string, byte> { { "key1", 1 }, { "key2", 2 } };

            // Act
            var byteData = new ByteData(initialData);

            // Assert
            Assert.AreEqual(1, byteData.Data("key1"));
            Assert.AreEqual(2, byteData.Data("key2"));
        }

        [Test]
        public void Test_Constructor_WithoutDictionary()
        {
            // Act
            var byteData = new ByteData();

            // Assert
            Assert.IsNotNull(byteData);
            Assert.IsEmpty(byteData.GetAllKeys());
        }

        [Test]
        public void Test_SetData_AddsNewKey()
        {
            // Arrange
            var byteData = new ByteData();

            // Act
            byteData.SetData("key1", 1);

            // Assert
            Assert.IsTrue(byteData.ContainsKey("key1"));
            Assert.AreEqual(1, byteData.Data("key1"));
        }

        [Test]
        public void Test_SetData_UpdatesExistingKey()
        {
            // Arrange
            var byteData = new ByteData();
            byteData.SetData("key1", 1);

            // Act
            byteData.SetData("key1", 2);

            // Assert
            Assert.AreEqual(2, byteData.Data("key1"));
        }

        [Test]
        public void Test_Data_ThrowsKeyNotFoundException()
        {
            // Arrange
            var byteData = new ByteData();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => byteData.Data("nonexistentKey"));
        }

        [Test]
        public void Test_ContainsKey_ReturnsTrueForExistingKey()
        {
            // Arrange
            var byteData = new ByteData();
            byteData.SetData("key1", 1);

            // Act
            bool containsKey = byteData.ContainsKey("key1");

            // Assert
            Assert.IsTrue(containsKey);
        }

        [Test]
        public void Test_ContainsKey_ReturnsFalseForNonExistingKey()
        {
            // Arrange
            var byteData = new ByteData();

            // Act
            bool containsKey = byteData.ContainsKey("nonexistentKey");

            // Assert
            Assert.IsFalse(containsKey);
        }

        [Test]
        public void Test_GetAllKeys_ReturnsAllKeys()
        {
            // Arrange
            var byteData = new ByteData();
            byteData.SetData("key1", 1);
            byteData.SetData("key2", 2);

            // Act
            var keys = byteData.GetAllKeys();

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { "key1", "key2" }, keys);
        }

        [Test]
        public void Test_Equals_SameData_ReturnsTrue()
        {
            // Arrange
            var data1 = new ByteData(new Dictionary<string, byte> { { "key1", 1 }, { "key2", 2 } });
            var data2 = new ByteData(new Dictionary<string, byte> { { "key1", 1 }, { "key2", 2 } });

            // Act & Assert
            Assert.IsTrue(data1.Equals(data2));
        }

        [Test]
        public void Test_Equals_DifferentData_ReturnsFalse()
        {
            // Arrange
            var data1 = new ByteData(new Dictionary<string, byte> { { "key1", 1 }, { "key2", 2 } });
            var data2 = new ByteData(new Dictionary<string, byte> { { "key1", 1 }, { "key2", 3 } });

            // Act & Assert
            Assert.IsFalse(data1.Equals(data2));
        }

        [Test]
        public void Test_GetHashCode_SameData_ReturnsSameHashCode()
        {
            // Arrange
            var data1 = new ByteData(new Dictionary<string, byte> { { "key1", 1 }, { "key2", 2 } });
            var data2 = new ByteData(new Dictionary<string, byte> { { "key1", 1 }, { "key2", 2 } });

            // Act & Assert
            Assert.AreEqual(data1.GetHashCode(), data2.GetHashCode());
        }

        [Test]
        public void Test_GetHashCode_DifferentData_ReturnsDifferentHashCode()
        {
            // Arrange
            var data1 = new ByteData(new Dictionary<string, byte> { { "key1", 1 }, { "key2", 2 } });
            var data2 = new ByteData(new Dictionary<string, byte> { { "key1", 1 }, { "key2", 3 } });

            // Act & Assert
            Assert.AreNotEqual(data1.GetHashCode(), data2.GetHashCode());
        }
        [Test]
        public void Test_RemoveData_RemovesExistingKey()
        {
            // Arrange
            var byteData = new ByteData();
            byteData.SetData("key1", 2);

            // Act
            byteData.RemoveData("key1");

            // Assert
            Assert.IsFalse(byteData.ContainsKey("key1"));
        }

        [Test]
        public void Test_RemoveData_ThrowsKeyNotFoundExceptionForNonExistingKey()
        {
            // Arrange
            var byteData = new ByteData();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => byteData.RemoveData("nonexistentKey"));
        }

        [Test]
        public void Test_RemoveData_ThrowsArgumentExceptionForNullKey()
        {
            // Arrange
            var buteData = new ByteData();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => buteData.RemoveData(null));
        }

        [Test]
        public void Test_RemoveData_ThrowsArgumentExceptionForEmptyKey()
        {
            // Arrange
            var byteData = new ByteData();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => byteData.RemoveData(""));
        }
    }
}