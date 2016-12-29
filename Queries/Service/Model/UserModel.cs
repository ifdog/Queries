using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Model.Base;

namespace Service.Model
{
	public class UserModel:Item
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string RealName { get; set; }
		public long Power { get; set; }
		public DateTime LastAccess { get; set; }
		public DateTime RegDate { get; set; }
		public string Tag { get; set; }
	}
}
