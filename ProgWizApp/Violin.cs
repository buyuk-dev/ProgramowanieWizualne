using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michalski
{
	public enum ViolinState
	{
		Good,
		Average,
		Bad
	}

	public class Violin
	{
		public Violin(String maker, String name, uint year, uint price, string state)
		{
			this.name = name;
			this.maker = maker;
			this.year = year;
			this.price = price;
			ViolinState tmpState;
			Enum.TryParse(state, true, out tmpState);
			this.state = tmpState;
		}

		public override string ToString()
		{
			return $"{name} made by {maker} in {year} is in {state.ToString()} condition - ${price}";
		}

		public string name { get; set; }
		public string maker { get; set; }
		public uint price { get; set; }
		public uint year { get; set; }
		public ViolinState state { get; set; }
	}
}
