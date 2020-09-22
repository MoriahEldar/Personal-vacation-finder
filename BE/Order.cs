using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]
    public class Order : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public long HostingUnitKey { get; set; }
        public long GuestRequestKey { get; set; }
        private long orderKey = Configuration.OrderKey++;
        public long OrderKey
        {
            get
            {
                return orderKey;
            }
            set
            {
                orderKey = value;
            }
        }
        private OrderStatus status = OrderStatus.NOTYETCARE;
        public OrderStatus Status
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
        private string imageStatus = "resources/gmail.png";
        public string ImageStatus
        {
            get
            {
                return imageStatus;
            }
            set
            {
                imageStatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImageStatus"));
            }
        }
        private DateTime createDate = DateTime.Today;
        public DateTime CreateDate
        {
            get
            {
                return createDate;
            }
            set
            {
                createDate = value;
            }
        }
        public DateTime OrderDate { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}