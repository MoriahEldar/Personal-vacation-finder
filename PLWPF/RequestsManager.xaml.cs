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
    /// Interaction logic for RequestsManager.xaml
    /// </summary>
    public partial class RequestsManager : UserControl
    {
        Ibl bl = FactorySingletonBl.Instance;
        public RequestsManager()
        {
            InitializeComponent();
            AreaFilter.ItemsSource = Enum.GetValues(typeof(TzimmerArea));
            foreach (GuestRequest req in bl.GetGuestRequests())
                listView.Items.Add(new RequestViewManager(req));
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Predicate<GuestRequest> predicate = (req) =>
            {
                return req.FirstName.ToLower().Contains(search.Text.ToLower()) || req.LastName.ToLower().Contains(search.Text.ToLower()) || req.GuestRequestKey.ToString().Contains(search.Text);
            };
            listView.Items.Clear();
            foreach (GuestRequest req in bl.TrueCondition(predicate))
                listView.Items.Add(new RequestViewManager(req));
        }
        private void AreaFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listView.Items.Clear();
            foreach (var group in bl.ReqsByArea())
                if (group.Key.ToString() == AreaFilter.SelectedItem.ToString())
                    foreach (GuestRequest req in group)
                        listView.Items.Add(new RequestViewManager(req));
        }

        private void PeopleFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            listView.Items.Clear();
            foreach (var group in bl.ReqsByNumGuests())
                if (group.Key.ToString() == PeopleFilter.Text)
                    foreach (GuestRequest req in group)
                        listView.Items.Add(new RequestViewManager(req));
        }
    }
}
