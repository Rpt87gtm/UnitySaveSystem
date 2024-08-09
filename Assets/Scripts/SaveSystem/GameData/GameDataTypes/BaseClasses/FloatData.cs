using System.Collections.Generic;

namespace SaveSystem.GameData
{

    public class FloatData : AbstractBaseGameData<float>
    {
        public FloatData(Dictionary<string, float> data)
        {
            _data = data;
        }
        public FloatData() : this(new Dictionary<string, float>()) { }

    }
}
