using System.Collections.Generic;

namespace SaveSystem
{
    public interface IGameData<T>
    {
        T Data(string key);
        void SetData(string key, T value);

        void RemoveData(string key);
        bool ContainsKey(string key);
        IEnumerable<string> GetAllKeys();
    }
}