using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Windows.Input;

namespace Michalski
{
    class MainViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Violin> _violins;
        public ObservableCollection<Violin> Violins
        {
            get { return _violins; }
            set
            {
                _violins = value;
                Console.WriteLine("Setting Violins in MainViewModel.");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Violins"));
            }
        }

        private ObservableCollection<Maker> _makers;
        public ObservableCollection<Maker> Makers
        {
            get { return _makers; }
            set {
                _makers = value;  Console.WriteLine("Setting Makers in MainViewModel.");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Makers"));
            }
        }

        private ICommand newViolinCmd;
        public ICommand NewViolinCmd
        {
            get {
                if (newViolinCmd == null) newViolinCmd = new Commands.NewViolinCmd();
                return newViolinCmd;
            }
            set {
                newViolinCmd = value;
            }
        }

        public ICommand newMakerCmd;
        public ICommand NewMakerCmd
        {
            get
            {
                if (newMakerCmd == null) newMakerCmd = new Commands.NewMakerCmd();
                return newMakerCmd;
            }
            set
            {
                newMakerCmd = value;
            }
        }

        public MainViewModel()
        {
            string dburi = $"URI={Properties.Settings.Default.DataSourceUri}";
            Console.WriteLine(dburi);

            var connection = new SQLiteConnection(dburi);
            connection.Open();

            var cmd = new SQLiteCommand("select * from violins", connection);
            var reader = cmd.ExecuteReader();
            _violins = new ObservableCollection<Violin>();
            while (reader.Read())
            {
                var name = reader.GetString(0);
                var maker = reader.GetString(1);
                var year = reader.GetInt32(2);
                var price = reader.GetInt32(3);
                var state = reader.GetString(4);
                Violins.Add(new Violin(maker, name, (uint)year, (uint)price, state));
            }
            cmd.Dispose();
            reader.Dispose();

            cmd = new SQLiteCommand("select * from makers", connection);
            _makers = new ObservableCollection<Maker>();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var name = reader.GetString(0);
                var number = reader.GetString(1);
                var address = reader.GetString(2);
                Makers.Add(new Maker(name, number, address));
            }
            cmd.Dispose();
            reader.Dispose();

            connection.Dispose();
        }

        ~MainViewModel()
        {
            Console.WriteLine("MainViewModel Destructor.");
        }
    }
}
