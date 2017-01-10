using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nancy.Hosting.Self;

namespace ServiceConsole
{
	class Program
	{
		private static string[] _args;
		static void Main(string[] args)
		{
			_args = args;

			//using (var host = new NancyHost(new Uri($"http://{args[0]}:{args[1]}")))
			{
				//host.Start();
				MessageLoop();
			}
		}

		public static void MessageLoop()
		{
			var m = new byte[] {79, 107};
			var socket = new Socket(SocketType.Stream,ProtocolType.Tcp);
			var port = _args.Length > 2 ? Convert.ToInt32(_args[2]) : 10088;
			socket.Bind(new IPEndPoint(IPAddress.Loopback, port));
			Console.WriteLine("Service init");
			socket.Listen(1);
			var accept = socket.Accept();
			accept.Blocking = false;
			Console.WriteLine("Service started");
			while (true)
			{
				var b = new byte[10];
				Console.WriteLine("Service waiting");
				Thread.Sleep(5);
				if (accept.Receive(b)>0)
				{
					Console.WriteLine("Service tick");
					accept.Send(m);
					continue;
				}
				break;
			}
		}
	}
}
