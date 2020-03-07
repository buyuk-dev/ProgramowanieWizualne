using System.ComponentModel;

namespace Michalski
{
    public interface IMakerModel : INotifyPropertyChanged
    {
        int id { get; }
        string name { get; set; }
        string number { get; set; }
        string address { get; set; }
    }

    public class MakerDb : IMakerModel
    {
        public MakerDb()
        {
            id = -1;
        }

        public MakerDb(int id, string name, string number, string address)
        {
            this.id = id;
            this.name = name;
            this.number = number;
            this.address = address;
        }

        public override string ToString()
        {
            return $"Maker: {name} #{number} @{address}";
        }

        public void SetId(int id)
        {
            if (this.id < 0) this.id = id;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _id;
        public int id
        {
            get
            {
                return _id;
            }
            private set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("id"));
            }
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

        private string _number;
        public string number
        {
            get { return _number; }
            set
            {
                _number = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("number"));
            }
        }

        private string _address;
        public string address
        {
            get { return _address; }
            set
            {
                _address = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("address"));
            }
        }
    }

}
