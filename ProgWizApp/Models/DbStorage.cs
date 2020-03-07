﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Michalski
{
	public interface IViolinStorage
	{
		void Delete(IViolinModel item);
		void Save(IViolinModel item);
		List<IViolinModel> ReadAll();
	}

	public interface IMakerStorage
	{
		void Delete(IMakerModel item);
		void Save(IMakerModel item);
		List<IMakerModel> ReadAll();
	}

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
			Console.WriteLine(cmd);

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
			Console.WriteLine("select * from makers");
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

			Console.WriteLine(cmd);
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
			Console.WriteLine(cmd);
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
			var cmd = $"delete from violins where id = {item.id}";
			Console.WriteLine(cmd);

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
			Console.WriteLine("select * from violins");
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
			string cmd;
			int maxid = GetLastInsertId();
			if (item.id < 0)
			{
				(item as ViolinDb).SetId(maxid + 1);
				cmd = $"insert into violins values(" +
					  $"'{item.name}', '{item.maker}', {item.year}, {item.price}, '{item.state}', {item.id}" +
					  $")";
			}
			else
			{
				cmd = $"update violins set " +
					$"name='{item.name}'," +
					$"maker='{item.maker}'," +
					$"state='{item.state}'," +
					$"year={item.year}," +
					$"price={item.price} " +
					$"where id = {item.id}";
			}

			Console.WriteLine(cmd);
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
			var cmd = "select max(id) from violins";
			Console.WriteLine(cmd);
			var sql = new SQLiteCommand(cmd, connection);
			var reader = sql.ExecuteReader();
			reader.Read();
			int id = reader.GetInt32(0);
			reader.Dispose();
			sql.Dispose();
			connection.Close();
			connection.Dispose();
			Console.WriteLine($"Last insert id: {id}");
			return id;
		}
	}
}
