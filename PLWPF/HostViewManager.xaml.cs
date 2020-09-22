using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for HostViewManager.xaml
    /// </summary>
    public partial class HostViewManager : UserControl
    {
        Ibl bl = FactorySingletonBl.Instance;
        Host host;
        public HostViewManager(Host host)
        {
            InitializeComponent();
            this.host = host;
            this.DataContext = host;
            textBankAccountNumber.DataContext = host.HostBankAccount;
            textBankAddress.DataContext = host.HostBankAccount;
            textBankName.DataContext = host.HostBankAccount;
        }
        private void Charged_Click(object sender, RoutedEventArgs e)
        {
            host.Commission = 0;
            bl.UpdateHost(host);
            commissionText.Text = "0";
        }

        private void SendMail_Click(object sender, RoutedEventArgs e)
        {
            Mail mail = new Mail(new MailAddress(host.Mail));
            mail.Show();
        }
    }
}
