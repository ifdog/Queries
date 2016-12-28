using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
	public class Configuration
	{
		public int Mode { get; set; } = 0;
		public string HostingPath { get; set; }
		public string RequestingPath { get; set; }
	}
}
