using System;
using System.Collections.Generic;

namespace SaveSystem.GameData
{
    public class GameDataContainer : IGameDataContainer
    {
        private readonly Dictionary<string, object> _gameDatas;

        public GameDataContainer(Dictionary<string, object> dictionary) { 
            _gameDatas = dictionary;
        }

        public GameDataContainer() : this(new Dictionary<string, object>()) { }

        public void AddGameData<T>(string key, IGameData<T> gameData)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Game data name cannot be null or whitespace.", nameof(key));
            }

            _gameDatas[key] = gameData ?? throw new ArgumentNullException(nameof(gameData)); ;
        }

        public IGameData<T> GameData<T>(string key)
        {
            if (_gameDatas.TryGetValue(key, out object data))
            {
                if (data is IGameData<T> gameData)
                {
                    return gameData;
                }
                throw new InvalidCastException($"Stored data cannot be cast to the expected type {typeof(T)}.");

            }
            throw new KeyNotFoundException($"GameData with name '{key}' does not exist.");
        }

        public void RemoveGameData<T>(string key)
        {
            if (!_gameDatas.Remove(key))
            {
                throw new KeyNotFoundException($"GameData with name '{key}' does not exist.");
            }

        }
      
        public IEnumerable<string> GetAllKeys()
        {
            return _gameDatas.Keys;
        }

        public bool ContainsKey(string key)
        {
            return _gameDatas.ContainsKey(key);
        }
    }
}
