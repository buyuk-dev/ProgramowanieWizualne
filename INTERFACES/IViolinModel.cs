using System.ComponentModel;

namespace Michalski.Models
{
    public interface IViolinModel : INotifyPropertyChanged
	{
		int id { get; }
		string name { get; set; }
		string maker { get; set; }
		uint year { get; set; }
		uint price { get; set; }
		ViolinState state { get; set; }
	}
}
