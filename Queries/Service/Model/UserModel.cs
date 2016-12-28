using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model
{
	public class UserModel
	{
		public long Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public long Power { get; set; }
		public long LastAccess { get; set; }
		public long WhenReg { get; set; }
		public string Tag { get; set; }
	}
}
