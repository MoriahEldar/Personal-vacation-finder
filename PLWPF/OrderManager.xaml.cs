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
    /// Interaction logic for OrderManager.xaml
    /// </summary>
    public partial class OrderManager : UserControl
    {
        Ibl bl = FactorySingletonBl.Instance;
        public OrderManager()
        {
            InitializeComponent();
            foreach (Order order in bl.GetOrders())
                if(order.Status != OrderStatus.CLIENTNOCARE)
                    listView.Items.Add(new OrderViewManager(order, listView));
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Predicate<Order> predicate = (order) =>
            {
                return order.OrderKey.ToString().Contains(search.Text);
            };
            listView.Items.Clear();
            foreach (Order order in bl.GetOrders())
                if (predicate(order))
                    listView.Items.Add(new OrderViewManager(order, listView));

        }
    }
}
