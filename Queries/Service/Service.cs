using System;
using Nancy.Hosting.Self;
using NLog;
using NLog.Config;
using NLog.Targets;

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
            NlogConfig();
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

        public void NlogConfig()
        {
            var config = new LoggingConfiguration();
            var consoleTarget = new DebuggerTarget();
            var fileTarget = new FileTarget
            {
                FileName = "${basedir}/Service.log",
            };
            config.AddTarget("Console",consoleTarget);
            config.AddTarget("File",fileTarget);
            config.AddRule(LogLevel.Trace,LogLevel.Fatal,consoleTarget);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, fileTarget);
            LogManager.Configuration = config;
        }

        public void Dispose()
        {
            if (!this.Started) return;
            _host.Stop();
            this.Started = false;
        }
    }
}