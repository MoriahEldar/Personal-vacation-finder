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
    /// Interaction logic for HostManager.xaml
    /// </summary>
    public partial class HostManager : UserControl
    {
        Ibl bl = FactorySingletonBl.Instance;
        public HostManager()
        {
            InitializeComponent();
            foreach (Host host in bl.GetHosts())
                listView.Items.Add(new HostViewManager(host));
        }

        private void NumUnitFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            listView.Items.Clear();
            foreach (var myGroup in bl.HostByNumUnits())
                if (myGroup.Key.ToString() == NumUnitsFilter.Text)
                {
                    var list = from host in bl.GetHosts()
                               from item in myGroup
                               where item == host.Id
                               select host;
                    foreach (Host host in list)
                        listView.Items.Add(new HostViewManager(host));
                }
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Predicate<Host> predicate = (host) =>
            {
                return host.FirstName.ToLower().Contains(search.Text.ToLower()) || host.LastName.ToLower().Contains(search.Text.ToLower()) || host.Id.ToString().Contains(search.Text);
            };
            listView.Items.Clear();
            foreach (Host host in bl.GetHosts())
                if (predicate(host))
                    listView.Items.Add(new HostViewManager(host));

        }
    }
}
