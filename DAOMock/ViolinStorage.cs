using System.Collections.Generic;

namespace Michalski.Models
{
	public class ViolinStorage : IObjectStorage<IViolinModel>
	{
		List<IViolinModel> _data;

		public ViolinStorage(string dburi)
		{
			_data = new List<IViolinModel>{
				new ViolinModel(0, "Henglewski", "Julia", 1995, 15000, "good"),
				new ViolinModel(1, "Stradivari", "The Molitor", 1697, 2700000, "good"),
				new ViolinModel(2, "Stradivari", "The Dolphin", 1714, 4000000, "average"),
				new ViolinModel(3, "Guarneri", "The Lord Wilton", 1742, 4300000, "bad")
			};
		}

		public void Delete(IViolinModel item)
		{
			_data.Remove(item);
		}

		public List<IViolinModel> ReadAll()
		{
			return _data;
		}

		public void Save(IViolinModel item)
		{
			var id = _data.FindIndex((x) => { return item.id == x.id; });
			if (id < 0)
			{
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

		public IViolinModel CreateNewItem()
		{
			return new ViolinModel();
		}
	}
}
