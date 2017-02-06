using System;
using System.Threading.Tasks;
using Client.Dal;
using Common.Enums;
using Common.Factory;
using Common.Structure;
using Common.Structure.Base;
using RestSharp;

namespace Client.Bll
{
	public class UserBll
	{
		private readonly UserDal _userDal;
		private readonly ResultFactory ResultFactory = new ResultFactory();

		public UserBll(RestClient restClient)
		{
			this._userDal = new UserDal(restClient);
		}

		public Task<BaseResult> Login(UserModel userModel)
		{
			if (string.IsNullOrWhiteSpace(userModel.UserName))
			{
				return Task.Run<BaseResult>(() => ResultFactory.CreateUserResult(ResultCode.UserNameShouldNotBeWhiteSpace));
			}
			if (string.IsNullOrEmpty(userModel.UserName))
			{
				return Task.Run<BaseResult>(() => ResultFactory.CreateUserResult(ResultCode.UserNameShouldNotBeEmpty));
			}
			if (string.IsNullOrEmpty(userModel.Password))
			{
				return Task.Run<BaseResult>(() => ResultFactory.CreateUserResult(ResultCode.PasswordShouldNotBeEmpty));
			}
			var rum = new RequestUserModel
			{
				Action = "Login",
				Parameter = string.Empty,
				User = userModel
			};
			try
			{
				return _userDal.Post(rum);
			}
			catch (Exception e)
			{
				return Task.Run<BaseResult>(() => ResultFactory.CreateUserResult(e));
			}

		}

		public Task<BaseResult> Register(UserModel userModel)
		{
			if (string.IsNullOrWhiteSpace(userModel.UserName))
			{
				return Task.Run<BaseResult>(() => ResultFactory.CreateUserResult(ResultCode.UserNameShouldNotBeWhiteSpace));
			}
			if (string.IsNullOrEmpty(userModel.UserName))
			{
				return Task.Run<BaseResult>(() => ResultFactory.CreateUserResult(ResultCode.UserNameShouldNotBeEmpty));
			}
			if (string.IsNullOrEmpty(userModel.Password))
			{
				return Task.Run<BaseResult>(() => ResultFactory.CreateUserResult(ResultCode.PasswordShouldNotBeEmpty));
			}
			var rum = new RequestUserModel
			{
				Action = "Register",
				Parameter = string.Empty,
				User = userModel
			};
			try
			{
				return _userDal.Post(rum);
			}
			catch (Exception e)
			{
				return Task.Run<BaseResult>(() => ResultFactory.CreateUserResult(e));
			}
		}

		public Task<BaseResult> UpdatePassword(UserModel userModel)
		{
			if (string.IsNullOrWhiteSpace(userModel.UserName))
			{
				return Task.Run<BaseResult>(() => ResultFactory.CreateUserResult(ResultCode.UserNameShouldNotBeWhiteSpace));
			}
			if (string.IsNullOrEmpty(userModel.UserName))
			{
				return Task.Run<BaseResult>(() => ResultFactory.CreateUserResult(ResultCode.UserNameShouldNotBeEmpty));
			}
			if (string.IsNullOrEmpty(userModel.Password))
			{
				return Task.Run<BaseResult>(() => ResultFactory.CreateUserResult(ResultCode.PasswordShouldNotBeEmpty));
			}
			var rum = new RequestUserModel()
			{
				Action = "UpdatePassword",
				Parameter = string.Empty,
				User = userModel
			};
			try
			{
				return _userDal.Post(rum);
			}
			catch (Exception e)
			{
				return Task.Run<BaseResult>(() => ResultFactory.CreateUserResult(e));
			}
		}

	}
}
