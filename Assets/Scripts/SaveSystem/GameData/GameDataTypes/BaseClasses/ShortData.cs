using System.Collections.Generic;

namespace SaveSystem.GameData
{

    public class ShortData : AbstractBaseGameData<short>
    {
        public ShortData(Dictionary<string, short> data)
        {
            _data = data;
        }
        public ShortData() : this(new Dictionary<string, short>()) { }

    }
}
