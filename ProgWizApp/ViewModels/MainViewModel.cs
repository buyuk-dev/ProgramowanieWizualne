using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Michalski
{

    class ExtBindingList<T> : BindingList<T>
    {
        protected override void RemoveItem(int index)
        {
            T deletedItem = this.Items[index];
            if (BeforeChange != null)
            {
                BeforeChange(deletedItem);
            }
            base.RemoveItem(index);
        }

        public delegate void ItemChangedDelegate(T item);
        public event ItemChangedDelegate BeforeChange;
    }

    class MainViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ExtBindingList<IViolinModel> _violins;
        public ExtBindingList<IViolinModel> Violins
        {
            get { return _violins; }
            set
            {
                _violins = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Violins"));
            }
        }

        #region MAKERS_REGION
        private ObservableCollection<Maker> _makers;
        public ObservableCollection<Maker> Makers
        {
            get { return _makers; }
            set {
                _makers = value;  Console.WriteLine("Setting Makers in MainViewModel.");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Makers"));
            }
        }
        #endregion

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

        private IViolinStorage violinStorage;
        public MainViewModel()
        {
            string dburi = $"URI={Properties.Settings.Default.DataSourceUri}";
            violinStorage = new DbViolinStorage(dburi);

            _violins = new ExtBindingList<IViolinModel>();
            foreach (var v in violinStorage.ReadAll())
            {
                _violins.Add(v);
            }
            Violins.ListChanged += new ListChangedEventHandler(ViolinsChangedHandler);
            Violins.AllowNew = true;
            Violins.AddingNew += (sender, e) => e.NewObject = new ViolinDb();
            Violins.AllowEdit = true;
            Violins.AllowRemove = true;
            Violins.BeforeChange += BeforeViolinsChangedHandler;

            #region MAKERS_REGION
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
            #endregion
        }

        private void BeforeViolinsChangedHandler(IViolinModel item)
        {
            violinStorage.Delete(item);
        }

        private void ViolinsChangedHandler(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                violinStorage.Delete(Violins[e.NewIndex]);
                violinStorage.Save(Violins[e.NewIndex]);
            }
        }

        ~MainViewModel()
        {
        }
    }
}
