using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Common.Static
{
    public static class Net
    {
        public static string[] GetIpAddresses()
        {
            var l = Dns.GetHostEntry(Dns.GetHostName())
                .AddressList
                .Where(i => i.AddressFamily == AddressFamily.InterNetwork)
                .Select(i => i.ToString())
                .ToList();
            l.Add("localhost");
            return l.ToArray();
        }
    }
}
