using Clothing_Inventory.domain;
using Clothing_Inventory.persistence;
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

namespace Clothing_Inventory.ui
{
    /// <summary>
    /// Interaction logic for EditItem.xaml
    /// </summary>
    public partial class EditItem : UserControl
    {
        public EditItem()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow is MainWindow mainWin)
            {
                if (EditText.Text.Equals("EDITING"))
                {
                    // Update the Top in the Data Grid with the user given values
                    ((Top)mainWin.ClothesGrid.SelectedItem).description = DescriptionProperty.Text;
                    ((Top)mainWin.ClothesGrid.SelectedItem).mainColour = ColourProperty.Text;
                    Enum.TryParse<TopType>(TypeProperty.Text, ignoreCase: true, out TopType type);
                    ((Top)mainWin.ClothesGrid.SelectedItem).type = type;
                    ((Top)mainWin.ClothesGrid.SelectedItem).lastWorn = DateOnly.FromDateTime((DateTime)LastWornProperty.SelectedDate!);
                    ((Top)mainWin.ClothesGrid.SelectedItem).inUse = (bool)RegularlyCheckBox.IsChecked!;
                    mainWin.ClothesGrid.Items.Refresh();

                    // Save
                    List<Top> tops = ((List<Top>)mainWin.ClothesGrid.ItemsSource);
                    InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
                    persistence.saveTops(tops);

                    DeleteButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    // Create a new top from the user given values
                    List<Top> tops = ((List<Top>)mainWin.ClothesGrid.ItemsSource);
                    Enum.TryParse<TopType>(TypeProperty.Text, ignoreCase: true, out TopType type);
                    tops.Add(new Top(DescriptionProperty.Text, ColourProperty.Text, DateOnly.FromDateTime((DateTime)LastWornProperty.SelectedDate!), type, (bool)RegularlyCheckBox.IsChecked!));

                    // Save
                    mainWin.ClothesGrid!.ItemsSource = tops;
                    mainWin.ClothesGrid!.Items.Refresh();
                    InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
                    persistence.saveTops(tops);
                }
            }

            // Disable user editing after this point
            ColourProperty.IsReadOnly = true;
            DescriptionProperty.IsReadOnly = true;
            TypeProperty.IsReadOnly = true;
            LastWornProperty.IsEnabled = false;
            RegularlyCheckBox.IsEnabled = false;
            SaveButton.Visibility = Visibility.Hidden;
            CancelButton.Visibility = Visibility.Hidden;

            EditText.Text = string.Empty;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow is MainWindow mainWin)
            {
                mainWin.ClothesGrid.SelectedItems.Clear();
                mainWin.ClothesGrid.SelectedIndex = 0;
            }

            // Disable user editing after this point
            ColourProperty.IsReadOnly = true;
            DescriptionProperty.IsReadOnly = true;
            TypeProperty.IsReadOnly = true;
            LastWornProperty.IsEnabled = false;
            RegularlyCheckBox.IsEnabled = false;
            SaveButton.Visibility = Visibility.Hidden;
            CancelButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Collapsed;

            EditText.Text = string.Empty;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            deletePopup.IsOpen = true;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            deletePopup.IsOpen = false;
        }

        private void FinalDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            deletePopup.IsOpen = false;

            Window parentWindow = Window.GetWindow(this);

            if (parentWindow is MainWindow mainWin)
            {
                // Get the top that is selected
                Top currTop = (Top)mainWin.ClothesGrid.SelectedItem;
                List<Top> tops = ((List<Top>)mainWin.ClothesGrid.ItemsSource);

                tops.Remove(currTop);

                // Save the change
                InventoryPersistence persistence = new InventoryPersistenceJson("inventory.json");
                persistence.saveTops(tops);

                // Refresh the view
                mainWin.ClothesGrid!.ItemsSource = tops;
                mainWin.ClothesGrid.Items.Refresh();

                // Ensure something is always selected
                mainWin.ClothesGrid.SelectedItems.Clear();
                mainWin.ClothesGrid.SelectedIndex = 0;
            }

            // Things shouldn't be edited anymore
            ColourProperty.IsReadOnly = true;
            DescriptionProperty.IsReadOnly = true;
            TypeProperty.IsReadOnly = true;
            LastWornProperty.IsEnabled = false;
            RegularlyCheckBox.IsEnabled = false;
            SaveButton.Visibility = Visibility.Hidden;
            CancelButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Collapsed;

        }
    }
}
