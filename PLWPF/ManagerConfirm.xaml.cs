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
using System.Windows.Shapes;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for ManagerConfirm.xaml
    /// </summary>
    public partial class ManagerConfirm : Window
    {
        Grid thisGrid;
        Grid thisRuler;
        public ManagerConfirm(Grid thisGrid, Grid thisRuler)
        {
            InitializeComponent();
            this.thisGrid = thisGrid;
            this.thisRuler = thisRuler;
            this.KeyDown += Log_PreviewKeyDown;
        }
        private void Log_Click(object sender, RoutedEventArgs e)
        {
            if (passwordText.Password != Configuration.Password)
                MessageBox.Show("Incorrect password");
            else
            {
                thisGrid.Children.RemoveAt(0);
                thisRuler.Children.RemoveAt(0);
                thisRuler.Children.Add(new ManagerRuler(thisGrid, thisRuler));
                this.Close();
            }
        }
        /// <summary>
        /// login when enter is pressed
        /// </summary>
        private void Log_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Log_Click(sender, e);
        }
    }
}
