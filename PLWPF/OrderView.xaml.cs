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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        Order order;
        GuestRequest req;
        HostingUnit unit;
        Host host;
        List<Order> goodOrders;
        ListView list;
        Ibl bl = FactorySingletonBl.Instance;
        private static readonly BackgroundWorker worker = new BackgroundWorker();
        static MailMessage mail;
        static SmtpClient smtp;
        public OrderView(Order order, List<Order> goodOrders, ListView list)
        {
            InitializeComponent();
            this.order = order;
            this.goodOrders = goodOrders;
            this.list = list;
            if (order.Status == OrderStatus.MAILSENT)
                Status.Content = "Confirm Order";
            if (order.Status == OrderStatus.CARE)
                Status.Content = "Order is Yours";
            req = (from item in bl.GetGuestRequests()
                   where item.GuestRequestKey == order.GuestRequestKey
                   select item).FirstOrDefault();
            unit = (from item in bl.GetHostingUnits()
                    where item.HostingUnitKey == order.HostingUnitKey
                    select item).FirstOrDefault();
            host = (from item in bl.GetHosts()
                        where item.Id == unit.OwnerId
                        select item).FirstOrDefault();
            this.DataContext = req;
            OrderKeyText.DataContext = order;
            Icon.DataContext = order;
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            if (order.Status == OrderStatus.NOTYETCARE)
            {
                try
                {
                    order.Status = OrderStatus.MAILSENT;
                    order.ImageStatus = "resources/check.png";
                    Icon.DataContext = order;
                    order.OrderDate = DateTime.Today;
                    bl.UpdateOrder(order);
                    mail = new MailMessage();
                    mail.To.Add(req.Mail);
                    mail.From = new MailAddress(host.Mail);
                    mail.Subject = "A perfect vacation for you! :) from wert";
                    mail.Body = "Hello " + req.FirstName + ",<br/>We found the perfect vacation for you!<br/>It's a fabulous " + unit.Type.ToString().ToLower() + " in " + unit.Area.ToString().ToLower() + " area, " + unit.Address + "." +
                    "<br/>Your order key is: " + order.OrderKey + "<br/>Your guest request key is: " + order.GuestRequestKey + ".<br/>For more details, and in order to submit the deal, send me a mail back, or call me - " + host.PhoneNumber + 
                    "<br/>Have a nice day,<br/>" + host.FirstName;
                    mail.IsBodyHtml = true;
                    smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Credentials = new System.Net.NetworkCredential(host.Mail, host.Password);
                    smtp.EnableSsl = true;
                    worker.DoWork += worker_DoWork;
                    worker.RunWorkerAsync();
                    Status.Content = "Confirm Order";
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                    order.Status = OrderStatus.NOTYETCARE;
                    order.ImageStatus = "resources/gmail.png";
                    Icon.DataContext = order;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    order.Status = OrderStatus.NOTYETCARE;
                    order.ImageStatus = "resources/gmail.png";
                    Icon.DataContext = order;
                    bl.UpdateOrder(order);
                }
            }
            else if (order.Status == OrderStatus.MAILSENT)
            {
                try
                {
                    order.Status = OrderStatus.CARE;
                    order.ImageStatus = "resources/smile.png";
                    Icon.DataContext = order;
                    bl.UpdateOrder(order);
                    Status.Content = "Order is Yours";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    order.Status = OrderStatus.MAILSENT;
                    order.ImageStatus = "resources/check.png";
                    Icon.DataContext = order;
                }
            }
            
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete? This is an irreversible action", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                return;
            OrderStatus ordStatus = order.Status;
            try
            {
                order.Status = OrderStatus.CLIENTNOCARE;
                bl.UpdateOrder(order);
                goodOrders.Remove(order);
                list.Items.Remove(this);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                order.Status = ordStatus;
            }
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
