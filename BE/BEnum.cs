using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum TzimmerType { TZIMER, APARTMENT, HOTEL, CAMP, CARAVAN };
    public enum TzimmerArea { NORTH, HAIFA, TELAVIV, CENTER, JERUSALEM, SOUTH };
    public enum OrderStatus { NOTYETCARE, MAILSENT, CLIENTNOCARE, CARE };
    public enum RequestStatus { OPEN, CLOSED, TIMEOUT };
    public enum Options { YES, OPTIONAL, NO };
}