using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
                Console.WriteLine(violinsList.ToString());
            }
            Console.WriteLine("</Violins>");

            Console.WriteLine("<Makers>");
            foreach (var m in makersList)
            {
                Console.WriteLine(m.ToString());
            }
            Console.WriteLine("</Makers>");

            violinsListView.ItemsSource = violinsList;
            makersListView.ItemsSource = makersList;
        }

        private void OnViolinAddBtn(object sender, RoutedEventArgs e)
        {
            violinsList.Add(new Violin("Maker", "Name", 0, 1000, "Good"));
        }
    }
}
