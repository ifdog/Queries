using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Common.Static;
using Common.Structure;
using Service.Dal;
using Service.Model;

namespace Service.Bll
{
	public class UserBll
	{
		readonly UserDal _userDal = new UserDal();
		readonly TokenHelper _tokenHelper = new TokenHelper();
		public Task<BllResult> Register(UserPostBody userPostBody)
		{
			return Task.Run(() =>
			{
				if (userPostBody?.Action == null || userPostBody.UserName == null || userPostBody.Password == null)
				{
					return new BllResult(ResultCode.JsonParseFail);
				}
				if (!userPostBody.Action.Equals("Register"))
				{
					return new BllResult(ResultCode.ActionNotCorresponding);
				}
				if (string.IsNullOrEmpty(userPostBody.Password) || string.IsNullOrEmpty(userPostBody.UserName))
				{
					return new BllResult(ResultCode.InvalidParameter);
				}
				try
				{
					if (_userDal.CheckExist(userPostBody.UserName))
						return new BllResult(ResultCode.UserNameAlreadyExist);
					_userDal.CreateUser(userPostBody.UserName, userPostBody.Password);
					Log.Here($"User {userPostBody.UserName} registered");
					return new BllResult(ResultCode.Ok);
				}
				catch (Exception e)
				{
					Log.Here(e);
					return new BllResult(e);
				}
			});
		}

		public Task<BllResult> Login(UserPostBody userPostBody)
		{
			return Task.Run(() =>
			{
				if (userPostBody?.Action == null || userPostBody.UserName == null || userPostBody.Password == null)
				{
					return new BllResult(ResultCode.JsonParseFail);
				}
				if (!userPostBody.Action.Equals("Login"))
				{
					return new BllResult(ResultCode.ActionNotCorresponding);
				}
				if (string.IsNullOrEmpty(userPostBody.Password) || string.IsNullOrEmpty(userPostBody.UserName))
				{
					return new BllResult(ResultCode.ActionNotCorresponding);
				}
				try
				{
					if (!_userDal.CheckExist(userPostBody.UserName))
						return new BllResult(ResultCode.UserNotExist);
					if (!_userDal.FindUserByName(userPostBody.UserName).Password.Equals(userPostBody.Password))
						return new BllResult(ResultCode.InvalidUserNameOrPassword);
					_userDal.UpdateLastAccess(userPostBody.UserName);
					var result = new BllResult(ResultCode.Ok);
					result.Token = _tokenHelper.Create(_userDal.FindUserByName(userPostBody.UserName).Id, new TimeSpan(1, 0, 0, 0));
					Log.Here($"User {userPostBody.UserName} login");
					return result;
				}
				catch (Exception e)
				{
					Log.Here(e);
					return new BllResult(e);
				}
			});
		}

		public Task<BllResult> UpdatePassword(UserPostBody userPostBody, string token)
		{
			return Task.Run(() =>
			{
				if (userPostBody?.Action == null || userPostBody.UserName == null || userPostBody.Password == null)
				{
					return new BllResult(ResultCode.JsonParseFail);
				}
				if (!userPostBody.Action.Equals("UpdatePassword"))
				{
					return new BllResult(ResultCode.ActionNotCorresponding);
				}
				if (string.IsNullOrEmpty(userPostBody.Password) || string.IsNullOrEmpty(userPostBody.UserName))
				{
					return new BllResult(ResultCode.InvalidParameter);
				}
				if (string.IsNullOrEmpty(token))
				{
					return new BllResult(ResultCode.NotLoggedIn);
				}
				if (!_tokenHelper.Validate(token))
					return new BllResult(ResultCode.TokenExpired);
				try
				{
					if (!_userDal.CheckExist(userPostBody.UserName))
						return new BllResult(ResultCode.UserNotExist);
					_userDal.UpdatePassword(userPostBody.UserName, userPostBody.Password);
					Log.Here($"User {userPostBody.UserName} updated password");
					return new BllResult(ResultCode.Ok);
				}
				catch (Exception e)
				{
					Log.Here(e);
					return new BllResult(e);
				}
			});

		}
	}
}
