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
    /// Interaction logic for GuestRequestView.xaml
    /// </summary>
    public partial class GuestRequestView : UserControl
    {
        Ibl bl = FactorySingletonBl.Instance;
        GuestRequest req;
        List<GuestRequest> goodReqs;
        ListView list;
        long key;
        public GuestRequestView(GuestRequest req, HostingUnit unit, List<GuestRequest> goodReqs, ListView list)
        {
            InitializeComponent();
            this.DataContext = req;
            this.req = req;
            key = unit.HostingUnitKey;
            this.goodReqs = goodReqs;
            this.list = list;
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order()
            {
                HostingUnitKey = key,
                GuestRequestKey = req.GuestRequestKey
            };
            try
            {
                bl.AddOrder(order);
                goodReqs.Remove(req);
                list.Items.Remove(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
