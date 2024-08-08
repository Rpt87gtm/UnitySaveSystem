namespace SaveSystem
{
    interface IGameData<T>
    {
        T Data(string name);
    }
}