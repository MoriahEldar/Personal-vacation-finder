using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        Ibl bl = FactorySingletonBl.Instance;
        Grid ThisGrid;
        Grid ThisRuler;
        static MailMessage mail;
        static SmtpClient smtp;
        private static readonly BackgroundWorker worker = new BackgroundWorker();
        public Login(Grid ThisGrid, Grid ThisRuler)
        {
            InitializeComponent();
            this.ThisGrid = ThisGrid;
            this.ThisRuler = ThisRuler;
            this.KeyDown += Log_PreviewKeyDown;
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Host host;
            try
            {
                host = (from item in bl.GetHosts()
                        where Int32.Parse(item.Id) == Int32.Parse(idText.Text) && passwordText.Password == item.Password
                        select item).FirstOrDefault();
                if (host == null)
                    MessageBox.Show("Id or password are not correct");
                else
                {
                    ThisGrid.Children.RemoveAt(0);
                    ThisRuler.Children.RemoveAt(0);
                    ThisGrid.Children.Add(new HostHostingUnits(host, ThisGrid));
                    ThisRuler.Children.Add(new HostRuler(host, ThisGrid, ThisRuler));
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Id or password are not correct");
            }
        }
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (!bl.LoadBanksFinished())
                MessageBox.Show("Didn't finish to load banks");
            else
            {
                ThisGrid.Children.RemoveAt(0);
                ThisGrid.Children.Add(new AddHost(ThisGrid, ThisRuler));
                this.Close();
            }
        }
        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            Host host;
            try
            {
                host = (from item in bl.GetHosts()
                        where Int32.Parse(item.Id) == Int32.Parse(idText.Text)
                        select item).FirstOrDefault();
                if (host == null)
                    MessageBox.Show("Id does not exist in the system");
                else
                {
                    mail = new MailMessage();
                    mail.To.Add(host.Mail);
                    mail.From = Configuration.Mail;
                    mail.Subject = "Your password to wert site";
                    mail.Body = "Hello " + host.FirstName + ",<br/>Your password is: " + host.Password + "<br/> Have a nice day:),<br/>Wert site";
                    mail.IsBodyHtml = true;
                    smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Credentials = new System.Net.NetworkCredential(Configuration.Mail.ToString(), Configuration.Password);
                    smtp.EnableSsl = true;
                    worker.DoWork += worker_DoWork;
                    worker.RunWorkerAsync();
                }
            }
            catch
            {
                MessageBox.Show("Id does not exist in the system");
            }
        }
        /// <summary>
        /// login when enter is pressed
        /// </summary>
        private void Log_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login_Click(sender, e);
        }
        /// <summary>
        /// sends the mail
        /// </summary>
        private static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool success = false;
            while (success == false)
            {
                try
                {
                    smtp.Send(mail);
                    success = true;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
