using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SQLite;

using System.Windows.Input;

namespace Michalski
{
    class MainViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private BindingList<IViolinModel> _violins;
        public BindingList<IViolinModel> Violins
        {
            get { return _violins; }
            set
            {
                _violins = value;
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
            _violins = new BindingList<IViolinModel>();
            foreach (var v in ViolinDb.ReadAll()) {
                _violins.Add(v);
            }
            Violins.ListChanged += new ListChangedEventHandler(ViolinsChangedHandler);
            Violins.AllowNew = true;
            Violins.AddingNew += (sender, e) => e.NewObject = new ViolinDb();
            Violins.AllowEdit = true;

            /*
            var connection = new SQLiteConnection(dburi);
            connection.Open();

            var cmd = new SQLiteCommand("select * from makers", connection);
            var reader = cmd.ExecuteReader();
            
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
            */
        }

        private void ViolinsChangedHandler(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                // todo: doesn't work when violin name was changed (primary key).
                (Violins[e.NewIndex] as ViolinDb).Delete();
                (Violins[e.NewIndex] as ViolinDb).Upsert();
            }
            else if (e.ListChangedType == ListChangedType.ItemDeleted) {
                // todo: How to handle deleting item from DB?
            }
        }

        ~MainViewModel()
        {
        }
    }
}
