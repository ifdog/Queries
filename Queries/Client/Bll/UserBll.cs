using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Dal;
using Client.Model;
using Common.Enums;

namespace Client.Bll
{
	public class UserBll
	{
		private readonly RunContext _run;
		private readonly UserDal _userDal;
		public UserBll(RunContext run, UserDal userDal)
		{
			this._run = run;
			this._userDal = userDal;
		}

		public ResultCode Login(string name, string password)
		{
			return _userDal.Login(name, password);
		}

		public ResultCode Register(string name, string password)
		{
			return _userDal.Register(name, password);
		}

		public ResultCode UpdatePassword(string name, string password)
		{
			return _userDal.UpdatePassword(name, password);
		}

	}
}
