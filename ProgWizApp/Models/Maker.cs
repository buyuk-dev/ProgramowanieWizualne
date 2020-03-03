using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michalski
{
    public class Maker
    {
        public Maker(string name, string number, string address)
        {
            this.name = name;
            this.number = number;
            this.address = address;
        }

        public override string ToString()
        {
            return $"Maker: {name} #{number} @{address}";
        }

        public string name { get; set; }
        public string number { get; set; }
        public string address { get; set; }
    }

}
