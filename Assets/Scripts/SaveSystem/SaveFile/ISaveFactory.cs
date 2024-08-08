namespace SaveSystem
{
    public interface ISaveFactory
    {
        ISave Load(ISaveInfo saveInfo);
        ISave Create();
    }

}
