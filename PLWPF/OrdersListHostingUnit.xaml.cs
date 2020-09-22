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
    /// Interaction logic for OrdersListHostingUnit.xaml
    /// </summary>
    public partial class OrdersListHostingUnit : UserControl
    {
        Ibl bl = FactorySingletonBl.Instance;
        HostingUnit unit;
        Grid ThisGrid;
        int index;
        public OrdersListHostingUnit(HostingUnit unit, Grid ThisGrid, int index)
        {
            InitializeComponent();
            textBox.DataContext = unit;
            textBox_Copy.DataContext = unit;
            this.ThisGrid = ThisGrid;
            this.unit = unit;
            this.index = index;
            var listOrder = from item in bl.GetOrders()
                            where item.HostingUnitKey == unit.HostingUnitKey && item.Status != OrderStatus.CLIENTNOCARE
                            select item;
            foreach (var order in listOrder)
                listView.Items.Add(new OrderView(order, listOrder.ToList(), listView));
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
