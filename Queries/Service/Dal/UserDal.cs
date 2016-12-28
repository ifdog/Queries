using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Model;

namespace Service.Dal
{
	public class UserDal
	{
		readonly UserContext _userContext = new UserContext();

		public void CreateUser(string userName, string password, long power = 2)
		{
			if (userName == string.Empty || password == string.Empty) return;
			_userContext.Users.Add(new UserModel
			{
				LastAccess = -1,
				Password = password,
				Power = 2,
				Tag = string.Empty,
				UserName = userName,
				WhenReg = UnixTimeStamp.ToUnixTimaStamp(DateTime.Now)
			});
			_userContext.SaveChanges();
		}

		public bool CheckExist(string userName)
		{
			return _userContext.Users.Any(i => i.UserName.Equals(userName));
		}

		public UserModel FindUserByName(string userName)
		{
			return _userContext.Users.FirstOrDefault(i => i.UserName.Equals(userName));
		}

		public UserModel FindUserById(long userId)
		{
			return _userContext.Users.FirstOrDefault(i => i.Id == userId);
		}

		public void UpdatePassword(string userName, string password)
		{
			var m = _userContext.Users.FirstOrDefault(i => i.UserName.Equals(userName));
			if (m == null) return;
			m.Password = password;
			_userContext.SaveChanges();
		}

		public void UpdateLastAccess(string userName)
		{
			var m = _userContext.Users.FirstOrDefault(i => i.UserName.Equals(userName));
			if (m == null) return;
			m.LastAccess = UnixTimeStamp.ToUnixTimaStamp(DateTime.Now);
			_userContext.SaveChanges();
		}

		public void UpdateLastAccess(long userId)
		{
			var m = _userContext.Users.FirstOrDefault(i => i.Id == userId);
			if (m == null) return;
			m.LastAccess = UnixTimeStamp.ToUnixTimaStamp(DateTime.Now);
			_userContext.SaveChanges();
		}
	}
}
