using NUnit.Framework;
using SaveSystem.GameData;
using System.Collections.Generic;

namespace SaveSystem.Tests.GameData.BaseClasses
{
    [TestFixture]
    public class IntDataTests
    {
        [Test]
        public void Test_Constructor_WithDictionary()
        {
            // Arrange
            var initialData = new Dictionary<string, int> { { "key1", 1 }, { "key2", 2 } };

            // Act
            var intData = new IntData(initialData);

            // Assert
            Assert.AreEqual(1, intData.Data("key1"));
            Assert.AreEqual(2, intData.Data("key2"));
        }

        [Test]
        public void Test_Constructor_WithoutDictionary()
        {
            // Act
            var intData = new IntData();

            // Assert
            Assert.IsNotNull(intData);
            Assert.IsEmpty(intData.GetAllKeys());
        }

        [Test]
        public void Test_SetData_AddsNewKey()
        {
            // Arrange
            var intData = new IntData();

            // Act
            intData.SetData("key1", 1);

            // Assert
            Assert.IsTrue(intData.ContainsKey("key1"));
            Assert.AreEqual(1, intData.Data("key1"));
        }

        [Test]
        public void Test_SetData_UpdatesExistingKey()
        {
            // Arrange
            var intData = new IntData();
            intData.SetData("key1", 1);

            // Act
            intData.SetData("key1", 2);

            // Assert
            Assert.AreEqual(2, intData.Data("key1"));
        }

        [Test]
        public void Test_Data_ThrowsKeyNotFoundException()
        {
            // Arrange
            var intData = new IntData();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => intData.Data("nonexistentKey"));
        }

        [Test]
        public void Test_ContainsKey_ReturnsTrueForExistingKey()
        {
            // Arrange
            var intData = new IntData();
            intData.SetData("key1", 1);

            // Act
            bool containsKey = intData.ContainsKey("key1");

            // Assert
            Assert.IsTrue(containsKey);
        }

        [Test]
        public void Test_ContainsKey_ReturnsFalseForNonExistingKey()
        {
            // Arrange
            var intData = new IntData();

            // Act
            bool containsKey = intData.ContainsKey("nonexistentKey");

            // Assert
            Assert.IsFalse(containsKey);
        }

        [Test]
        public void Test_GetAllKeys_ReturnsAllKeys()
        {
            // Arrange
            var intData = new IntData();
            intData.SetData("key1", 1);
            intData.SetData("key2", 2);

            // Act
            var keys = intData.GetAllKeys();

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { "key1", "key2" }, keys);
        }

        [Test]
        public void Test_Equals_SameData_ReturnsTrue()
        {
            // Arrange
            var data1 = new IntData(new Dictionary<string, int> { { "key1", 1 }, { "key2", 2 } });
            var data2 = new IntData(new Dictionary<string, int> { { "key1", 1 }, { "key2", 2 } });

            // Act & Assert
            Assert.IsTrue(data1.Equals(data2));
        }

        [Test]
        public void Test_Equals_DifferentData_ReturnsFalse()
        {
            // Arrange
            var data1 = new IntData(new Dictionary<string, int> { { "key1", 1 }, { "key2", 2 } });
            var data2 = new IntData(new Dictionary<string, int> { { "key1", 1 }, { "key2", 3 } });

            // Act & Assert
            Assert.IsFalse(data1.Equals(data2));
        }

        [Test]
        public void Test_GetHashCode_SameData_ReturnsSameHashCode()
        {
            // Arrange
            var data1 = new IntData(new Dictionary<string, int> { { "key1", 1 }, { "key2", 2 } });
            var data2 = new IntData(new Dictionary<string, int> { { "key1", 1 }, { "key2", 2 } });

            // Act & Assert
            Assert.AreEqual(data1.GetHashCode(), data2.GetHashCode());
        }

        [Test]
        public void Test_GetHashCode_DifferentData_ReturnsDifferentHashCode()
        {
            // Arrange
            var data1 = new IntData(new Dictionary<string, int> { { "key1", 1 }, { "key2", 2 } });
            var data2 = new IntData(new Dictionary<string, int> { { "key1", 1 }, { "key2", 3 } });

            // Act & Assert
            Assert.AreNotEqual(data1.GetHashCode(), data2.GetHashCode());
        }
    }
}