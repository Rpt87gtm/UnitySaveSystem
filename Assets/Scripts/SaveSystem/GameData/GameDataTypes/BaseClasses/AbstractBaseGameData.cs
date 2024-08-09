using System;
using System.Collections.Generic;
using System.Linq;

namespace SaveSystem.GameData
{
    public abstract class AbstractBaseGameData<T> : IGameData<T>
    {
        protected Dictionary<string, T> _data;

        public T Data(string key)
        {
            if (_data.TryGetValue(key, out T value))
            {
                return value;
            }
            throw new KeyNotFoundException($"GameData with name '{key}' does not exist.");
        }

        public void SetData(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be null or whitespace.", nameof(key));
            }
            _data[key] = value ?? throw new ArgumentNullException(nameof(value));
        }

        public bool ContainsKey(string key)
        {
            return _data.ContainsKey(key);
        }

        public IEnumerable<string> GetAllKeys()
        {
            return _data.Keys;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            AbstractBaseGameData<T> other = (AbstractBaseGameData<T>)obj;
            return _data.Count == other._data.Count && !_data.Except(other._data).Any();
        }

        public override int GetHashCode()
        {
            // Используем хэш-код словаря _data
            int hashCode = 0;
            foreach (var kvp in _data)
            {
                hashCode ^= kvp.Key.GetHashCode();
                hashCode ^= (kvp.Value != null) ? kvp.Value.GetHashCode() : 0;
            }
            return hashCode;
        }
    }
}