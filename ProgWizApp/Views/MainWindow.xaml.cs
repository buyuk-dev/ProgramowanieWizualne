using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Michalski
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ViolinFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = (sender as TextBox).Text;
            if (filter.Length > 0)
            {
                IEnumerable<IViolinModel> filteredDataEnumerable = from violin in mainVM.Violins where violin.maker.StartsWith(filter) select violin;
                var filteredData = new ExtBindingList<IViolinModel>();
                foreach (var item in filteredDataEnumerable)
                {
                    filteredData.Add(item);
                }
                ViolinsDataGrid.DataContext = filteredData;
            }
            else
            {
                ViolinsDataGrid.DataContext = mainVM.Violins;
            }
        }
    }
}
