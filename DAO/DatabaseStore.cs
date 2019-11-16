using System;
using System.Collections.Generic;
using System.Text;

namespace Michalski.ProgWiz.DAO
{
    using Interfaces;

    class DatabaseStore : IDataStore
    {
        public void Add(IData record)
        {
            throw new NotImplementedException();
        }

        public IData[] Find(IDataFilter filter)
        {
            throw new NotImplementedException();
        }

        public void Remove(IData record)
        {
            throw new NotImplementedException();
        }

        public void Update(IData oldRecord, IData newRecord)
        {
            throw new NotImplementedException();
        }
    }
}
