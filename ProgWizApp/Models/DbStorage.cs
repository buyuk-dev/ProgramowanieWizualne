using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michalski
{
	public interface IViolinStorage
	{
		void Delete(IViolinModel item);
		void Save(IViolinModel item);
		List<IViolinModel> ReadAll();
	}

	public class DbViolinStorage : IViolinStorage
	{
		private string dburi;

		private ViolinDb Read(SQLiteDataReader reader)
		{
			var name = reader.GetString(0);
			var maker = reader.GetString(1);
			var year = reader.GetInt32(2);
			var price = reader.GetInt32(3);
			var state = reader.GetString(4);
			var id = reader.GetInt32(5);
			return new ViolinDb(id, maker, name, (uint)year, (uint)price, state);
		}

		public DbViolinStorage(string dburi)
		{
			this.dburi = dburi;
		}

		public void Delete(IViolinModel item)
		{
			var cmd = $"delete from violins where name = '{item.name}'";
			var connection = new SQLiteConnection(dburi);
			connection.Open();

			var sql = new SQLiteCommand(cmd, connection);
			sql.ExecuteNonQuery();
			sql.Dispose();

			connection.Close();
			connection.Dispose();
		}

		public List<IViolinModel> ReadAll()
		{
			var data = new List<IViolinModel>();
			var connection = new SQLiteConnection(dburi);
			connection.Open();

			var cmd = new SQLiteCommand("select * from violins", connection);
			var reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				data.Add(Read(reader));
			}

			cmd.Dispose();
			reader.Dispose();

			connection.Close();
			connection.Dispose();
			return data;
		}

		public void Save(IViolinModel item)
		{
			var connection = new SQLiteConnection(dburi);
			connection.Open();

			// todo: is this query correctly replacing existing items and inserting new items?
			var cmd = $"insert or replace into violins values (" +
					  $"{item.id} '{item.name}', '{item.maker}', {item.year}, {item.price}, '{item.state}'" +
					  $")";

			var sql = new SQLiteCommand(cmd, connection);
			sql.ExecuteNonQuery();
			sql.Dispose();
			connection.Close();
			connection.Dispose();
		}
	}
}
