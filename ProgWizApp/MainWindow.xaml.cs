using System;
using System.Windows;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;

namespace Michalski
{
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Violin> violinsList;
        public static ObservableCollection<Maker> makersList;
        public static AddViolinDialog addViolinDialog;
        public static AddMakerDialog addMakerDialog;

        public MainWindow()
        {
            InitializeComponent();
            Title = Properties.Settings.Default.AppTitle;

        }

        private void OnViolinRemoveBtn(object sender, RoutedEventArgs e)
        {
            var sel = ViolinsDG.SelectedIndex;
            if (sel < 0 || sel >= violinsList.Count)
            {
                MessageBox.Show("Select item to remove.", "Violins", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                violinsList.RemoveAt(sel);
            }
        }

        private void OnMakerRemoveBtn(object sender, RoutedEventArgs e)
        {
            var sel = MakersDG.SelectedIndex;
            if (sel < 0 || sel >= makersList.Count)
            {
                MessageBox.Show("Select item to remove.", "Makers", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                makersList.RemoveAt(sel);
            }
        }
    }
}
