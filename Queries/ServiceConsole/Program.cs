using System;
using System.Net;
using System.Net.Sockets;
using Nancy.Hosting.Self;

namespace ServiceConsole
{
    class Program
    {
        private static string[] _args;

        static void Main(string[] args)
        {
            _args = args;

            using (var host = new NancyHost(new Uri($"http://{args[0]}:{args[1]}")))
            {
                host.Start();
                MessageLoop();
            }
        }

        public static void MessageLoop()
        {
            var m = new byte[] {79, 107};
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            var port = _args.Length > 2 ? Convert.ToInt32(_args[2]) : 10088;
            socket.Bind(new IPEndPoint(IPAddress.Loopback, port));
            socket.Listen(1);
            var accept = socket.Accept();
            accept.ReceiveTimeout = 5000;
            while (true)
            {
                var b = new byte[10];
	            try
	            {
		            accept.Receive(b);
		            Console.WriteLine("Tick.");
	            }
	            catch (TimeoutException e)
	            {
		            break;
	            }
	            catch (SocketException e)
	            {
		            break;
	            }
				
			}
		}
	}
}
