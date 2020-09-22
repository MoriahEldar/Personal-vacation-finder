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
using System.Net.Mail;
using BE;
using BL;
using System.Threading;
using System.ComponentModel;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Mail.xaml
    /// </summary>
    public partial class Mail : Window
    {
        Ibl bl = FactorySingletonBl.Instance;
        static MailMessage mail = new MailMessage();
        static SmtpClient smtp = new SmtpClient();
        private static readonly BackgroundWorker worker = new BackgroundWorker();
        public Mail(MailAddress recieve)
        {
            InitializeComponent();
            this.DataContext = mail;
            mail.To.Add(recieve);
            mail.From = Configuration.Mail;
            mail.IsBodyHtml = false;
            smtp.Host = "smtp.gmail.com";
        }
        private void button_Click(object asender, RoutedEventArgs e)
        {
            smtp.Credentials = new System.Net.NetworkCredential(Configuration.Mail.ToString(), Configuration.Password);
            smtp.EnableSsl = true;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerAsync();
            this.Close();
        }
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
