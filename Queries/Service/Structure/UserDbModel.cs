using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Structure;

namespace Service.Structure
{
	public class UserDbModel
	{
		public byte[] Id { get; set; }
		public UserModel User { get; set; }
	}
}
