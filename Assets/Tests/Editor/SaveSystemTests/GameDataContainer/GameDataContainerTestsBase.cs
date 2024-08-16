using NUnit.Framework;
using SaveSystem.GameData;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SaveSystem.Tests.GameDataContainerTests
{
    [TestFixture]
    public abstract class GameDataContainerTestsBase
    {
        protected abstract IGameDataContainer CreateContainer();


        [Test]
        public void Test_AddGameData_AddsNewKey()
        {
            // Arrange
            var container = CreateContainer();
            var gameData = new BoolData();

            // Act
            container.AddGameData("key1", gameData);

            // Assert
            Assert.IsTrue(container.ContainsKey("key1"));
            Assert.AreSame(gameData, container.GameData<bool>("key1"));
        }

        [Test]
        public void Test_AddGameData_ThrowsArgumentExceptionForNullKey()
        {
            // Arrange
            var container = CreateContainer();
            var gameData = new BoolData();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => container.AddGameData(null, gameData));
        }

        [Test]
        public void Test_AddGameData_ThrowsArgumentNullExceptionForNullGameData()
        {
            // Arrange
            var container = CreateContainer();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => container.AddGameData<BoolData>("key1", null));
        }

        [Test]
        public void Test_GameData_ReturnsCorrectData()
        {
            // Arrange
            var container = CreateContainer();
            var gameData = new BoolData();
            container.AddGameData("key1", gameData);

            // Act
            var retrievedData = container.GameData<bool>("key1");

            // Assert
            Assert.AreSame(gameData, retrievedData);
        }

        [Test]
        public void Test_GameData_ThrowsKeyNotFoundException()
        {
            // Arrange
            var container = CreateContainer();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => container.GameData<bool>("nonexistentKey"));
        }

        [Test]
        public void Test_GameData_ThrowsInvalidCastException()
        {
            // Arrange
            var container = CreateContainer();
            container.AddGameData("key1", new BoolData());

            // Act & Assert
            Assert.Throws<InvalidCastException>(() => container.GameData<int>("key1"));
        }

        [Test]
        public void Test_RemoveGameData_RemovesExistingKey()
        {
            // Arrange
            var container = CreateContainer();
            container.AddGameData("key1", new BoolData());

            // Act
            container.RemoveGameData<bool>("key1");

            // Assert
            Assert.IsFalse(container.ContainsKey("key1"));
        }

        [Test]
        public void Test_RemoveGameData_ThrowsKeyNotFoundException()
        {
            // Arrange
            var container = CreateContainer();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => container.RemoveGameData<bool>("nonexistentKey"));
        }

        [Test]
        public void Test_GetAllKeys_ReturnsAllKeys()
        {
            // Arrange
            var container = CreateContainer();
            container.AddGameData("key1", new BoolData());
            container.AddGameData("key2", new BoolData());

            // Act
            var keys = container.GetAllKeys();

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { "key1", "key2" }, keys);
        }

        [Test]
        public void Test_ContainsKey_ReturnsTrueForExistingKey()
        {
            // Arrange
            var container = CreateContainer();
            container.AddGameData("key1", new BoolData());

            // Act
            bool containsKey = container.ContainsKey("key1");

            // Assert
            Assert.IsTrue(containsKey);
        }

        [Test]
        public void Test_ContainsKey_ReturnsFalseForNonExistingKey()
        {
            // Arrange
            var container = CreateContainer();

            // Act
            bool containsKey = container.ContainsKey("nonexistentKey");

            // Assert
            Assert.IsFalse(containsKey);
        }
        [Test]
        public void Test_RemoveGameData_ThrowsArgumentExceptionForNullKey()
        {
            // Arrange
            var container = CreateContainer();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => container.RemoveGameData<bool>(null));
        }
        [Test]
        public void Test_AddGameData_OverwritesExistingKey()
        {
            // Arrange
            var container = CreateContainer();
            var gameData1 = new BoolData();
            var gameData2 = new BoolData();

            // Act
            container.AddGameData("key1", gameData1);
            container.AddGameData("key1", gameData2);

            // Assert
            Assert.AreSame(gameData2, container.GameData<bool>("key1"));
        }

        [Test]
        public void Test_RemoveGameData_ThrowsArgumentExceptionForEmptyKey()
        {
            // Arrange
            var container = CreateContainer();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => container.RemoveGameData<bool>(""));
        }

        [Test]
        public void Test_Performance_AddAndRetrieveLargeNumberOfGameData()
        {
            // Arrange
            var container = CreateContainer();
            const int numberOfEntries = 10000;
            var stopwatch = new Stopwatch();

            // Act
            stopwatch.Start();
            for (int i = 0; i < numberOfEntries; i++)
            {
                container.AddGameData($"key{i}", new BoolData());
            }
            stopwatch.Stop();
            var addTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();
            for (int i = 0; i < numberOfEntries; i++)
            {
                _ = container.GameData<bool>($"key{i}");
            }
            stopwatch.Stop();
            var retrieveTime = stopwatch.ElapsedMilliseconds;

            // Assert
            Assert.LessOrEqual(addTime, 1000, "Adding 10000 entries took too long.");
            Assert.LessOrEqual(retrieveTime, 1000, "Retrieving 10000 entries took too long.");
        }
    }
}