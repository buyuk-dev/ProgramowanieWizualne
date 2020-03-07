﻿using System.ComponentModel;
using Michalski.Utils;
using Michalski.Models;
using Michalski.BusinessLogic;

namespace Michalski.WPFApp
{
    public class MainViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        readonly static string dburi = $"URI={Properties.Settings.Default.DataSourceUri}";

        #region VIOLINS_TAB
        private IViolinStorage violinStorage;
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

        private void InitViolinData()
        {
            violinStorage = new DbViolinStorage(dburi);
            _violins = new ExtBindingList<IViolinModel>();
            foreach (var v in violinStorage.ReadAll())
            {
                _violins.Add(v);
            }
            Violins.ListChanged += new ListChangedEventHandler(ViolinsChangedHandler);
            Violins.AddingNew += (sender, e) => e.NewObject = new ViolinDb();
            Violins.BeforeChange += BeforeViolinsChangedHandler;
        }
        #endregion // VIOLINS_TAB

        #region MAKERS_TAB
        private IMakerStorage makerStorage;
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

        private void InitMakersData()
        {
            makerStorage = new DbMakerStorage(dburi);
            _makers = new ExtBindingList<IMakerModel>();
            foreach (var m in makerStorage.ReadAll())
            {
                _makers.Add(m);
            }
            Makers.ListChanged += new ListChangedEventHandler(MakersChangedHandler);
            Makers.AddingNew += (sender, e) => e.NewObject = new MakerDb();
            Makers.BeforeChange += BeforeMakersChangeHandler;
        }
        #endregion // MAKERS_TAB

        public MainViewModel()
        {
            InitViolinData();
            InitMakersData();
        }
    }
}
