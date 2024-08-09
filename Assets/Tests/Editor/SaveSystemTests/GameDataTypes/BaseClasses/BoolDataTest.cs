using NUnit.Framework;
using SaveSystem.GameData;
using System.Collections.Generic;

namespace SaveSystem.Tests.GameData.BaseClasses
{
    [TestFixture]
    public class BoolDataTests
    {
        [Test]
        public void Test_Constructor_WithDictionary()
        {
            // Arrange
            var initialData = new Dictionary<string, bool> { { "key1", true }, { "key2", false } };

            // Act
            var boolData = new BoolData(initialData);

            // Assert
            Assert.AreEqual(true, boolData.Data("key1"));
            Assert.AreEqual(false, boolData.Data("key2"));
        }

        [Test]
        public void Test_Constructor_WithoutDictionary()
        {
            // Act
            var boolData = new BoolData();

            // Assert
            Assert.IsNotNull(boolData);
            Assert.IsEmpty(boolData.GetAllKeys());
        }

        [Test]
        public void Test_SetData_AddsNewKey()
        {
            // Arrange
            var boolData = new BoolData();

            // Act
            boolData.SetData("key1", true);

            // Assert
            Assert.IsTrue(boolData.ContainsKey("key1"));
            Assert.AreEqual(true, boolData.Data("key1"));
        }

        [Test]
        public void Test_SetData_UpdatesExistingKey()
        {
            // Arrange
            var boolData = new BoolData();
            boolData.SetData("key1", true);

            // Act
            boolData.SetData("key1", false);

            // Assert
            Assert.AreEqual(false, boolData.Data("key1"));
        }

        [Test]
        public void Test_Data_ThrowsKeyNotFoundException()
        {
            // Arrange
            var boolData = new BoolData();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => boolData.Data("nonexistentKey"));
        }

        [Test]
        public void Test_ContainsKey_ReturnsTrueForExistingKey()
        {
            // Arrange
            var boolData = new BoolData();
            boolData.SetData("key1", true);

            // Act
            bool containsKey = boolData.ContainsKey("key1");

            // Assert
            Assert.IsTrue(containsKey);
        }

        [Test]
        public void Test_ContainsKey_ReturnsFalseForNonExistingKey()
        {
            // Arrange
            var boolData = new BoolData();

            // Act
            bool containsKey = boolData.ContainsKey("nonexistentKey");

            // Assert
            Assert.IsFalse(containsKey);
        }

        [Test]
        public void Test_GetAllKeys_ReturnsAllKeys()
        {
            // Arrange
            var boolData = new BoolData();
            boolData.SetData("key1", true);
            boolData.SetData("key2", false);

            // Act
            var keys = boolData.GetAllKeys();

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { "key1", "key2" }, keys);
        }

        [Test]
        public void Test_Equals_SameData_ReturnsTrue()
        {
            // Arrange
            var data1 = new BoolData(new Dictionary<string, bool> { { "key1", true }, { "key2", false } });
            var data2 = new BoolData(new Dictionary<string, bool> { { "key1", true }, { "key2", false } });

            // Act & Assert
            Assert.IsTrue(data1.Equals(data2));
        }

        [Test]
        public void Test_Equals_DifferentData_ReturnsFalse()
        {
            // Arrange
            var data1 = new BoolData(new Dictionary<string, bool> { { "key1", true }, { "key2", false } });
            var data2 = new BoolData(new Dictionary<string, bool> { { "key1", true }, { "key2", true } });

            // Act & Assert
            Assert.IsFalse(data1.Equals(data2));
        }

        [Test]
        public void Test_GetHashCode_SameData_ReturnsSameHashCode()
        {
            // Arrange
            var data1 = new BoolData(new Dictionary<string, bool> { { "key1", true }, { "key2", false } });
            var data2 = new BoolData(new Dictionary<string, bool> { { "key1", true }, { "key2", false } });

            // Act & Assert
            Assert.AreEqual(data1.GetHashCode(), data2.GetHashCode());
        }

        [Test]
        public void Test_GetHashCode_DifferentData_ReturnsDifferentHashCode()
        {
            // Arrange
            var data1 = new BoolData(new Dictionary<string, bool> { { "key1", true }, { "key2", false } });
            var data2 = new BoolData(new Dictionary<string, bool> { { "key1", true }, { "key2", true } });

            // Act & Assert
            Assert.AreNotEqual(data1.GetHashCode(), data2.GetHashCode());
        }
    }
}