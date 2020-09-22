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
using System.Windows.Shapes;
using BL;
using BE;
using System.ComponentModel;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for UpdateHostingUnitWindow.xaml
    /// </summary>
    public partial class UpdateHostingUnitWindow : Window
    {
        Ibl bl = FactorySingletonBl.Instance;
        HostingUnit unit;
        Host owner;
        public UpdateHostingUnitWindow(HostingUnit unit)
        {
            InitializeComponent();
            owner = (from item in bl.GetHosts()
                     where item.Id == unit.OwnerId
                     select item).FirstOrDefault();
            this.unit = unit;
            this.DataContext = unit;
            AreaComboBox.ItemsSource = Enum.GetValues(typeof(TzimmerArea));
            TypeComboBox.ItemsSource = Enum.GetValues(typeof(TzimmerType));
            OwnerComboBox.ItemsSource = bl.GetHosts();
            OwnerComboBox.DisplayMemberPath = "FirstName";
        }
        private void OwnerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OwnerComboBox.SelectedItem != owner)
            {
                if (MessageBox.Show("Are you sure you want to change the owner? This is an irreversible action", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    unit.OwnerId = owner.Id;
                else
                {
                    Host helpHost = OwnerComboBox.SelectedItem as Host;
                    unit.OwnerId = helpHost.Id;
                    owner = helpHost;
                }
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBox.Text == "" || textBox_Copy.Text == "" || textBox_Copy1.Text == "" || textBox_Copy2.Text == "" || textBox_Copy3.Text == "" || textBox_Copy4.Text == "" || unit.Description == "")
                    throw new ArgumentNullException("You need to fill all fields");
                bl.UpdateHostingUnit(unit);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closed(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit? It will not be saved.", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                e.Cancel = true;
        }
    }
}
