using System.Collections.Generic;

namespace SaveSystem
{
    public interface ISaveInfoStorage
    {
        IEnumerable<ISaveInfo> SaveInfoList();
    }

}
