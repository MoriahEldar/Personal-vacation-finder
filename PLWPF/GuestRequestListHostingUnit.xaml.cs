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
    /// Interaction logic for GuestRequestListHostingUnit.xaml
    /// </summary>
    public partial class GuestRequestListHostingUnit : UserControl
    {
        Ibl bl = FactorySingletonBl.Instance;
        HostingUnit unit;
        List<GuestRequest> goodReqs;
        Grid ThisGrid;
        int index;
        public GuestRequestListHostingUnit(HostingUnit unit, Grid ThisGrid, int index)
        {
            InitializeComponent();
            this.unit = unit;
            this.ThisGrid = ThisGrid;
            this.index = index;
            textBox.DataContext = unit;
            textBox_Copy.DataContext = unit;
            goodReqs = bl.Goodreq(unit);
            foreach (var req in goodReqs)
                listView.Items.Add(new GuestRequestView(req, unit, goodReqs, listView));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var host = (from item in bl.GetHosts()
                        where item.Id == unit.OwnerId
                        select item).FirstOrDefault();
            ThisGrid.Children.RemoveAt(0);
            ThisGrid.Children.Add(new HostHostingUnits(host, ThisGrid, index));
        }
    }
}
