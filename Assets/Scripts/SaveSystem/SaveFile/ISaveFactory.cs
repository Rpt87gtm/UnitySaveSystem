namespace SaveSystem
{
    interface ISaveFactory
    {
        ISave Load(ISaveInfo saveInfo);
        ISave Create();
    }

}
