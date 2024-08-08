

namespace SaveSystem
{
    public class SaveFileOnDisk : ISave
    {
        private IJsonConverter _converter;
        private string _path;

        public SaveFileOnDisk(IJsonConverter converter, string path)
        {
            _converter = converter;
            _path = path;
        }

        public IGameDataContainer GameDataContainer()
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
