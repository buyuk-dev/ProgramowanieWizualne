using System.Collections.Generic;
using System.Data.SQLite;
using Michalski.Models;

namespace Michalski.BusinessLogic
{
    public class DbMakerStorage : IMakerStorage
	{
		private string dburi;

		private MakerDb Read(SQLiteDataReader reader)
		{
			var name = reader.GetString(0);
			var number = reader.GetString(1);
			var address = reader.GetString(2);
			var id = reader.GetInt32(3);
			return new MakerDb(id, name, number, address);
		}

		public DbMakerStorage(string dburi)
		{
			this.dburi = dburi;
		}

		public void Delete(IMakerModel item)
		{
			var cmd = $"delete from makers where id = {item.id}";
			var connection = new SQLiteConnection(dburi);
			connection.Open();
			var sql = new SQLiteCommand(cmd, connection);
			sql.ExecuteNonQuery();
			sql.Dispose();
			connection.Close();
			connection.Dispose();
		}

		public List<IMakerModel> ReadAll()
		{
			var data = new List<IMakerModel>();
			var connection = new SQLiteConnection(dburi);
			connection.Open();
			var cmd = new SQLiteCommand("select * from makers", connection);
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

		public void Save(IMakerModel item)
		{
			string cmd;
			int maxid = GetLastInsertId();
			if (item.id < 0)
			{
				(item as MakerDb).SetId(maxid + 1);
				cmd = $"insert into makers values(" +
					  $"'{item.name}', '{item.number}', '{item.address}', {item.id}" +
					  $")";
			}
			else
			{
				cmd = $"update makers set " +
					$"name='{item.name}'," +
					$"number='{item.number}'," +
					$"address='{item.address}' " +
					$"where id = {item.id}";
			}
			var connection = new SQLiteConnection(dburi);
			connection.Open();
			var sql = new SQLiteCommand(cmd, connection);
			sql.ExecuteNonQuery();
			sql.Dispose();
			connection.Close();
			connection.Dispose();
		}

		public int GetLastInsertId()
		{
			var connection = new SQLiteConnection(dburi);
			connection.Open();
			var cmd = "select max(id) from makers";
			var sql = new SQLiteCommand(cmd, connection);
			var reader = sql.ExecuteReader();
			reader.Read();
			int id = reader.GetInt32(0);
			reader.Dispose();
			sql.Dispose();
			connection.Close();
			connection.Dispose();
			return id;
		}
	}
}
