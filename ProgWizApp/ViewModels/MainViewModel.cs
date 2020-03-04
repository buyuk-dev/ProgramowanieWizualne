using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SQLite;
using System.Threading;
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

        private string dburi = $"URI={Properties.Settings.Default.DataSourceUri}"; 

        public MainViewModel()
        {
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
            connection.Close();
            connection.Dispose();

            Violins.CollectionChanged += new NotifyCollectionChangedEventHandler(ViolinsChangedHandler);
        }

        private void ViolinsChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            var connection = new SQLiteConnection(dburi);
            connection.Open();
            string cmd = null;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    var violin = e.NewItems[0] as Violin;
                    cmd = $"insert into violins values (" +
                            $"'{violin.name}', '{violin.maker}', {violin.year}, {violin.price}, '{violin.state}'" +
                            $")";
                }
                break;
                case NotifyCollectionChangedAction.Remove:
                {
                    var violin = e.OldItems[0] as Violin;
                    cmd = $"delete from violins where name = '{violin.name}'";
                }
                break;
                case NotifyCollectionChangedAction.Replace:
                {
                    var oldViolin = e.OldItems[0] as Violin;
                    var newViolin = e.NewItems[0] as Violin;
                    cmd = $"update violins" +
                            $"set " +
                            $"name = '{newViolin.name}', " +
                            $"maker = '{newViolin.maker}', " +
                            $"year = {newViolin.year}, " +
                            $"price = {newViolin.price}, " +
                            $"state = {newViolin.state}" +
                            $"where " +
                            $"name = '{oldViolin.name}'";
                }
                break;
            }

            new SQLiteCommand(cmd, connection).ExecuteNonQuery();
            connection.Close();
            connection.Dispose();
        }

        ~MainViewModel()
        {
        }
    }
}
