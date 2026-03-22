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

            ClothesGrid.SelectedIndex = 0;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
            List<Top> tops = persistence.loadTops();

            var grid = sender as DataGrid;
            grid!.ItemsSource = tops;
        }

        private void ClothesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (ClothesGrid.SelectedItem != null)
            {
                EditItemUI.ColourProperty.Text = ((Top)ClothesGrid.SelectedItem).mainColour;
                EditItemUI.DescriptionProperty.Text = ((Top)ClothesGrid.SelectedItem).description;
                EditItemUI.TypeProperty.Text = ((Top)ClothesGrid.SelectedItem).type.ToString();
                EditItemUI.LastWornProperty.SelectedDate = ((Top)ClothesGrid.SelectedItem).lastWorn.ToDateTime(TimeOnly.MinValue);
                EditItemUI.RegularlyCheckBox.IsChecked = ((Top)ClothesGrid.SelectedItem).inUse;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditItemUI.ColourProperty.IsReadOnly = false;
            EditItemUI.DescriptionProperty.IsReadOnly = false;
            EditItemUI.TypeProperty.IsEnabled = true;
            EditItemUI.LastWornProperty.IsEnabled = true;
            EditItemUI.RegularlyCheckBox.IsEnabled = true;
            EditItemUI.SaveButton.Visibility = Visibility.Visible;
            EditItemUI.CancelButton.Visibility = Visibility.Visible;
            EditItemUI.DeleteButton.Visibility = Visibility.Visible;

            EditItemUI.EditText.Text = "EDITING";
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            EditItemUI.DeleteButton.Visibility = Visibility.Collapsed;
            EditItemUI.ColourProperty.Text = string.Empty;
            EditItemUI.DescriptionProperty.Text = string.Empty;
            EditItemUI.TypeProperty.Text = string.Empty;
            EditItemUI.LastWornProperty.SelectedDate = new DateTime(DateTime.Now.Year, 1, 1);
            EditItemUI.RegularlyCheckBox.IsChecked = true;

            EditItemUI.ColourProperty.IsReadOnly = false;
            EditItemUI.DescriptionProperty.IsReadOnly = false;
            EditItemUI.TypeProperty.IsEnabled = true;
            EditItemUI.RegularlyCheckBox.IsEnabled = true;
            EditItemUI.LastWornProperty.IsEnabled = true;

            EditItemUI.SaveButton.Visibility = Visibility.Visible;
            EditItemUI.CancelButton.Visibility = Visibility.Visible;

            EditItemUI.EditText.Text = "ADDING";
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (FilterType.Text.Equals("Shirt Type"))
            {
                Enum.TryParse<TopType>(FilterBy.Text, ignoreCase: true, out TopType type);
                InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
                List<Top> tops = persistence.loadTopsByType(type);

                ClothesGrid!.ItemsSource = tops;
            }
            else if (FilterType.Text.Equals("Colour"))
            {
                string colour = FilterBy.Text;
                InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
                List<Top> tops = persistence.loadTopsByColour(colour);

                ClothesGrid!.ItemsSource = tops;
            }
            else if (FilterType.Text.Equals("Description"))
            {
                string description = FilterBy.Text;
                InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
                List<Top> tops = persistence.loadTopsByDescription(description);

                ClothesGrid!.ItemsSource = tops;
            }
            else if (FilterType.Text.Equals("In Rotation"))
            {
                string rotation = FilterBy.Text;
                bool inRotation = Convert.ToBoolean(rotation);
                InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
                List<Top> tops = persistence.loadTopsByInRotation(inRotation);

                ClothesGrid!.ItemsSource = tops;
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
            List<Top> tops = persistence.loadTops();

            ClothesGrid!.ItemsSource = tops;

            FilterType.Text = string.Empty;
            FilterBy.Text = string.Empty;
        }
    }
}