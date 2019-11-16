using System;
using System.Collections.Generic;
using System.Text;

namespace Michalski.ProgWiz.Interfaces
{
    public interface IDataStore
    {
        IData[] Find(IDataFilter filter);
        void Add(IData record);
        void Remove(IData record);
        void Update(IData oldRecord, IData newRecord);
    }
}
