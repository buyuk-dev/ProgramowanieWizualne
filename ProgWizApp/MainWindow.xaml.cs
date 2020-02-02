using System;
using System.Windows;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Michalski
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Violin> violinsList;
        public static ObservableCollection<Maker> makersList;
        public static AddViolinDialog addViolinDialog;
        public static AddMakerDialog addMakerDialog;

        public MainWindow()
        {
            InitializeComponent();
            this.Title = Michalski.Properties.Settings.Default.AppTitle;
            string dburi = $"URI={Michalski.Properties.Settings.Default.DataSourceUri}";
            Console.WriteLine(dburi);

            var connection = new SQLiteConnection(dburi);
            connection.Open();

            var cmd = new SQLiteCommand("select * from violins", connection);
            var reader = cmd.ExecuteReader();

            violinsList = new ObservableCollection<Violin>();
            makersList = new ObservableCollection<Maker>();

            ViolinsDG.DataContext = violinsList;
            MakersDG.DataContext = makersList;

            while (reader.Read())
            {
                var name = reader.GetString(0);
                var maker = reader.GetString(1);
                var year = reader.GetInt32(2);
                var price = reader.GetInt32(3);
                var state = reader.GetString(4);
                violinsList.Add(new Violin(maker, name, (uint)year, (uint)price, state));
            }
            cmd.Dispose();
            reader.Dispose();

            cmd = new SQLiteCommand("select * from makers", connection);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var name = reader.GetString(0);
                var number = reader.GetString(1);
                var address = reader.GetString(2);
                makersList.Add(new Maker(name, number, address));
            }
            cmd.Dispose();
            reader.Dispose();
            connection.Dispose();
        }

        private void OnViolinRemoveBtn(object sender, RoutedEventArgs e)
        {
            var sel = ViolinsDG.SelectedIndex;
            violinsList.RemoveAt(sel);
        }

        private void OnMakerRemoveBtn(object sender, RoutedEventArgs e)
        {
            var sel = MakersDG.SelectedIndex;
            makersList.RemoveAt(sel);
        }
    }
}
