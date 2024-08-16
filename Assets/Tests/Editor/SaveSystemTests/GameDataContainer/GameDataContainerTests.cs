using NUnit.Framework;
using SaveSystem.GameData;
using System.Collections.Generic;

namespace SaveSystem.Tests.GameDataContainerTests
{
    [TestFixture]
    public class GameDataContainerTests : GameDataContainerTestsBase
    {
        [Test]
        public void Test_Constructor_WithDictionary()
        {
            // Arrange
            var initialData = new Dictionary<string, object> { { "key1", new BoolData() }, { "key2", new IntData() } };

            // Act
            var container = new GameDataContainer(initialData);

            // Assert
            Assert.IsTrue(container.ContainsKey("key1"));
            Assert.IsTrue(container.ContainsKey("key2"));
        }

        [Test]
        public void Test_Constructor_WithoutDictionary()
        {
            // Act
            var container = new GameDataContainer();

            // Assert
            Assert.IsNotNull(container);
            Assert.IsEmpty(container.GetAllKeys());
        }
        protected override IGameDataContainer CreateContainer()
        {
            return new GameDataContainer();
        }
        [Test]
        public void Test_Equals_SameData_ReturnsTrue()
        {
            // Arrange
            var data1 = new GameDataContainer(new Dictionary<string, object> { { "key1", new BoolData() } });
            var data2 = new GameDataContainer(new Dictionary<string, object> { { "key1", new BoolData() } });

            // Act & Assert
            Assert.IsTrue(data1.Equals(data2));
        }

        [Test]
        public void Test_Equals_DifferentData_ReturnsFalse()
        {
            // Arrange
            var data1 = new GameDataContainer(new Dictionary<string, object> { { "key1", new BoolData() } });
            var data2 = new GameDataContainer(new Dictionary<string, object> { { "key2", new BoolData() } });

            // Act & Assert
            Assert.IsFalse(data1.Equals(data2));
        }

        [Test]
        public void Test_GetHashCode_SameData_ReturnsSameHashCode()
        {
            // Arrange
            var data1 = new GameDataContainer(new Dictionary<string, object> { { "key1", new BoolData() } });
            var data2 = new GameDataContainer(new Dictionary<string, object> { { "key1", new BoolData() } });

            // Act & Assert
            Assert.AreEqual(data1.GetHashCode(), data2.GetHashCode());
        }

        [Test]
        public void Test_GetHashCode_DifferentData_ReturnsDifferentHashCode()
        {
            // Arrange
            var data1 = new GameDataContainer(new Dictionary<string, object> { { "key1", new BoolData() } });
            var data2 = new GameDataContainer(new Dictionary<string, object> { { "key2", new BoolData() } });

            // Act & Assert
            Assert.AreNotEqual(data1.GetHashCode(), data2.GetHashCode());
        }
    }
}