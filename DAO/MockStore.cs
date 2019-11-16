using System;
using System.Collections.Generic;
using System.Text;

namespace Michalski.ProgWiz.DAO
{
    using Interfaces;

    class MockStore : IDataStore
    {
        public void Add(IData record)
        {
            Console.WriteLine("Adding new data record.");
            database.Add(record);
        }

        public IData[] Find(IDataFilter filter)
        {
            Console.WriteLine("Searching for data records.");
            Console.WriteLine("Filtering not implemented.");
            return database.ToArray();
        }

        public void Remove(IData record)
        {
            Console.WriteLine("Removing data record.");
            database.Remove(record);
        }

        public void Update(IData oldRecord, IData newRecord)
        {
            Console.WriteLine("Updating data record.");
            database.Remove(oldRecord);
            database.Add(newRecord);
        }

        private List<IData> database;
    }
}
