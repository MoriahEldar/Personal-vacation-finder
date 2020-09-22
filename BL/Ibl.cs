using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface Ibl
    {
        void AddRequest(GuestRequest guestRequest);
        void UpdateRequest(GuestRequest guestRequest);
        List<GuestRequest> GetGuestRequests();
        void AddHostingUnit(HostingUnit hostingUnit);
        void DeleteHostingUnit(HostingUnit hostingUnit);
        void UpdateHostingUnit(HostingUnit hostingUnitKey);
        List<HostingUnit> GetHostingUnits();
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        List<Order> GetOrders();
        void AddHost(Host host);
        void UpdateHost(Host host);
        List<Host> GetHosts();
        List<BankAccount> GetBankAccounts();
        List<HostingUnit> Available(DateTime date, int numDays);
        int NumDaysPast(DateTime start, DateTime end);
        int NumDaysPast(DateTime start);
        List<Order> Expired(int numDays, OrderStatus status);
        List<GuestRequest> TrueCondition(Predicate<GuestRequest> condition);
        int NumReceivedOrders(GuestRequest req);
        int NumUnitOrders(HostingUnit unit, OrderStatus status = OrderStatus.NOTYETCARE);
        bool LoadBanksFinished();
        List<GuestRequest> Goodreq (HostingUnit unit);
        List<HostingUnit> HostsUnits(Host host);
        List<IGrouping<TzimmerArea, GuestRequest>> ReqsByArea();
        List<IGrouping<int, GuestRequest>> ReqsByNumGuests();
        List<IGrouping<int, string>> HostByNumUnits();
        List<IGrouping<TzimmerArea, HostingUnit>> UnitsByArea();
    }
}