using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;
using DS;

namespace DAL
{
    class Dal_XML_imp : Idal
    {
        XElement confRoot;
        XElement orderRoot;
        static string projectPath = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory.ToString()).FullName).FullName;
        static string confPath = projectPath + "/Data/conf.xml";
        static string orderPath = projectPath + "/Data/order.xml";
        static string hostPath = projectPath + "/Data/host.xml";
        static string hostingUnitPath = projectPath + "/Data/hostingUnit.xml";
        static string guestRequestPath = projectPath + "/Data/guestRequest.xml";
        public static readonly BackgroundWorker worker = new BackgroundWorker();
        private static bool finishBankLoad;
        public bool FinishBankLoad 
        { 
            get
            {
                return finishBankLoad;
            }
            set
            {
                finishBankLoad = value;
            }
        }
        internal Dal_XML_imp()
        {
            finishBankLoad = false;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerAsync();
            if (!File.Exists(confPath))
            {
                DataSource_XML.saveConf();
            }
            else
            {
                confRoot = XElement.Load(confPath);
                Configuration.GuestRequestKey = Int32.Parse(confRoot.Element("GuestRequestKey").Value);
                Configuration.HostingUnitKey = Int32.Parse(confRoot.Element("HostingUnitKey").Value);
                Configuration.OrderKey = Int32.Parse(confRoot.Element("OrderKey").Value);
                Configuration.Commission = Int32.Parse(confRoot.Element("Commission").Value);
                Configuration.Mail = new MailAddress(confRoot.Element("Email").Value);
                Configuration.Password = confRoot.Element("Password").Value;
            }
            if (!File.Exists(guestRequestPath))
                DataSource_XML.SaveToXML(new List<GuestRequest>(), guestRequestPath);
            DataSource.guestRequests = DataSource_XML.LoadFromXML<List<GuestRequest>>(guestRequestPath);
            if (!File.Exists(hostPath))
                DataSource_XML.SaveToXML(new List<Host>(), hostPath);
            DataSource.hosts = DataSource_XML.LoadFromXML<List<Host>>(hostPath);
            if (!File.Exists(hostingUnitPath))
                DataSource_XML.SaveToXML(new List<HostingUnit>(), hostingUnitPath);
            DataSource.hostingUnits = DataSource_XML.LoadFromXML<List<HostingUnit>>(hostingUnitPath);
            if (!File.Exists(orderPath))
            {
                orderRoot = new XElement("order");
                orderRoot.Save(orderPath);
            }
            orderRoot = XElement.Load(orderPath);
        }
        public void AddRequest(GuestRequest req)
        {
            if (DataSource.guestRequests.Any(item => req.GuestRequestKey == item.GuestRequestKey))
                throw new DuplicateWaitObjectException("This key already exists, DAL error");
            DataSource.guestRequests.Add(req.Clone());
            DataSource_XML.saveConf();
            DataSource_XML.SaveToXML(DataSource.guestRequests, guestRequestPath);
        }
        public void UpdateRequest(GuestRequest guestRequest)
        {
            var list = (from item in DataSource.guestRequests
                        where guestRequest.GuestRequestKey == item.GuestRequestKey
                        select item).FirstOrDefault<GuestRequest>();
            if (list == null)
                throw new KeyNotFoundException("Key not found, DAL error");
            list.Status = guestRequest.Status;
            DataSource_XML.SaveToXML(DataSource.guestRequests, guestRequestPath);
        }
        public List<GuestRequest> GetGuestRequests()
        {
            var list = from item in DataSource.guestRequests
                       select item.Clone();
            return list.ToList();
        }
        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            hostingUnit.HostingUnitKey = Configuration.HostingUnitKey++;
            if (DataSource.hostingUnits.Any(item => hostingUnit.HostingUnitKey == item.HostingUnitKey))
                throw new DuplicateWaitObjectException("This key already exists, DAL error");
            DataSource.hostingUnits.Add(hostingUnit.Clone());
            DataSource_XML.saveConf();
            DataSource_XML.SaveToXML(DataSource.hostingUnits, hostingUnitPath);
        }
        public void UpdateHostingUnit(HostingUnit hostingUnit)
        {
            var list = (from item in DataSource.hostingUnits
                        where hostingUnit.HostingUnitKey == item.HostingUnitKey
                        select item).FirstOrDefault<HostingUnit>();
            if (list == null)
                throw new KeyNotFoundException("Key not found, DAL error");
            DataSource.hostingUnits.Remove(list);
            DataSource.hostingUnits.Add(hostingUnit.Clone());
            DataSource_XML.SaveToXML(DataSource.hostingUnits, hostingUnitPath);
        }
        public void DeleteHostingUnit(HostingUnit hostingUnit)
        {
            var list = (from item in DataSource.hostingUnits
                        where hostingUnit.HostingUnitKey == item.HostingUnitKey
                        select item).FirstOrDefault<HostingUnit>();
            if (list == null)
                throw new KeyNotFoundException("Key not found, DAL error");
            DataSource.hostingUnits.Remove(list);
            DataSource_XML.SaveToXML(DataSource.hostingUnits, hostingUnitPath);
        }
        public List<HostingUnit> GetHostingUnits()
        {
            var list = from item in DataSource.hostingUnits
                       select item.Clone();
            return list.ToList();
        }
        public void AddOrder(Order order)
        {
            var exist = (from item in orderRoot.Elements()
                         where Int32.Parse(item.Element("OrderKey").Value) == order.OrderKey
                         select item).Any();
            if (exist)
                throw new DuplicateWaitObjectException("This key already exists, DAL error");
            XElement ord = new XElement("Order");
            ord.Add(new XElement("HostingUnitKey", order.HostingUnitKey),
                    new XElement("GuestRequestKey", order.GuestRequestKey),
                    new XElement("OrderKey", order.OrderKey),
                    new XElement("Status", order.Status),
                    new XElement("ImageStatus", order.ImageStatus),
                    new XElement("CreateDate", order.CreateDate),
                    new XElement("OrderDate", order.OrderDate));
            DataSource_XML.saveConf();
            orderRoot.Add(ord);
            orderRoot.Save(orderPath);
        }
        public void UpdateOrder(Order order)
        {
            var list = (from item in orderRoot.Elements()
                        where Int32.Parse(item.Element("OrderKey").Value) == order.OrderKey
                        select item).FirstOrDefault();
            if (list == null)
                throw new KeyNotFoundException("Key not found, DAL error");
            list.Element("Status").Value = order.Status.ToString();
            list.Element("ImageStatus").Value = order.ImageStatus;
            if (order.Status == OrderStatus.MAILSENT)
                list.Element("OrderDate").Value = DateTime.Today.ToString();
            orderRoot.Save(orderPath);
        }
        public List<Order> GetOrders()
        {
            var list = from item in orderRoot.Elements()
                       select new Order()
                       {
                           HostingUnitKey = Int32.Parse(item.Element("HostingUnitKey").Value),
                           GuestRequestKey = Int32.Parse(item.Element("GuestRequestKey").Value),
                           OrderKey = Int32.Parse(item.Element("OrderKey").Value),
                           Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), item.Element("Status").Value),
                           ImageStatus = item.Element("ImageStatus").Value,
                           CreateDate = DateTime.Parse(item.Element("CreateDate").Value),
                           OrderDate = DateTime.Parse(item.Element("OrderDate").Value)
                       };
            return list.ToList();
        }
        public void AddHost(Host host)
        {
            if (DataSource.hosts.Any(item => host.Id == item.Id))
                throw new DuplicateWaitObjectException("This key already exists, DAL error");
            DataSource.hosts.Add(host.Clone());
            DataSource_XML.SaveToXML(DataSource.hosts, hostPath);
        }
        public void UpdateHost(Host host)
        {
            var list = (from item in DataSource.hosts
                        where host.Id == item.Id
                        select item).FirstOrDefault<Host>();
            if (list == null)
                throw new KeyNotFoundException("Key not found, DAL error");
            DataSource.hosts.Remove(list);
            DataSource.hosts.Add(host.Clone());
            DataSource_XML.SaveToXML(DataSource.hosts, hostPath);
        }
        public List<Host> GetHosts()
        {
            var list = from item in DataSource.hosts
                       select item.Clone();
            return list.ToList();
        }
        public List<BankAccount> GetBankAccounts()
        {
            var list = from item in DataSource.BankAccounts
                       select item.Clone();
            return list.ToList();
        }
        public static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            const string xmlLocalPath = @"atm.xml";
            WebClient wc = new WebClient();
            bool success = false;
            while (!success)
            {
                try
                {
                    //For some reason, the file he loads from the bank website is not a good file, so we didn't use it
                    /*try
                    {
                        string xmlServerPath =
                       @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                        wc.DownloadFile(xmlServerPath, xmlLocalPath);
                    }
                    catch (Exception)
                    {*/
                    string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                    wc.DownloadFile(xmlServerPath, xmlLocalPath);
                    //}
                    //finally
                    //{
                    wc.Dispose();
                    //}
                    success = true;
                }
                catch (Exception)
                {
                    wc.Dispose();
                    Thread.Sleep(2000);
                }
            }
            XElement bank = XElement.Load(xmlLocalPath);
            List<BankAccount> bankAccounts = new List<BankAccount>();
            bankAccounts = (from item in bank.Elements("ATM")
                            select new BankAccount()
                            {
                                BankNumber = Int32.Parse(item.Element("קוד_בנק").Value),
                                BankName = item.Element("שם_בנק").Value,
                                BranchNumber = Int32.Parse(item.Element("קוד_סניף").Value),
                                BranchAddress = item.Element("כתובת_ה-ATM").Value + ", " + item.Element("ישוב").Value
                            }).ToList();
            foreach (var item1 in bankAccounts)
            {
                bool flag = true;
                foreach (var item2 in DataSource.BankAccounts)
                    if (item1.BranchNumber == item2.BranchNumber && item1.BankNumber == item2.BankNumber)
                        flag = false;
                if (flag)
                    DataSource.BankAccounts.Add(item1.Clone());
            }
            finishBankLoad = true;
        }
    }
}
