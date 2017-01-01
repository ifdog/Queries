using System;
using Nancy.Hosting.Self;

namespace Service
{
    public class Service : IDisposable
    {
        private readonly NancyHost _host;
        public bool Started { get; private set; }

        public Service(string hostingPath)
        {
            var uri = new Uri(hostingPath);
            _host = new NancyHost(uri);
        }

        public void StartHosting()
        {
            if (this.Started) return;
            _host.Start();
            this.Started = true;
        }

        public void StopHosting()
        {
            if (!this.Started) return;
            _host.Stop();
            this.Started = false;
        }

        public void Dispose()
        {
            if (!this.Started) return;
            _host.Stop();
            this.Started = false;
        }
    }
}