using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for UpdateHostWindow.xaml
    /// </summary>
    public partial class UpdateHostWindow : Window
    {
        Ibl bl = FactorySingletonBl.Instance;
        Host host;
        List<BankAccount> banks;
        public UpdateHostWindow(Host host)
        {
            InitializeComponent();
            this.host = host;
            this.DataContext = host;
            List<string> temp = new List<string>();
            banks = new List<BankAccount>();
            foreach (var item1 in bl.GetBankAccounts())
            {
                bool flag = true;
                foreach (var item2 in banks)
                    if (item1.BankNumber == item2.BankNumber)
                        flag = false;
                if (flag)
                {
                    temp.Add(item1.BankName);
                    banks.Add(item1);
                }
            }
            bankNameCombo.ItemsSource = temp;
            bankNameCombo.DataContext = host.HostBankAccount;
            branchNameCombo.DataContext = host.HostBankAccount;
            bankAccountText.DataContext = host.HostBankAccount;
            passwordBox.Password = host.Password;
        }
        private void bankNameCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            branchNameCombo.ItemsSource = from item in bl.GetBankAccounts()
                                          where item.BankName == banks[bankNameCombo.SelectedIndex].BankName
                                          select item.BranchAddress;
            host.HostBankAccount.BankNumber = (from item in bl.GetBankAccounts()
                                               where item.BankName == banks[bankNameCombo.SelectedIndex].BankName
                                               select item.BankNumber).FirstOrDefault();
            branchNameCombo.IsEnabled = true;
        }
        private void branchNameCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            host.HostBankAccount.BranchNumber = (from item in bl.GetBankAccounts()
                                                 where item.BankName == host.HostBankAccount.BankName && item.BranchAddress == host.HostBankAccount.BranchAddress
                                                 select item.BranchNumber).FirstOrDefault();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                host.Password = passwordBox.Password;
                if (textBox.Text == "" || textBox_Copy.Text == "" || textBox_Copy1.Text == "" || textBox_Copy2.Text == "" || bankAccountText.Text == "" || textBox_Copy4.Text == "" || passwordBox.Password == "")
                    throw new ArgumentNullException("You need to fill all fields");
                if (bankNameCombo.SelectedIndex == -1 || branchNameCombo.SelectedIndex == -1)
                    throw new ArgumentNullException("You need to fill all fields");
                bl.UpdateHost(host);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
