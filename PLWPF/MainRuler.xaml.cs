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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainRuler.xaml
    /// </summary>
    public partial class MainRuler : UserControl
    {
        Grid ThisGrid;
        Grid ThisRuler;
        public MainRuler(Grid ThisGrid, Grid ThisRuler)
        {
            InitializeComponent();
            this.ThisGrid = ThisGrid;
            this.ThisRuler = ThisRuler;
        }
        private void Host_Click(object sender, RoutedEventArgs e)
        {
            (new Login(ThisGrid, ThisRuler)).Show();
        }
        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            (new ManagerConfirm(ThisGrid, ThisRuler)).Show();
            //(new SendMail()).Show();
        }

        private void GuestRequest_Click(object sender, RoutedEventArgs e)
        {
            ThisGrid.Children.RemoveAt(0);
            ThisGrid.Children.Add(new AddGuestRequest());
        }
    }
}
