using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Static
{
    public static class Net
    {
        public static string[] GetIpAddresses()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList.Select(i => i.ToString()).ToArray();
        }
    }
}
