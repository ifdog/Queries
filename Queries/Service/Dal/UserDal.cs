using System;
using System.Linq;
using Common.Static;
using Service.Common.DataBase;
using Service.Common.Factory;
using Service.Model;

namespace Service.Dal
{
	public class UserDal
	{
		private readonly BoxDb<UserModel> _itemDb = new BoxDb<UserModel>();
        private readonly UserFactory _userFactory = new UserFactory();

		public void CreateUser(string userName,string password, string realName, long power)
		{
		    var user =_userFactory.CreateNew(userName, password, realName, power);
		    _itemDb.Insert(user);
		}

		public bool CheckExist(string userName)
		{
			return _itemDb.Select().Any(i => i.UserName.Equals(userName));
		}

		public UserModel FindUserByName(string userName)
		{
			return _itemDb.Select().FirstOrDefault(i => i.UserName.Equals(userName));
		}

		public bool UpdatePassword(string userName, string password)
		{
			var x = _itemDb.Select().FirstOrDefault(i => i.UserName.Equals(userName));
			if (x == null) return false;
			x.Password = password;
			return _itemDb.Update(x);
		}

		public bool UpdateLastAccess(string userName)
		{
			var x = _itemDb.Select().FirstOrDefault(i => i.UserName.Equals(userName));
			if (x == null) return false;
			x.LastAccess = DateTime.Now;
			return _itemDb.Update(x);
		}
	}
}
