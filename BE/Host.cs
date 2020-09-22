using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class Host : IComparable<Host>
    {
        private string id;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                string zero = "";
                for (int i = value.Length; i < 9; i++)
                    zero += "0";
                string tempId = zero + value;
                if (tempId.Length != 9)
                    throw new ArgumentException("Invalid id");
                int sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int x = (i % 2 + 1) * Int32.Parse(tempId[i].ToString());
                    sum += x % 10 + x / 10;
                }
                if(sum % 10 != 0)
                    throw new ArgumentException("Invalid id");
                id = value;
            }
        }
        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                for (int i = 0; i < value.Length; i++)
                    if (!(value[i] >= 'a' && value[i] <= 'z') && !(value[i] >= 'A' && value[i] <= 'Z') && !(value[i] >= 'א' && value[i] <= 'ת'))
                        throw new ArgumentException("Can only include letters");
                firstName = value;
            }
        }
        private string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                for (int i = 0; i < value.Length; i++)
                    if (!(value[i] >= 'a' && value[i] <= 'z') && !(value[i] >= 'A' && value[i] <= 'Z') && !(value[i] >= 'א' && value[i] <= 'ת'))
                        throw new ArgumentException("Can only include letters");
                lastName = value;
            }
        }
        private string phoneNumber;
        public string PhoneNumber 
        { 
            get
            {
                return phoneNumber;
            }
            set
            {
                for (int i = 0; i < value.Length; i++)
                    if(value[i] < '0' || value[i] > '9')
                        throw new ArgumentException("Can only include numbers");
                if(value[0] != '0')
                    throw new ArgumentException("Invalid phone number");
                if (value.Length != 9 && value.Length != 10)
                    throw new ArgumentException("Invalid phone number");
                if (value.Length == 9)
                    if(value[1] == '0' || value[1] == '1' || value[1] == '5' || value[1] == '6' || value[1] == '7')
                        throw new ArgumentException("Invalid phone number");
                if (value.Length == 10)
                    if (value[1] != '5')
                        throw new ArgumentException("Invalid phone number");
                phoneNumber = value;
            }
        }
        private string mail;
        public string Mail
        {
            get
            {
                return mail;
            }
            set
            {
                try
                {
                    MailAddress x = new MailAddress(value);
                }
                catch (Exception)
                {
                    throw new ArgumentException("Not a mail");
                }
                mail = value;
            }
        }
        public BankAccount HostBankAccount { get; set; }
        public bool CollectionClearance { get; set; }

        private double commission = 0; //commission that havn't been collected
        public double Commission
        {
            get
            {
                return commission;
            }
            set
            {
                commission = value;
            }
        }
        public string Password { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }

        public int CompareTo(Host obj)
        {
            return Id.CompareTo(obj.Id);
        }
    }
}
