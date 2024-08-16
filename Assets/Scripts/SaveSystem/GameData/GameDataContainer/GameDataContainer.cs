using System;
using System.Collections.Generic;

namespace SaveSystem.GameData
{
    public class GameDataContainer : IGameDataContainer
    {
        private readonly Dictionary<string, (Type, object)> _gameDatas;

        public GameDataContainer(Dictionary<string, (Type, object)> dictionary)
        {
            _gameDatas = dictionary;
        }

        public GameDataContainer() : this(new Dictionary<string, (Type, object)>()) { }

        public GameDataContainer(Dictionary<string, object> gameDatas)
            : this(ConvertToTypeWithObject(gameDatas)) { }

        private static Dictionary<string, (Type, object)> ConvertToTypeWithObject(Dictionary<string, object> gameDatas)
        {
            Dictionary<string, (Type, object)> result = new();
            foreach (var gameData in gameDatas)
            {
                result[gameData.Key] = (gameData.Value.GetType(), gameData.Value);
            }
            return result;
        }

        public void AddGameData<T>(string key, IGameData<T> gameData)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Game data name cannot be null or whitespace.", nameof(key));
            }

            if (gameData == null)
            {
                throw new ArgumentNullException(nameof(gameData));
            }

            _gameDatas[key] = (gameData.GetType(), gameData);
        }

        public IGameData<T> GameData<T>(string key)
        {
            if (_gameDatas.TryGetValue(key, out var data))
            {
                if (data.Item2 is IGameData<T> gameData)
                {
                    return gameData;
                }
                throw new InvalidCastException($"Stored data cannot be cast {data.Item2} to the expected type {typeof(IGameData<T>)}.");
            }
            throw new KeyNotFoundException($"GameData with name '{key}' does not exist.");
        }

        public void RemoveGameData<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key) || key == "")
            {
                throw new ArgumentException("Key cannot be null or whitespace.", nameof(key));
            }

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

        public override bool Equals(object obj)
        {
            if (obj is not GameDataContainer other)
            {
                return false;
            }

            if (_gameDatas.Count != other._gameDatas.Count)
            {
                return false;
            }

            foreach (var pair in _gameDatas)
            {
                if (!other._gameDatas.TryGetValue(pair.Key, out var otherPair) ||
                    pair.Value.Item1 != otherPair.Item1 ||
                    !Equals(pair.Value.Item2, otherPair.Item2))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 17;

            foreach (var pair in _gameDatas)
            {
                hash = hash * 31 + pair.Key.GetHashCode();
                hash = hash * 31 + pair.Value.Item1.GetHashCode();
                if (pair.Value.Item2 != null)
                {
                    hash = hash * 31 + pair.Value.Item2.GetHashCode();
                }
            }

            return hash;
        }

        public override string ToString()
        {
            IJsonConverter jsonConverter = new NewtonSoftJsonConverter();
            return jsonConverter.ToJson(_gameDatas);
        }
    }
}