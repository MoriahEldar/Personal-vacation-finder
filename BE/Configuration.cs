using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
 
namespace BE
{
    public class Configuration
    {
        public static long GuestRequestKey = 10000000;
        public static long HostingUnitKey = 10000000;
        public static long OrderKey = 10000000;
        public static float Commission = 10;
        public static MailAddress Mail = new MailAddress("wertsite4u@gmail.com");
        public static string Password = "whatdoyouwannado";
    }
}