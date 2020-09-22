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
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostRuler.xaml
    /// </summary>
    public partial class HostRuler : UserControl
    {
        Ibl bl = FactorySingletonBl.Instance;
        Host host;
        Grid mainGrid;
        Grid mainRuler;
        public HostRuler(Host host, Grid mainGrid, Grid mainRuler)
        {
            InitializeComponent();
            this.DataContext = host;
            this.host = host;
            this.mainGrid = mainGrid;
            this.mainRuler = mainRuler;
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (!bl.LoadBanksFinished())
                MessageBox.Show("Can not open it yet, please try again later");
            else
                (new UpdateHostWindow(host)).Show();
        }
        private void AddUnit_Click(object sender, RoutedEventArgs e)
        {
            (new AddHostingUnitWindow(host)).Show();
        }
        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.RemoveAt(0);
            mainGrid.Children.Add(new AddGuestRequest());
            mainRuler.Children.RemoveAt(0);
            mainRuler.Children.Add(new MainRuler(mainGrid, mainRuler));
        }
    }
}
