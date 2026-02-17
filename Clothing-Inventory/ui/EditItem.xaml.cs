using Clothing_Inventory.domain;
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
                ((Top)mainWin.ClothesGrid.SelectedItem).description = DescriptionProperty.Text;
                ((Top)mainWin.ClothesGrid.SelectedItem).mainColour = ColourProperty.Text;
                Enum.TryParse<TopType>(TypeProperty.Text, ignoreCase: true, out TopType type);
                ((Top)mainWin.ClothesGrid.SelectedItem).type =  type;
                ((Top)mainWin.ClothesGrid.SelectedItem).lastWorn =  DateOnly.FromDateTime((DateTime)LastWornProperty.SelectedDate!);

            }
        }
    }
}
