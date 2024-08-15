using NUnit.Framework;
using SaveSystem.GameData;
using System;
using System.Collections.Generic;

namespace SaveSystem.Tests.GameData.BaseClasses
{
    [TestFixture]
    public class DoubleDataTests
    {
        [Test]
        public void Test_Constructor_WithDictionary()
        {
            // Arrange
            var initialData = new Dictionary<string, double> { { "key1", 1.1 }, { "key2", 2.2 } };

            // Act
            var doubleData = new DoubleData(initialData);

            // Assert
            Assert.AreEqual(1.1, doubleData.Data("key1"));
            Assert.AreEqual(2.2, doubleData.Data("key2"));
        }

        [Test]
        public void Test_Constructor_WithoutDictionary()
        {
            // Act
            var doubleData = new DoubleData();

            // Assert
            Assert.IsNotNull(doubleData);
            Assert.IsEmpty(doubleData.GetAllKeys());
        }

        [Test]
        public void Test_SetData_AddsNewKey()
        {
            // Arrange
            var doubleData = new DoubleData();

            // Act
            doubleData.SetData("key1", 1.1);

            // Assert
            Assert.IsTrue(doubleData.ContainsKey("key1"));
            Assert.AreEqual(1.1, doubleData.Data("key1"));
        }

        [Test]
        public void Test_SetData_UpdatesExistingKey()
        {
            // Arrange
            var doubleData = new DoubleData();
            doubleData.SetData("key1", 1.1);

            // Act
            doubleData.SetData("key1", 2.2);

            // Assert
            Assert.AreEqual(2.2, doubleData.Data("key1"));
        }

        [Test]
        public void Test_Data_ThrowsKeyNotFoundException()
        {
            // Arrange
            var doubleData = new DoubleData();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => doubleData.Data("nonexistentKey"));
        }

        [Test]
        public void Test_ContainsKey_ReturnsTrueForExistingKey()
        {
            // Arrange
            var doubleData = new DoubleData();
            doubleData.SetData("key1", 1.1);

            // Act
            bool containsKey = doubleData.ContainsKey("key1");

            // Assert
            Assert.IsTrue(containsKey);
        }

        [Test]
        public void Test_ContainsKey_ReturnsFalseForNonExistingKey()
        {
            // Arrange
            var doubleData = new DoubleData();

            // Act
            bool containsKey = doubleData.ContainsKey("nonexistentKey");

            // Assert
            Assert.IsFalse(containsKey);
        }

        [Test]
        public void Test_GetAllKeys_ReturnsAllKeys()
        {
            // Arrange
            var doubleData = new DoubleData();
            doubleData.SetData("key1", 1.1);
            doubleData.SetData("key2", 2.2);

            // Act
            var keys = doubleData.GetAllKeys();

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { "key1", "key2" }, keys);
        }

        [Test]
        public void Test_Equals_SameData_ReturnsTrue()
        {
            // Arrange
            var data1 = new DoubleData(new Dictionary<string, double> { { "key1", 1.1 }, { "key2", 2.2 } });
            var data2 = new DoubleData(new Dictionary<string, double> { { "key1", 1.1 }, { "key2", 2.2 } });

            // Act & Assert
            Assert.IsTrue(data1.Equals(data2));
        }

        [Test]
        public void Test_Equals_DifferentData_ReturnsFalse()
        {
            // Arrange
            var data1 = new DoubleData(new Dictionary<string, double> { { "key1", 1.1 }, { "key2", 2.2 } });
            var data2 = new DoubleData(new Dictionary<string, double> { { "key1", 1.1 }, { "key2", 3.3 } });

            // Act & Assert
            Assert.IsFalse(data1.Equals(data2));
        }

        [Test]
        public void Test_GetHashCode_SameData_ReturnsSameHashCode()
        {
            // Arrange
            var data1 = new DoubleData(new Dictionary<string, double> { { "key1", 1.1 }, { "key2", 2.2 } });
            var data2 = new DoubleData(new Dictionary<string, double> { { "key1", 1.1 }, { "key2", 2.2 } });

            // Act & Assert
            Assert.AreEqual(data1.GetHashCode(), data2.GetHashCode());
        }

        [Test]
        public void Test_GetHashCode_DifferentData_ReturnsDifferentHashCode()
        {
            // Arrange
            var data1 = new DoubleData(new Dictionary<string, double> { { "key1", 1.1 }, { "key2", 2.2 } });
            var data2 = new DoubleData(new Dictionary<string, double> { { "key1", 1.1 }, { "key2", 3.3 } });

            // Act & Assert
            Assert.AreNotEqual(data1.GetHashCode(), data2.GetHashCode());
        }
        [Test]
        public void Test_RemoveData_RemovesExistingKey()
        {
            // Arrange
            var doubleData = new DoubleData();
            doubleData.SetData("key1", 2.4);

            // Act
            doubleData.RemoveData("key1");

            // Assert
            Assert.IsFalse(doubleData.ContainsKey("key1"));
        }

        [Test]
        public void Test_RemoveData_ThrowsKeyNotFoundExceptionForNonExistingKey()
        {
            // Arrange
            var doubleData = new DoubleData();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => doubleData.RemoveData("nonexistentKey"));
        }

        [Test]
        public void Test_RemoveData_ThrowsArgumentExceptionForNullKey()
        {
            // Arrange
            var doubleData = new DoubleData();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => doubleData.RemoveData(null));
        }

        [Test]
        public void Test_RemoveData_ThrowsArgumentExceptionForEmptyKey()
        {
            // Arrange
            var doubleData = new DoubleData();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => doubleData.RemoveData(""));
        }
    }
}