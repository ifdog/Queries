using System;
using Nancy.Hosting.Self;

namespace Service
{
    public class Service : IDisposable
    {
        private readonly NancyHost _host;
        public bool Started { get; private set; }

        public Service(string hostingPath,int hostingPort)
        {
            var uri = new Uri($"http://{hostingPath}:{hostingPort}");
            _host = new NancyHost(uri);
        }

        public Service StartHosting()
        {
            if (!this.Started)
            {
                _host.Start();
                this.Started = true;
            }
            return this;
        }

        public Service StopHosting()
        {
            if (this.Started)
            {
                _host.Stop();
                this.Started = false;
            }
            return this;
        }

        public void Dispose()
        {
            if (!this.Started) return;
            _host.Stop();
            this.Started = false;
        }
    }
}