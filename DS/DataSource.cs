using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public class DataSource
    {
        public static List<GuestRequest> guestRequests = new List<GuestRequest>();
        public static List<HostingUnit> hostingUnits = new List<HostingUnit>();
        public static List<Host> hosts = new List<Host>();
        public static List<BankAccount> BankAccounts = new List<BankAccount>();
    }
}