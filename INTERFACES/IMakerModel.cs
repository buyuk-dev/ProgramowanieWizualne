using System.ComponentModel;

namespace Michalski.Models
{
    public interface IMakerModel : INotifyPropertyChanged
    {
        int id { get; }
        string name { get; set; }
        string number { get; set; }
        string address { get; set; }
    }
}
