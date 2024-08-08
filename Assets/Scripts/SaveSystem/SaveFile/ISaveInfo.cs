using System.Collections.Generic;

namespace SaveSystem
{
    public interface ISaveInfo
    {
        Dictionary<string, string> Info();
        void Update(string key, string value);

        SaveInfoType Type();
    }


    public enum SaveInfoType
    {
        Disk
    }
}
