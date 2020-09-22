using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface Idal
    {
        bool FinishBankLoad { get; set; }
        void AddRequest(GuestRequest guestRequest);
        void UpdateRequest(GuestRequest guestRequest);
        List<GuestRequest> GetGuestRequests();
        void AddHostingUnit(HostingUnit hostingUnit);
        void DeleteHostingUnit(HostingUnit hostingUnit);
        void UpdateHostingUnit(HostingUnit hostingUnit);
        List<HostingUnit> GetHostingUnits();
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        List<Order> GetOrders();
        void AddHost(Host host);
        void UpdateHost(Host host);
        List<Host> GetHosts();
        List<BankAccount> GetBankAccounts();
    }
}