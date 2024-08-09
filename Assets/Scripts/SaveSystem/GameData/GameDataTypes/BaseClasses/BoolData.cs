using System.Collections.Generic;

namespace SaveSystem.GameData
{

    public class BoolData : AbstractBaseGameData<bool>
    {
        public BoolData(Dictionary<string, bool> data)
        {
            _data = data;
        }
        public BoolData() : this(new Dictionary<string, bool>()) { }

    }
}
