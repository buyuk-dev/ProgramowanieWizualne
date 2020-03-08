using System;
using System.Collections.Generic;
using System.IO;

namespace Michalski.Models
{
	public class ViolinStorage : IObjectStorage<IViolinModel>
	{
		private readonly string dburi;

		IViolinModel TextToModel(string line)
		{
			var fields = line.Split(';');

			return new ViolinModel(int.Parse(fields[0]), fields[2], fields[1], uint.Parse(fields[3]), uint.Parse(fields[4]), fields[5]);
		}

		string ModelToText(IViolinModel item)
		{
			return $"{item.id};{item.name};{item.maker};{item.year};{item.price};{item.state}";
		}

		public ViolinStorage(string dburi)
		{
			this.dburi = dburi + "\\violin.csv";
		}

		public void Delete(IViolinModel item)
		{
			using (StreamWriter temp = new StreamWriter("tmp.csv"))
			using (StreamReader reader = new StreamReader(dburi))
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

		public List<IViolinModel> ReadAll()
		{
			var data = new List<IViolinModel>();
			if (File.Exists(dburi) == false)
			{
				File.Create(dburi).Close();
			}
			using (StreamReader reader = new StreamReader(dburi))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					data.Add(TextToModel(line));
				}
			}
			return data;
		}

		public void Save(IViolinModel item)
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
					(item as ViolinModel).SetId(GetLastInsertId() + 1);
					temp.WriteLine(ModelToText(item));
				}
			}
			File.Replace("tmp.csv", dburi, null);
		}

		public int GetLastInsertId()
		{
			var data = ReadAll();
			if (data.Count == 0) return 0;
			else return data[data.Count - 1].id;
		}

		public IViolinModel CreateNewItem()
		{
			return new ViolinModel();
		}
	}
}
