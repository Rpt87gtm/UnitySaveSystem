using System.Collections.Generic;

namespace SaveSystem.GameData
{

    public class DoubleData : AbstractBaseGameData<double>
    {
        public DoubleData(Dictionary<string, double> data)
        {
            _data = data;
        }
        public DoubleData() : this(new Dictionary<string, double>()) { }

    }
}
