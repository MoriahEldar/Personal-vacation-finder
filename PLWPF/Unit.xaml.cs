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
    /// Interaction logic for Unit.xaml
    /// </summary>
    public partial class Unit : UserControl
    {
        Ibl bl = FactorySingletonBl.Instance;
        HostingUnit unit;
        Grid ThisGrid;
        int index;
        public Unit(HostingUnit unit, Grid ThisGrid, int index)
        {
            InitializeComponent();
            this.ThisGrid = ThisGrid;
            this.unit = unit;
            this.index = index;
            this.DataContext = unit;
            for (DateTime i = DateTime.Today.AddMonths(-1); i < DateTime.Today.AddMonths(11); i = i.AddDays(1))
                if (unit.Diary[i.Month - 1, i.Day - 1])
                    MyCalendar.BlackoutDates.Add(new CalendarDateRange(i));
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            (new UpdateHostingUnitWindow(unit)).Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this unit? This is an irreversible action", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try 
                {
                    bl.DeleteHostingUnit(unit);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Requests_Click(object sender, RoutedEventArgs e)
        {
            ThisGrid.Children.RemoveAt(0);
            ThisGrid.Children.Add(new GuestRequestListHostingUnit(unit, ThisGrid, index));
        }
        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            ThisGrid.Children.RemoveAt(0);
            ThisGrid.Children.Add(new OrdersListHostingUnit(unit, ThisGrid, index));
        }
    }
}
