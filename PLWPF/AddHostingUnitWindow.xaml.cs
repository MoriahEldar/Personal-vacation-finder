using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddHostingUnitWindow.xaml
    /// </summary>
    public partial class AddHostingUnitWindow : Window
    {
        Ibl bl = FactorySingletonBl.Instance;
        HostingUnit unit = new HostingUnit();
        public AddHostingUnitWindow(Host host)
        {
            InitializeComponent();
            this.DataContext = unit;
            unit.OwnerId = host.Id;
            unit.Diary = new bool[12, 31];
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 31; j++)
                    unit.Diary[i, j] = false;
            AreaComboBox.ItemsSource = Enum.GetValues(typeof(TzimmerArea));
            TypeComboBox.ItemsSource = Enum.GetValues(typeof(TzimmerType));
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBox.Text == "" || textBox_Copy.Text == "" || textBox_Copy1.Text == "" || textBox_Copy2.Text == "" || textBox_Copy3.Text == "" || textBox_Copy4.Text == "" || unit.Description == "")
                    throw new ArgumentNullException("You need to fill all fields");
                bl.AddHostingUnit(unit);
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
