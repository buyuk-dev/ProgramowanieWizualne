using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Michalski
{
    public class Table<T>
    {
        public List<T> data
        {
            get;
        }

        public void insert(T record)
        {

        }
    }
}
