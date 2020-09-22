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
    /// Interaction logic for OrderViewManager.xaml
    /// </summary>
    public partial class OrderViewManager : UserControl
    {
        Ibl bl = FactorySingletonBl.Instance;
        Order order;
        ListView list;
        public OrderViewManager(Order order, ListView list)
        {
            InitializeComponent();
            textOrderDate.Text = order.OrderDate.ToString("dd-MM-yyyy");
            if (order.Status == OrderStatus.NOTYETCARE)
                textOrderDate.Text = "No order date yet";
            this.order = order;
            this.list = list;
            #region find req, unit and host
            var req = (from item in bl.GetGuestRequests()
            where item.GuestRequestKey == order.GuestRequestKey
                   select item).FirstOrDefault();
            var unit = (from item in bl.GetHostingUnits()
                    where item.HostingUnitKey == order.HostingUnitKey
                    select item).FirstOrDefault();
            var host = (from item in bl.GetHosts()
                    where item.Id == unit.OwnerId
                    select item).FirstOrDefault();
            this.DataContext = order;
            #endregion
            #region dataContext 
            FirstNameReqText.DataContext = req;
            LastNameReqText.DataContext = req;
            GuestReqKeyText.DataContext = req;
            EntryDateReqText.DataContext = req;
            ReleaseDateReqText.DataContext = req;
            FirstNameHostText.DataContext = host;
            LastNameHostText.DataContext = host;
            HostKeyText.DataContext = host;
            HostingUnitNameText.DataContext = unit;
            HostingUnitKeyText.DataContext = unit;
            #endregion
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete? This is an irreversible action", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                return;
            OrderStatus ordStatus = order.Status;
            try
            {
                order.Status = OrderStatus.CLIENTNOCARE;
                bl.UpdateOrder(order);
                list.Items.Remove(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                order.Status = ordStatus;
            }
        }
    }
}
