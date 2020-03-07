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
            BeforeChange?.Invoke(deletedItem);
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

        private ExtBindingList<IMakerModel> _makers;
        public ExtBindingList<IMakerModel> Makers
        {
            get { return _makers; }
            set
            {
                _makers = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Makers"));
            }
        }

        private IViolinStorage violinStorage;
        private IMakerStorage makerStorage;

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

            makerStorage = new DbMakerStorage(dburi);
            _makers = new ExtBindingList<IMakerModel>();
            foreach (var m in makerStorage.ReadAll())
            {
                _makers.Add(m);
            }
            Makers.ListChanged += new ListChangedEventHandler(MakersChangedHandler);
            Makers.AllowNew = true;
            Makers.AddingNew += (sender, e) => e.NewObject = new MakerDb();
            Makers.AllowEdit = true;
            Makers.AllowRemove = true;
            Makers.BeforeChange += BeforeMakersChangeHandler;

        }

        private void BeforeMakersChangeHandler(IMakerModel item)
        {
            makerStorage.Delete(item);
        }

        private void MakersChangedHandler(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                if (e.PropertyDescriptor.Name == "id") return; // ignoring, only possible on new item
                makerStorage.Save(Makers[e.NewIndex]);
            }

            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                makerStorage.Save(Makers[e.NewIndex]);
            }
        }

        private void BeforeViolinsChangedHandler(IViolinModel item)
        {
            violinStorage.Delete(item);
        }

        private void ViolinsChangedHandler(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                if (e.PropertyDescriptor.Name == "id") return; // ignoring, only possible on new item
                violinStorage.Save(Violins[e.NewIndex]);
            }

            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                violinStorage.Save(Violins[e.NewIndex]);
            }
        }

        ~MainViewModel()
        {
        }
    }
}
