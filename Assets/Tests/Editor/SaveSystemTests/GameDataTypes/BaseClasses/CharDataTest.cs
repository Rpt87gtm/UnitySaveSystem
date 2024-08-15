using NUnit.Framework;
using SaveSystem.GameData;
using System;
using System.Collections.Generic;

namespace SaveSystem.Tests.GameData.BaseClasses
{
    [TestFixture]
    public class CharDataTests
    {
        [Test]
        public void Test_Constructor_WithDictionary()
        {
            // Arrange
            var initialData = new Dictionary<string, char> { { "key1", 'a' }, { "key2", 'b' } };

            // Act
            var charData = new CharData(initialData);

            // Assert
            Assert.AreEqual('a', charData.Data("key1"));
            Assert.AreEqual('b', charData.Data("key2"));
        }

        [Test]
        public void Test_Constructor_WithoutDictionary()
        {
            // Act
            var charData = new CharData();

            // Assert
            Assert.IsNotNull(charData);
            Assert.IsEmpty(charData.GetAllKeys());
        }

        [Test]
        public void Test_SetData_AddsNewKey()
        {
            // Arrange
            var charData = new CharData();

            // Act
            charData.SetData("key1", 'a');

            // Assert
            Assert.IsTrue(charData.ContainsKey("key1"));
            Assert.AreEqual('a', charData.Data("key1"));
        }

        [Test]
        public void Test_SetData_UpdatesExistingKey()
        {
            // Arrange
            var charData = new CharData();
            charData.SetData("key1", 'a');

            // Act
            charData.SetData("key1", 'b');

            // Assert
            Assert.AreEqual('b', charData.Data("key1"));
        }

        [Test]
        public void Test_Data_ThrowsKeyNotFoundException()
        {
            // Arrange
            var charData = new CharData();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => charData.Data("nonexistentKey"));
        }

        [Test]
        public void Test_ContainsKey_ReturnsTrueForExistingKey()
        {
            // Arrange
            var charData = new CharData();
            charData.SetData("key1", 'a');

            // Act
            bool containsKey = charData.ContainsKey("key1");

            // Assert
            Assert.IsTrue(containsKey);
        }

        [Test]
        public void Test_ContainsKey_ReturnsFalseForNonExistingKey()
        {
            // Arrange
            var charData = new CharData();

            // Act
            bool containsKey = charData.ContainsKey("nonexistentKey");

            // Assert
            Assert.IsFalse(containsKey);
        }

        [Test]
        public void Test_GetAllKeys_ReturnsAllKeys()
        {
            // Arrange
            var charData = new CharData();
            charData.SetData("key1", 'a');
            charData.SetData("key2", 'b');

            // Act
            var keys = charData.GetAllKeys();

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { "key1", "key2" }, keys);
        }

        [Test]
        public void Test_Equals_SameData_ReturnsTrue()
        {
            // Arrange
            var data1 = new CharData(new Dictionary<string, char> { { "key1", 'a' }, { "key2", 'b' } });
            var data2 = new CharData(new Dictionary<string, char> { { "key1", 'a' }, { "key2", 'b' } });

            // Act & Assert
            Assert.IsTrue(data1.Equals(data2));
        }

        [Test]
        public void Test_Equals_DifferentData_ReturnsFalse()
        {
            // Arrange
            var data1 = new CharData(new Dictionary<string, char> { { "key1", 'a' }, { "key2", 'b' } });
            var data2 = new CharData(new Dictionary<string, char> { { "key1", 'a' }, { "key2", 'c' } });

            // Act & Assert
            Assert.IsFalse(data1.Equals(data2));
        }

        [Test]
        public void Test_GetHashCode_SameData_ReturnsSameHashCode()
        {
            // Arrange
            var data1 = new CharData(new Dictionary<string, char> { { "key1", 'a' }, { "key2", 'b' } });
            var data2 = new CharData(new Dictionary<string, char> { { "key1", 'a' }, { "key2", 'b' } });

            // Act & Assert
            Assert.AreEqual(data1.GetHashCode(), data2.GetHashCode());
        }

        [Test]
        public void Test_GetHashCode_DifferentData_ReturnsDifferentHashCode()
        {
            // Arrange
            var data1 = new CharData(new Dictionary<string, char> { { "key1", 'a' }, { "key2", 'b' } });
            var data2 = new CharData(new Dictionary<string, char> { { "key1", 'a' }, { "key2", 'c' } });

            // Act & Assert
            Assert.AreNotEqual(data1.GetHashCode(), data2.GetHashCode());
        }
        [Test]
        public void Test_RemoveData_RemovesExistingKey()
        {
            // Arrange
            var charData = new CharData();
            charData.SetData("key1", 'd');

            // Act
            charData.RemoveData("key1");

            // Assert
            Assert.IsFalse(charData.ContainsKey("key1"));
        }

        [Test]
        public void Test_RemoveData_ThrowsKeyNotFoundExceptionForNonExistingKey()
        {
            // Arrange
            var charData = new CharData();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => charData.RemoveData("nonexistentKey"));
        }

        [Test]
        public void Test_RemoveData_ThrowsArgumentExceptionForNullKey()
        {
            // Arrange
            var charData = new CharData();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => charData.RemoveData(null));
        }

        [Test]
        public void Test_RemoveData_ThrowsArgumentExceptionForEmptyKey()
        {
            // Arrange
            var charData = new CharData();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => charData.RemoveData(""));
        }
    }
}