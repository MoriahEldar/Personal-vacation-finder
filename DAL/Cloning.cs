using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public static class Cloning
    {
        public static GuestRequest Clone(this GuestRequest req)
        {
            GuestRequest copy = new GuestRequest
            {
                GuestRequestKey = req.GuestRequestKey,
                FirstName = req.FirstName,
                LastName = req.LastName,
                Mail = req.Mail,
                Status = req.Status,
                RegistrationDate = req.RegistrationDate,
                EntryDate = req.EntryDate,
                ReleaseDate = req.ReleaseDate,
                Area = req.Area,
                Type = req.Type,
                Adults = req.Adults,
                Children = req.Children,
                Babies = req.Babies,
                Pool = req.Pool,
                Garden = req.Garden,
                ChildrenAttractions = req.ChildrenAttractions
            };
            return copy;
        }
        public static Host Clone(this Host host)
        {
            Host copy = new Host
            {
                Id = host.Id,
                FirstName = host.FirstName,
                LastName = host.LastName,
                PhoneNumber = host.PhoneNumber,
                Mail = host.Mail,
                HostBankAccount = host.HostBankAccount,
                CollectionClearance = host.CollectionClearance,
                Commission = host.Commission,
                Password = host.Password
            };
            return copy;
        }
        public static HostingUnit Clone(this HostingUnit unit)
        {
            HostingUnit copy = new HostingUnit
            {
                HostingUnitKey = unit.HostingUnitKey,
                OwnerId = unit.OwnerId,
                HostingUnitName = unit.HostingUnitName,
                Description = unit.Description,
                NumRooms = unit.NumRooms,
                ChildrenBeds = unit.ChildrenBeds,
                Cribs = unit.Cribs,
                AdultsBeds = unit.AdultsBeds,
                Address = unit.Address,
                Area = unit.Area,
                Type = unit.Type,
                Pool = unit.Pool,
                Garden = unit.Garden,
                ChildrenAttractions = unit.ChildrenAttractions,
                Diary = new bool[12, 31]
            };
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 31; j++)
                    copy.Diary[i, j] = unit.Diary[i, j];
            return copy;
        }
        public static Order Clone(this Order order)
        {
            Order copy = new Order()
            {
                HostingUnitKey = order.HostingUnitKey,
                GuestRequestKey = order.GuestRequestKey,
                OrderKey = order.OrderKey,
                Status = order.Status,
                ImageStatus = order.ImageStatus,
                CreateDate = order.CreateDate,
                OrderDate = order.OrderDate
            };
            return copy;
        }
        public static BankAccount Clone(this BankAccount bankAccount)
        {
            BankAccount copy = new BankAccount()
            {
                BankNumber = bankAccount.BankNumber,
                BankName = bankAccount.BankName,
                BranchNumber = bankAccount.BranchNumber,
                BranchAddress = bankAccount.BranchAddress,
                BankAccountNumber = bankAccount.BankAccountNumber
            };
            return copy;
        }
    }
}