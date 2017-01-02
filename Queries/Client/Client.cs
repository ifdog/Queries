using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Client
{
    public class Client
    {
        private RestClient _restClient;
        private 

        public Client(string serviceDomain)
        {
            _restClient = new RestClient(serviceDomain);
            _restClient.CookieContainer = new CookieContainer();
        }
    }
}
