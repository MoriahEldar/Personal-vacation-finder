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
    /// Interaction logic for HostHostingUnits.xaml
    /// </summary>
    public partial class HostHostingUnits : UserControl
    {
        Grid ThisGrid;
        Ibl bl = FactorySingletonBl.Instance;
        Host host;
        int index;
        public HostHostingUnits(Host host, Grid ThisGrid, int index = 0)
        {
            InitializeComponent();
            this.host = host;
            this.ThisGrid = ThisGrid;
            this.index = index;
            if (bl.HostsUnits(host).Count == 0)
            {

            }
            else
            {
                Unit a = new Unit(bl.HostsUnits(host)[index], ThisGrid, index);
                UnitGrid.Children.Add(a);
            }
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            if (bl.HostsUnits(host).Count != 0)
            {
                index++;
                index %= bl.HostsUnits(host).Count; // For recycle
                try
                {
                    UnitGrid.Children.RemoveAt(0);
                }
                catch (Exception)
                { }
                Unit a = new Unit(bl.HostsUnits(host)[index], ThisGrid, index);
                UnitGrid.Children.Add(a);
            }
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            if (bl.HostsUnits(host).Count != 0)
            {
                index--;
                index += bl.HostsUnits(host).Count;
                index %= bl.HostsUnits(host).Count; // For recycle
                try
                {
                    UnitGrid.Children.RemoveAt(0);
                }
                catch (Exception)
                { }
                Unit a = new Unit(bl.HostsUnits(host)[index], ThisGrid, index);
                UnitGrid.Children.Add(a);
            }
        }
    }
}
