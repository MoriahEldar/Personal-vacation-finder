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
    /// Interaction logic for AddGuestRequest.xaml
    /// </summary>
    public partial class AddGuestRequest : UserControl
    {
        Ibl bl = FactorySingletonBl.Instance;
        GuestRequest req = new GuestRequest();
        public AddGuestRequest()
        {
            InitializeComponent();
            this.DataContext = req;
            areaCombo.ItemsSource = Enum.GetValues(typeof(TzimmerArea));
            typeCombo.ItemsSource = Enum.GetValues(typeof(TzimmerType));
            poolCombo.ItemsSource = Enum.GetValues(typeof(Options));
            gardenCombo.ItemsSource = Enum.GetValues(typeof(Options));
            childrenCombo.ItemsSource = Enum.GetValues(typeof(Options));
            entry.DisplayDateStart = DateTime.Today;
            entry.DisplayDateEnd = DateTime.Today.AddMonths(11).AddDays(-1);
            entry.BlackoutDates.Add(new CalendarDateRange(DateTime.Today));
            entry.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddMonths(11).AddDays(-1)));
            release.IsEnabled = false;
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBox.Text == "" || textBox_Copy.Text == "" || textBox_Copy1.Text == "" || textBox_Copy2.Text == "" || textBox_Copy3.Text == "" || textBox_Copy4.Text == "")
                    throw new ArgumentNullException("You need to fill all fields");
                bl.AddRequest(req);
                MessageBox.Show("Added");
                req = new GuestRequest();
                this.DataContext = req;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void entry_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            release.IsEnabled = true;
            release.BlackoutDates.Remove(release.BlackoutDates.FirstOrDefault());
            release.SelectedDate = entry.SelectedDate.GetValueOrDefault().AddDays(1);
            release.DisplayDateStart = entry.SelectedDate;
            release.BlackoutDates.Add(new CalendarDateRange(entry.SelectedDate.GetValueOrDefault()));
            if (entry.SelectedDate.GetValueOrDefault().AddMonths(1) < DateTime.Today.AddMonths(11).AddDays(-1))
                release.DisplayDateEnd = entry.SelectedDate.GetValueOrDefault().AddMonths(1);
            else
                release.DisplayDateEnd = DateTime.Today.AddMonths(11).AddDays(-1);
        }
    }
}