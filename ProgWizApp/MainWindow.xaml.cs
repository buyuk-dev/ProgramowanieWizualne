using System;
using System.Windows;
using System.Data.SQLite;
using System.Collections.ObjectModel;

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

            Console.WriteLine("<Violins>");
            foreach (var v in violinsList)
            {
                Console.WriteLine($"\t{v.ToString()}");
            }
            Console.WriteLine("</Violins>");

            Console.WriteLine("<Makers>");
            foreach (var m in makersList)
            {
                Console.WriteLine($"\t{m.ToString()}");
            }
            Console.WriteLine("</Makers>");

            violinsListView.ItemsSource = violinsList;
            makersListView.ItemsSource = makersList;
        }

        private void OnViolinAddBtn(object sender, RoutedEventArgs e)
        {
            addViolinDialog = new AddViolinDialog();
            addViolinDialog.Visibility = Visibility.Visible;
            //violinsList.Add(new Violin("Maker", "Name", 0, 1000, "Good"));
        }

        private void OnViolinSelected(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selected = violinsListView.SelectedItems[0] as Violin;
            Console.WriteLine($"Selected instrument: {selected.name}");
        }

        private void OnViolinDeleteBtn(object sender, RoutedEventArgs e)
        {

        }

        private void OnViolinEditBtn(object sender, RoutedEventArgs e)
        {

        }

        private void OnMakerAddBtn(object sender, RoutedEventArgs e)
        {
            addMakerDialog = new AddMakerDialog();
            addMakerDialog.Visibility = Visibility.Visible;
        }

        private void OnMakerDeleteBtn(object sender, RoutedEventArgs e)
        {

        }

        private void OnMakerEditBtn(object sender, RoutedEventArgs e)
        {

        }
    }
}
