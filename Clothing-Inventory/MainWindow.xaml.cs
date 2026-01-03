using Clothing_Inventory.domain;
using Clothing_Inventory.persistence;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clothing_Inventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Top top1 = new Top("stripes", "blue", new DateOnly(2025, 12, 31), TopType.CASUAL, true);
            //List<Top> tops = new List<Top>();
            //tops.Add(top1);

            InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
            //persistence.saveTops(tops);
            List<Top> tops1 = persistence.loadTops();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
            List<Top> tops = persistence.loadTops();

            var grid = sender as DataGrid;
            grid.ItemsSource = tops;
        }
    }
}