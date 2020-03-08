using System.Collections.Generic;
using System.IO;

namespace Michalski.Models
{
    public class MakerStorage : IObjectStorage<IMakerModel>
	{
		private readonly string dburi;

		IMakerModel TextToModel(string line)
		{
			var fields = line.Split(';');
			return new MakerModel(int.Parse(fields[0]), fields[1], fields[2], fields[3]);
		}

		string ModelToText(IMakerModel item)
		{
			return $"{item.id};{item.name};{item.number};{item.address}";
		}

		public MakerStorage(string dburi)
		{
			this.dburi = dburi + "\\makers.csv";
		}

		~MakerStorage()
		{
		}

		public void Delete(IMakerModel item)
		{
			using (StreamWriter temp = new StreamWriter("tmp.csv"))
			using(StreamReader reader = new StreamReader(dburi))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					var record = TextToModel(line);
					if (record.id != item.id)
					{
						temp.WriteLine(line);
					}
				}
			}
			File.Replace("tmp.csv", dburi, null);
		}

		public List<IMakerModel> ReadAll()
		{
			List<IMakerModel> data = new List<IMakerModel>();
			if (File.Exists(dburi) == false)
			{
				File.Create(dburi).Close();
			}

			using (StreamReader reader = new StreamReader(dburi))
			{
				string line;
				while((line = reader.ReadLine()) != null)
				{
					data.Add(TextToModel(line));
				}
			}
			return data;
		}

		public void Save(IMakerModel item)
		{
			using (StreamWriter temp = new StreamWriter("tmp.csv"))
			using (StreamReader reader = new StreamReader(dburi))
			{
				string line;
				bool update = false;
				while ((line = reader.ReadLine()) != null)
				{
					var record = TextToModel(line);
					if (record.id == item.id)
					{
						update = true;
						temp.WriteLine(ModelToText(item));
					}
					else
					{
						temp.WriteLine(line);
					}
				}
				if (update == false)
				{
					(item as MakerModel).SetId(GetLastInsertId() + 1);
					temp.WriteLine(ModelToText(item));
				}
			}
			File.Replace("tmp.csv", dburi, null);
		}

		public int GetLastInsertId()
		{
			var data = ReadAll();
			if (data.Count == 0) return 0;
			return data[data.Count - 1].id;
		}

		public IMakerModel CreateNewItem()
		{
			return new MakerModel();
		}
	}
}
