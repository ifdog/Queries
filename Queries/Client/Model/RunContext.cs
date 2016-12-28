using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
	public class RunContext
	{
		private readonly ConfigHelper _configHelper = new ConfigHelper();

		public Configuration Configuration { get; set; }
		public string UserName { get; set; }
		public DataRow OperatingDataRow { get; set; }

		public RunContext()
		{
			Configuration = _configHelper.Load();
		}
	}
}
