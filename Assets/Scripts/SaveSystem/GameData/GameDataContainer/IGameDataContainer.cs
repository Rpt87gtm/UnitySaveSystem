using System.Collections.Generic;

namespace SaveSystem
{
    public interface IGameDataContainer
    {
        void AddGameData<T>(string key, IGameData<T> gameData);
        void RemoveGameData<T>(string key);
        bool ContainsKey(string key);
        IGameData<T> GameData<T>(string key);
        IEnumerable<string> GetAllKeys();

    }

}
