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
using BL;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for ManagerRuler.xaml
    /// </summary>
    public partial class ManagerRuler : UserControl
    {
        Ibl bl = FactorySingletonBl.Instance;
        Grid mainGrid;
        Grid mainRuler;
        public ManagerRuler(Grid mainGrid, Grid mainRuler)
        {
            InitializeComponent();
            this.mainGrid = mainGrid;
            this.mainRuler = mainRuler;
        }
        private void RequestsList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainGrid.Children.RemoveAt(0);
            }
            catch (Exception ex)
            {}
            mainGrid.Children.Add(new RequestsManager());
        }
        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainGrid.Children.RemoveAt(0);
            }
            catch (Exception ex)
            { }
            mainGrid.Children.Add(new AddGuestRequest());
            mainRuler.Children.RemoveAt(0);
            mainRuler.Children.Add(new MainRuler(mainGrid, mainRuler));
        }

        private void HostsList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainGrid.Children.RemoveAt(0);
            }
            catch (Exception ex)
            { }
            mainGrid.Children.Add(new HostManager());
        }

        private void OrdersList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainGrid.Children.RemoveAt(0);
            }
            catch (Exception ex)
            { }
            mainGrid.Children.Add(new OrderManager());
        }
    }
}
