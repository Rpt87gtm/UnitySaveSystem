using System.Collections.Generic;

namespace SaveSystem.GameData
{
    public class IntData : AbstractBaseGameData<int>
    {
        public IntData(Dictionary<string, int> data)
        {
            _data = data;
        }
        public IntData() : this(new Dictionary<string, int>()) { }

    }
}
