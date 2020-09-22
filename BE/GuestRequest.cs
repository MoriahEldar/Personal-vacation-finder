using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class GuestRequest
    {
        private long guestRequestKey = Configuration.GuestRequestKey++;
        public long GuestRequestKey
        {
            get
            {
                return guestRequestKey;
            }
            set
            {
                guestRequestKey = value;
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
        private RequestStatus status = RequestStatus.OPEN;
        public RequestStatus Status 
        { 
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
        private DateTime registrationDate = DateTime.Today;
        public DateTime RegistrationDate 
        {
            get
            {
                return registrationDate;
            }
            set
            {
                registrationDate = value;
            }
        }
        private DateTime entryDate = DateTime.Today.AddDays(1);
        public DateTime EntryDate
        { 
            get
            {
                return entryDate;
            } 
            set
            {
                entryDate = value;
            }
        }
        private DateTime releaseDate = DateTime.Today.AddDays(2);
        public DateTime ReleaseDate
        {
            get
            {
                return releaseDate;
            }
            set
            {
                releaseDate = value;
            }
        }
        public TzimmerArea Area { get; set; }
        public TzimmerType Type { get; set; }
        private int adults;
        public int Adults 
        { 
            get
            {
                return adults;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("You have to have atleast one adult");
                adults = value;
            }
        }
        private int children;
        public int Children
        {
            get
            {
                return children;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Invalid number of children");
                children = value;
            }
        }
        private int babies;
        public int Babies
        {
            get
            {
                return babies;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Invalid number of babies");
                babies = value;
            }
        }
        public Options Pool { get; set; }
        public Options Garden { get; set; }
        public Options ChildrenAttractions { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}