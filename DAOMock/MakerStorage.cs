using System.Collections.Generic;

namespace Michalski.Models
{
    public class MakerStorage : IObjectStorage<IMakerModel>
	{
		private List<IMakerModel> _data;

		public MakerStorage(string dburi)
		{
			_data = new List<IMakerModel>{
				new MakerModel(1, "Stradivari", "000-000-000", "Cremona"),
				new MakerModel(2, "Henglewski", "111-111-111", "Poznań"),
				new MakerModel(3, "Guarneri", "222-222-222", "Cremona"),
				new MakerModel(4, "Kowalski", "333-333-333", "Świebodzim"),
				new MakerModel(5, "Nowak", "444-444-444", "Nekla"),
				new MakerModel(6, "Smith", "555-555-555", "New York")
			};
		}

		public void Delete(IMakerModel item)
		{
			_data.Remove(item);
		}

		public List<IMakerModel> ReadAll()
		{
			return _data;
		}

		public void Save(IMakerModel item)
		{
			var id = _data.FindIndex((x) => { return item.id == x.id; });
			if (id < 0)
			{
				(item as MakerModel).SetId(GetLastInsertId() + 1);
				_data.Add(item);
			}
			else
			{
				_data[id] = item;
			}
		}

		public int GetLastInsertId()
		{
			return _data.Count - 1;
		}

		public IMakerModel CreateNewItem()
		{
			return new MakerModel();
		}
	}
}
