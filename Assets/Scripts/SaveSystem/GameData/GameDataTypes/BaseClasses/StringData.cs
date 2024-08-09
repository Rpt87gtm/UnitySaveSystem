using System.Collections.Generic;

namespace SaveSystem.GameData
{

    public class StringData : AbstractBaseGameData<string>
    {
        public StringData(Dictionary<string, string> data)
        {
            _data = data;
        }
        public StringData() : this(new Dictionary<string, string>()) { }

    }
}
