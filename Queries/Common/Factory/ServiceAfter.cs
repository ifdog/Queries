using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Common.Static;

namespace Common.Factory
{
    public class ServiceAfter:IDisposable
    {
        private readonly BackgroundWorker _backgroundWorker;
        private readonly Socket _socket;
        private readonly string _path;
        private readonly int _port;
	    private const int SocketPort = 10088;
	    private readonly byte[] _1Bytes = {1};
	    private readonly byte[] _0Bytes = {0};
	    private ProcessStartInfo _start ;

        public ServiceAfter(string path, int port)
        {
            this._path = path;
            this._port = port;
            _socket = new Socket(SocketType.Stream,ProtocolType.Tcp);
	        _backgroundWorker = new BackgroundWorker {WorkerSupportsCancellation = true};
	        _backgroundWorker.DoWork += _backgroundWorker_DoWork;
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _socket.Connect(new IPEndPoint(IPAddress.Loopback, SocketPort));
            while (true)
            {
	            try
	            {
					_socket.Send(_1Bytes);
				}
				catch (SocketException)
	            {
					_socket.Connect(new IPEndPoint(IPAddress.Loopback, SocketPort));
	            }
                Thread.Sleep(3000);
                if (_backgroundWorker.CancellationPending)
                {
                    break;
                }
            }
        }

        public void Run()
        {
            _start = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = "ServiceConsole.exe",
                Arguments = $"{_path} {_port} {SocketPort}",
                Verb = "runas"
            };
			KillProcessExists();
            Process.Start(_start);
            _backgroundWorker.RunWorkerAsync();
        }

		private static void KillProcessExists()
		{
			Process.GetProcessesByName("ServiceConsole")
				.Where(i => i.MainModule.FileName.Equals(System.IO.Path.Combine(Environment.CurrentDirectory, "ServiceConsole.exe")))
				.ForEach(i =>
				{
					i.Kill();
					i.Close();
				});
		}

	    public void Stop()
	    {
		    _backgroundWorker.CancelAsync();
	    }

	    public void Dispose()
	    {
		    KillProcessExists();
	    }
    }
}
