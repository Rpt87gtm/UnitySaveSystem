namespace SaveSystem
{
    interface IGameDataContainer
    {
        void AddGameData<T>(string name, IGameData<T> gameData);
        void RemoveGameData<T>(string name);
        IGameData<T> GameData<T>(string name);
    }

}
