using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BE;
using DAL;
using System.Threading;

namespace BL
{
    class Bl_imp : Ibl
    {
        Idal data = FactorySingletonDal.Instance;
        public Bl_imp()
        {
            Thread change = new Thread(new ThreadStart(changeStatus));
            change.Start();
        }
        void changeStatus()
        {
            while (true)
            {
                foreach (var order in Expired(30, OrderStatus.MAILSENT))
                {
                    order.Status = OrderStatus.CLIENTNOCARE;
                    data.UpdateOrder(order);
                }
                TimeSpan oneDay = TimeSpan.FromDays(1);
                Thread.Sleep(oneDay);
            }
        }
        public void AddRequest(GuestRequest guestRequest)
        {
            if (guestRequest.EntryDate >= guestRequest.ReleaseDate)
                throw new InvalidTimeZoneException("You need to register for atleast one night, BL error");
            try
            {
                data.AddRequest(guestRequest);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void UpdateRequest(GuestRequest guestRequest)
        {
            if (guestRequest.Status == RequestStatus.OPEN)
                throw new InvalidEnumArgumentException("Can not change status to open, BL error");
            var list = (from item in data.GetGuestRequests()
                        where guestRequest.GuestRequestKey == item.GuestRequestKey
                        select item).FirstOrDefault<GuestRequest>();
            if (list == null)
                throw new KeyNotFoundException("Key not found, BL error");
            if (list.Status == RequestStatus.CLOSED)
                throw new InvalidEnumArgumentException("Can not change closed status, BL error");
            if (list.Status == RequestStatus.TIMEOUT)
                throw new InvalidEnumArgumentException("Can not change timeout status, BL error");
            try
            {
                data.UpdateRequest(guestRequest);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<GuestRequest> GetGuestRequests()
        {
            return data.GetGuestRequests();
        }
        public void AddHostingUnit(HostingUnit hostingUnit)
        {
            var host = (from item in data.GetHosts()
                        where item.Id == hostingUnit.OwnerId
                        select item).FirstOrDefault();
            if (host == null)
                throw new ArgumentNullException("No such host, Bl error");
            try
            {
                data.AddHostingUnit(hostingUnit);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void DeleteHostingUnit(HostingUnit hostingUnit)
        {
            if (NumUnitOrders(hostingUnit) > 0)
                throw new InvalidOperationException("There is an order that is accosiated with this hosting unit, BL error");
            try
            {
                data.DeleteHostingUnit(hostingUnit);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void UpdateHostingUnit(HostingUnit hostingUnit)
        {
            var unit = (from item in data.GetHostingUnits()
                        where hostingUnit.HostingUnitKey == item.HostingUnitKey
                        select item).FirstOrDefault<HostingUnit>();
            if (unit == null)
                throw new KeyNotFoundException("Hosting unit key not found, BL error");
            try
            {
                data.UpdateHostingUnit(hostingUnit);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<HostingUnit> GetHostingUnits()
        {
            return data.GetHostingUnits();
        }
        public void AddOrder(Order order)
        {
            var req = (from item in data.GetGuestRequests()
                       where order.GuestRequestKey == item.GuestRequestKey
                       select item).FirstOrDefault<GuestRequest>();
            var unit = (from item in data.GetHostingUnits()
                        where order.HostingUnitKey == item.HostingUnitKey
                        select item).FirstOrDefault<HostingUnit>();
            if (unit == null)
                throw new KeyNotFoundException("Hosting unit key was not found, BL error");
            if (req == null)
                throw new KeyNotFoundException("Guest request key was not found, BL error");
            for (DateTime i = req.EntryDate; i < req.ReleaseDate; i = i.AddDays(1))
                if (unit.Diary[i.Month - 1, i.Day - 1])
                    throw new InvalidOperationException("There are accupied days, BL error");
            try
            {
                data.AddOrder(order);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void UpdateOrder(Order order)
        {
            var unit = (from item in data.GetHostingUnits()
                        where order.HostingUnitKey == item.HostingUnitKey
                        select item).FirstOrDefault<HostingUnit>();
            var host = (from item in data.GetHosts()
                        where unit.OwnerId == item.Id
                        select item).FirstOrDefault<Host>();
            var lastOrder = (from item in data.GetOrders()
                             where order.OrderKey == item.OrderKey
                             select item).FirstOrDefault<Order>();
            var req = (from item in data.GetGuestRequests()
                       where order.GuestRequestKey == item.GuestRequestKey
                       select item).FirstOrDefault<GuestRequest>();
            if (lastOrder == null)
                throw new KeyNotFoundException("Order key not found, BL error");
            if (order.Status == OrderStatus.NOTYETCARE && lastOrder.Status != OrderStatus.MAILSENT)
                throw new InvalidEnumArgumentException("Can not change status to notYetCare, BL error");
            if (lastOrder.Status == OrderStatus.CARE)
                throw new InvalidEnumArgumentException("Can not change care status, BL error");
            if (order.Status == OrderStatus.MAILSENT && lastOrder.Status != OrderStatus.NOTYETCARE)
                throw new InvalidEnumArgumentException("Can change status to mailSent only from notYetCare status, BL error");
            if (order.Status == OrderStatus.MAILSENT && !host.CollectionClearance)
                throw new InvalidOperationException("Can not change status to mailSent if the host didn't approve collection clearance, BL error");
            if (order.Status == OrderStatus.CARE)
                for (DateTime i = req.EntryDate; i < req.ReleaseDate; i = i.AddDays(1))
                    if (unit.Diary[i.Month - 1, i.Day - 1])
                        throw new InvalidOperationException("Can not change status to care, the unit is accupied in these dates");
            try
            {
                data.UpdateOrder(order);
            }
            catch (Exception e)
            {
                throw e;
            }
            if (order.Status == OrderStatus.CARE)
            {
                host.Commission += Configuration.Commission * NumDaysPast(req.EntryDate, req.ReleaseDate);
                UpdateHost(host);
                for (DateTime i = req.EntryDate; i < req.ReleaseDate; i = i.AddDays(1))
                    unit.Diary[i.Month - 1, i.Day - 1] = true;
                UpdateHostingUnit(unit);
                req.Status = RequestStatus.CLOSED;
                UpdateRequest(req);
                foreach (var ord in data.GetOrders())
                    if (ord.GuestRequestKey == order.GuestRequestKey && ord.OrderKey != order.OrderKey)
                    {
                        ord.Status = OrderStatus.CLIENTNOCARE;
                        UpdateOrder(ord);
                    }
            }
        }
        public List<Order> GetOrders()
        {
            return data.GetOrders();
        }
        public void AddHost(Host host)
        {
            try
            {
                data.AddHost(host);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void UpdateHost(Host host)
        {
            var thisHost = (from item in data.GetHosts()
                            where host.Id == item.Id
                            select item).FirstOrDefault();
            if (thisHost == null)
                throw new KeyNotFoundException("Host key not found, BL error");
            var hostingUnits = from item in data.GetHostingUnits()
                               where item.OwnerId == host.Id
                               select item;
            var orders = (from item in data.GetOrders()
                          from unit in hostingUnits
                          where item.HostingUnitKey == unit.HostingUnitKey
                          select item).Any();
            if (thisHost.CollectionClearance && !host.CollectionClearance)
            {
                if (orders)
                    throw new InvalidOperationException("There is an order that is accosiated with this host, BL error");
            }
            try
            {
                data.UpdateHost(host);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Host> GetHosts()
        {
            return data.GetHosts();
        }
        public List<BankAccount> GetBankAccounts()
        {
            return data.GetBankAccounts();
        }
        public List<HostingUnit> Available(DateTime date, int numDays)
        {
            if (numDays <= 0)
                throw new IndexOutOfRangeException("The number of days should be more the 0, BL error");
            Predicate<HostingUnit> isFree = delegate (HostingUnit funcUnit)
            {
                for (int i = 0; i < numDays; i++)
                    if (funcUnit.Diary[date.AddDays(i).Month - 1, date.AddDays(i).Day - 1])
                        return false;
                return true;
            };
            var list = from unit in data.GetHostingUnits()
                       where isFree(unit)
                       select unit;
            return list.ToList();
        }
        public int NumDaysPast(DateTime start, DateTime end)
        {
            if (start > end)
                throw new IndexOutOfRangeException("The start day should be before the end day, BL error");
            return end.Subtract(start).Days;
        }
        public int NumDaysPast(DateTime start)
        {
            return NumDaysPast(start, DateTime.Today);
        }
        public List<Order> Expired(int numDays, OrderStatus status)
        {
            if (numDays < 0)
                throw new IndexOutOfRangeException("The number of days should not be negative, BL error");
            if (status == OrderStatus.NOTYETCARE)
            {
                var list = from order in data.GetOrders()
                           where (order.Status == OrderStatus.MAILSENT || order.Status == OrderStatus.NOTYETCARE) && NumDaysPast(order.CreateDate) >= numDays
                           select order;
                return list.ToList();
            }
            if (status == OrderStatus.MAILSENT)
            {
                var list = from order in data.GetOrders()
                           where order.Status == OrderStatus.MAILSENT && NumDaysPast(order.OrderDate) >= numDays
                           select order;
                return list.ToList();
            }
            return null;
        }
        public List<GuestRequest> TrueCondition(Predicate<GuestRequest> condition)
        {
            var list = from req in data.GetGuestRequests()
                       where condition(req)
                       select req;
            return list.ToList();
        }
        public int NumReceivedOrders(GuestRequest req)
        {
            return data.GetOrders().Count(order => order.GuestRequestKey == req.GuestRequestKey);
        }
        public int NumUnitOrders(HostingUnit unit, OrderStatus status = OrderStatus.NOTYETCARE)
        {
            if (status == OrderStatus.NOTYETCARE)
                return data.GetOrders().Count(order => order.HostingUnitKey == unit.HostingUnitKey);
            if (status == OrderStatus.CARE)
                return data.GetOrders().Count(order => order.HostingUnitKey == unit.HostingUnitKey && order.Status == OrderStatus.CARE);
            throw new InvalidEnumArgumentException("Invalid Status, Bl error");
        }
        public bool LoadBanksFinished()
        {
            return data.FinishBankLoad;
        }
        public List<GuestRequest> Goodreq(HostingUnit unit)
        {
            Func<GuestRequest, bool> compare = req =>
            {
                if (req.Status != RequestStatus.OPEN)
                    return false;
                var order = (from item in data.GetOrders()
                             where item.HostingUnitKey == unit.HostingUnitKey && item.GuestRequestKey == req.GuestRequestKey
                             select item).Any();
                if (order)
                    return false;
                if (unit.Area != req.Area || unit.Type != req.Type)
                    return false;
                if (unit.AdultsBeds * unit.NumRooms < req.Adults)
                    return false;
                if (unit.ChildrenBeds * unit.NumRooms + unit.AdultsBeds * unit.NumRooms - req.Adults < req.Children) // Children can use adults beds too, so we checked how many adults bed are available also for children 
                    return false;
                if (unit.Cribs < req.Babies)
                    return false;
                if (!unit.Pool && req.Pool == Options.YES || unit.Pool && req.Pool == Options.NO)
                    return false;
                if (!unit.Garden && req.Garden == Options.YES || unit.Garden && req.Garden == Options.NO)
                    return false;
                if (!unit.ChildrenAttractions && req.ChildrenAttractions == Options.YES || unit.ChildrenAttractions && req.ChildrenAttractions == Options.NO)
                    return false;
                for (DateTime i = req.EntryDate; i < req.ReleaseDate; i = i.AddDays(1))
                    if (unit.Diary[i.Month - 1, i.Day - 1])
                        return false;
                return true;
            };
            var reqs = from item in data.GetGuestRequests()
                       where compare(item)
                       select item;
            return reqs.ToList();
        }
        public List<HostingUnit> HostsUnits(Host host)
        {
            var list = from item in data.GetHostingUnits()
                       where item.OwnerId == host.Id
                       select item;
            return list.ToList();
        }
        public List<IGrouping<TzimmerArea, GuestRequest>> ReqsByArea()
        {
            var list = from req in data.GetGuestRequests()
                       group req by req.Area into newGroup
                       orderby newGroup.Key
                       select newGroup;
            return list.ToList();
        }
        public List<IGrouping<int, GuestRequest>> ReqsByNumGuests()
        {
            var list = from req in data.GetGuestRequests()
                       let num = req.Adults + req.Children + req.Babies
                       group req by num into newGroup
                       orderby newGroup.Key
                       select newGroup;
            return list.ToList();
        }
        public List<IGrouping<int, string>> HostByNumUnits()
        {
            var tempList = from unit in data.GetHostingUnits()
                           group unit by unit.OwnerId into tempGroup
                           orderby tempGroup.Key
                           select tempGroup;
            var list = from owner in tempList
                       group owner.Key by owner.Count() into newGroup
                       orderby newGroup.Key
                       select newGroup;
            return list.ToList();
        }
        public List<IGrouping<TzimmerArea, HostingUnit>> UnitsByArea()
        {
            var list = from unit in data.GetHostingUnits()
                       group unit by unit.Area into newGroup
                       orderby newGroup.Key
                       select newGroup;
            return list.ToList();
        }
    }
}