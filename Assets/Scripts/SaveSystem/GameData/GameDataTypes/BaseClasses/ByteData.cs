using System.Collections.Generic;

namespace SaveSystem.GameData
{

    public class ByteData : AbstractBaseGameData<byte>
    {
        public ByteData(Dictionary<string, byte> data)
        {
            _data = data;
        }
        public ByteData() : this(new Dictionary<string, byte>()) { }

    }
}
