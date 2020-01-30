using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michalski
{
	public class Violin
	{
		public Violin(String maker, String name, uint year, uint price)
		{
			this.name = name;
			this.maker = maker;
			this.year = year;
			this.price = price;
		}

		public string toString()
		{
			return $"{name} made by {maker} in {year} - ${price}";
		}

		public string name { get; set; }
		public string maker { get; set; }
		public uint price { get; set; }
		public uint year { get; set; }
	}
}
