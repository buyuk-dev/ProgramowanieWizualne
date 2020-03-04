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

	public class Violin : INotifyPropertyChanged
	{
		public Violin() { }
		public Violin(String maker, String name, uint year, uint price, string state)
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
				Console.WriteLine($"setting violin name to: {value}");
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
	}
}
