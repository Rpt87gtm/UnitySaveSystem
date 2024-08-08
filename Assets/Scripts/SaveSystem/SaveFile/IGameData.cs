namespace SaveSystem
{
    public interface IGameData<T>
    {
        T Data(string name);
    }
}