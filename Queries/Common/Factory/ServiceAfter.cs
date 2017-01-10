using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NPOI.HPSF;

namespace Common.Factory
{
    public class ServiceAfter
    {
        private BackgroundWorker _backgroundWorker;
        private Socket socket;
        private string path;
        private int port;
        private int socketPort;
        private byte[] bytes = {1};
        ProcessStartInfo start ;

        public ServiceAfter(string path, int port)
        {
            this.path = path;
            this.port = port;
            socket = new Socket(SocketType.Stream,ProtocolType.Tcp);
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            
            
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            socket.Connect(IPAddress.Parse("127.0.0.1"),socketPort);
            while (true)
            {
                socket.Send(bytes);
                Thread.Sleep(2000);
                if (_backgroundWorker.CancellationPending)
                {
                    break;
                }
            }
        }

        public void Run()
        {
            start = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = "ServiceConsole.exe",
                Arguments = $"{path} {port}",
                Verb = "runas"
            };
            Process.Start(start);
            _backgroundWorker.RunWorkerAsync();
        }
    }
}
