﻿using System;
using System.Net;
using System.Resources;
using System.Text;
using Client.Bll;
using RestSharp;

namespace Client
{
    public class Client
    {
        private readonly RestClient _restClient;
        private ResourceManager _resourceManager;

        public UserBll User { get; private set; }
        public ItemBll Item { get; private set; }

        public Client(string serviceDomain)
        {
            _resourceManager = new ResourceManager("Client.Resources", typeof(Client).Assembly);
            _restClient = new RestClient(serviceDomain)
            {
                CookieContainer = new CookieContainer(),
                Encoding = Encoding.UTF8,
                BaseUrl = new Uri(serviceDomain),
                FollowRedirects = true,
                UserAgent = _resourceManager.GetString("UserAgent"),
                Timeout = 10000
            };
            User = new UserBll(_restClient);
            Item = new ItemBll(_restClient);
        }

    }
}
