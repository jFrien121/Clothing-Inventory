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

            ClothesGrid.SelectedIndex = 0;
        }

        //----- GRID EVENT HANDLERS

        /**
         * Sets up the main grid with all the tops that are regularly worn
         */
        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
            List<Top> tops = persistence.loadTopsByInRotation(true);
            var grid = sender as DataGrid;
            grid!.ItemsSource = tops;
        }

        /* When a new row is selected on the grid, fill fields on right sidebar
         * with the details about that shirt
         */
        private void ClothesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            // Only change fields if the row contains data
            if (ClothesGrid.SelectedItem != null)
            {
                EditItemUI.ColourProperty.Text = ((Top)ClothesGrid.SelectedItem).mainColour;
                EditItemUI.DescriptionProperty.Text = ((Top)ClothesGrid.SelectedItem).description;
                EditItemUI.TypeProperty.Text = ((Top)ClothesGrid.SelectedItem).type.ToString();
                EditItemUI.LastWornProperty.SelectedDate = ((Top)ClothesGrid.SelectedItem).lastWorn.ToDateTime(TimeOnly.MinValue);
                EditItemUI.RegularlyCheckBox.IsChecked = ((Top)ClothesGrid.SelectedItem).inUse;
            }
        }

        //----- BUTTON CLICKING EVENT HANDLERS

        /* This method responds to the Edit button being clicked
         * It allows the fields for editing new shirts to be editable
         */
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Allow fields to be editable
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

        /* This method responds to the Add button being clicked
         * It allows the fields for adding new shirts to be editable
         */
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            // Clear all previous data
            EditItemUI.DeleteButton.Visibility = Visibility.Collapsed;
            EditItemUI.ColourProperty.Text = string.Empty;
            EditItemUI.DescriptionProperty.Text = string.Empty;
            EditItemUI.TypeProperty.Text = string.Empty;
            EditItemUI.LastWornProperty.SelectedDate = new DateTime(DateTime.Now.Year, 1, 1);
            EditItemUI.RegularlyCheckBox.IsChecked = true;

            // Allow fields to be editable
            EditItemUI.ColourProperty.IsReadOnly = false;
            EditItemUI.DescriptionProperty.IsReadOnly = false;
            EditItemUI.TypeProperty.IsEnabled = true;
            EditItemUI.RegularlyCheckBox.IsEnabled = true;
            EditItemUI.LastWornProperty.IsEnabled = true;

            EditItemUI.SaveButton.Visibility = Visibility.Visible;
            EditItemUI.CancelButton.Visibility = Visibility.Visible;

            EditItemUI.EditText.Text = "ADDING";
        }

        /* This method responds to the Search button being clicked
         * It reads the filters set by the user & gets the tops from
         * the database that match the filters given
         */
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the tops with specified type of shirt
            if (FilterType.Text.Equals("Shirt Type"))
            {
                // Convert text to enum
                // TODO: Error check this
                Enum.TryParse<TopType>(FilterBy.Text, ignoreCase: true, out TopType type);
                InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
                List<Top> tops = persistence.loadTopsByType(type);

                ClothesGrid!.ItemsSource = tops;
            }
            // Get the tops with specified colour of shirt
            else if (FilterType.Text.Equals("Colour"))
            {
                string colour = FilterBy.Text;
                InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
                List<Top> tops = persistence.loadTopsByColour(colour);

                ClothesGrid!.ItemsSource = tops;
            }
            // Get the tops with specified shirt description
            else if (FilterType.Text.Equals("Description"))
            {
                string description = FilterBy.Text;
                InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
                List<Top> tops = persistence.loadTopsByDescription(description);

                ClothesGrid!.ItemsSource = tops;
            }
            // Get the tops based on whether or not they are regularly worn
            else if (FilterType.Text.Equals("In Rotation"))
            {
                string rotation = FilterBy.Text;
                bool inRotation = Convert.ToBoolean(rotation);
                InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
                List<Top> tops = persistence.loadTopsByInRotation(inRotation);

                ClothesGrid!.ItemsSource = tops;
            }
        }

        /**
         * This method responds to the Reset button being clicked
         * It refreshes any filters that have been placed
         */
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
            List<Top> tops = persistence.loadTopsByInRotation(true);

            ClothesGrid!.ItemsSource = tops;

            // Clear filter fields
            FilterType.Text = string.Empty;
            FilterBy.Text = string.Empty;

            ClothesGrid.SelectedIndex = 0; // Ensure something is selected
        }
    }
}