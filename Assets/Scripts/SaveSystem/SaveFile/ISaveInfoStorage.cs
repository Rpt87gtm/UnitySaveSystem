using System.Collections.Generic;

namespace SaveSystem
{
    interface ISaveInfoStorage
    {
        IEnumerable<ISaveInfo> SaveInfoList();
    }

}
