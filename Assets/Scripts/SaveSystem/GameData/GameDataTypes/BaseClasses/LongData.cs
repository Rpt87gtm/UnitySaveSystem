using System.Collections.Generic;

namespace SaveSystem.GameData
{

    public class LongData : AbstractBaseGameData<long>
    {
        public LongData(Dictionary<string, long> data)
        {
            _data = data;
        }
        public LongData() : this(new Dictionary<string, long>()) { }

    }
}
