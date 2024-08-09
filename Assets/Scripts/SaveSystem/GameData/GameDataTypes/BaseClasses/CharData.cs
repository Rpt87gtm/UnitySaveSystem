using System.Collections.Generic;

namespace SaveSystem.GameData
{

    public class CharData : AbstractBaseGameData<char>
    {
        public CharData(Dictionary<string, char> data)
        {
            _data = data;
        }
        public CharData() : this(new Dictionary<string, char>()) { }

    }
}
