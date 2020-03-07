using System;
using System.Collections.Generic;
using System.Text;

namespace Michalski.Models
{
    public interface IObjectStorage<ObjectType>
    {
        void Delete(ObjectType item);
        void Save(ObjectType item);
        List<ObjectType> ReadAll();
        ObjectType CreateNewItem();
    }
}
