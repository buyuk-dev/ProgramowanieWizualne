using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.ComponentModel;

namespace Michalski
{
	public enum ViolinState
	{
		Good,
		Average,
		Bad
	}

	public interface IViolinModel : INotifyPropertyChanged
	{
		string name { get; set; }
		string maker { get; set; }
		uint year { get; set; }
		uint price { get; set; }
		ViolinState state { get; set; }
	}

	public class ViolinDb : IViolinModel
	{
		public ViolinDb()
		{
		}

		public ViolinDb(string maker, string name, uint year, uint price, string state)
		{
			this.name = name;
			this.maker = maker;
			this.year = year;
			this.price = price;
			Enum.TryParse(state, true, out ViolinState tmpState);
			this.state = tmpState;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public override string ToString()
		{
			return $"{name} made by {maker} in {year} is in {state.ToString()} condition - ${price}";
		}

		private string _name;
		public string name
		{
			get { return _name; }
			set
			{
				_name = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("name"));
			}
		}

		private string _maker;
		public string maker
		{
			get
			{
				return _maker;
			}
			set
			{
				_maker = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("maker"));
			}
		}

		private uint _price;
		public uint price
		{
			get { return _price; }
			set
			{
				_price = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("price"));
			}
		}

		private uint _year;
		public uint year
		{
			get
			{
				return _year;
			}
			set
			{
				_year = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("year"));
			}
		}

		private ViolinState _state;
		public ViolinState state
		{
			get { return _state; }
			set
			{
				_state = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("state"));
			}
		}

		public static ViolinDb Read(SQLiteDataReader reader)
		{
			var name = reader.GetString(0);
			var maker = reader.GetString(1);
			var year = reader.GetInt32(2);
			var price = reader.GetInt32(3);
			var state = reader.GetString(4);
			return new ViolinDb(maker, name, (uint)year, (uint)price, state);
		}

		public static List<IViolinModel> ReadAll()
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

		public void Upsert()
		{
			var connection = new SQLiteConnection(dburi);
			connection.Open();

			// todo: is this query correctly replacing existing items and inserting new items?
			var cmd = $"insert or replace into violins values (" +
				      $"'{name}', '{maker}', {year}, {price}, '{state}'" +
					  $")";
			Console.WriteLine($"upsert cmd: {cmd}");
			var sql = new SQLiteCommand(cmd, connection);
			sql.ExecuteNonQuery();
			sql.Dispose();
			connection.Close();
			connection.Dispose();
		}

		public void Delete()
		{
			var connection = new SQLiteConnection(dburi);
			connection.Open();
			var cmd = $"delete from violins where name = '{name}'";
			Console.WriteLine($"delete cmd: {cmd}");
			new SQLiteCommand(cmd, connection).ExecuteNonQuery();
			connection.Close();
			connection.Dispose();
		}

		private static string dburi = $"URI={Properties.Settings.Default.DataSourceUri}";
	}
}
