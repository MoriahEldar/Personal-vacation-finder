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
using System.Windows.Forms;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ibl bl = FactorySingletonBl.Instance;
        public MainWindow()
        {
            InitializeComponent();
            Data.Children.Add(new AddGuestRequest());
            MainRuler a = new MainRuler(Data, Ruler);
            Ruler.Children.Add(a);
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            //שמנו את זה כדי שזה יפעל ללא בעיות גם כשעושים שינויים בקוד. אבל אז זה הורג את התהליכון של פעם ביום.
            //אם לא רוצים שהתהליכון ימות, אפשר למחוק את השורה אז, וכל עוד לא עושים שינויים בקוד, הכל יעבוד
            Environment.Exit(Environment.ExitCode);
        }
    }
}
