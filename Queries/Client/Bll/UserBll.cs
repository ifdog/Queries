using System;
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

        public BaseResult Login(UserModel userModel)
        {
            if (string.IsNullOrWhiteSpace(userModel.UserName))
            {
                return ResultFactory.CreateUserResult(ResultCode.UserNameShouldNotBeWhiteSpace);
            }
            if (string.IsNullOrEmpty(userModel.UserName))
            {
                return ResultFactory.CreateUserResult(ResultCode.UserNameShouldNotBeEmpty);
            }
            if (string.IsNullOrEmpty(userModel.Password))
            {
                return ResultFactory.CreateUserResult(ResultCode.PasswordShouldNotBeEmpty);
            }
            var rum = new RequestUserModel()
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
                return ResultFactory.CreateUserResult(e);
            }
		    
		}

		public BaseResult Register(UserModel userModel)
		{
            if (string.IsNullOrWhiteSpace(userModel.UserName))
            {
                return ResultFactory.CreateUserResult(ResultCode.UserNameShouldNotBeWhiteSpace);
            }
            if (string.IsNullOrEmpty(userModel.UserName))
            {
                return ResultFactory.CreateUserResult(ResultCode.UserNameShouldNotBeEmpty);
            }
            if (string.IsNullOrEmpty(userModel.Password))
            {
                return ResultFactory.CreateUserResult(ResultCode.PasswordShouldNotBeEmpty);
            }
            var rum = new RequestUserModel()
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
                return ResultFactory.CreateUserResult(e);
            }
        }

		public BaseResult UpdatePassword(UserModel userModel)
		{
            if (string.IsNullOrWhiteSpace(userModel.UserName))
            {
                return ResultFactory.CreateUserResult(ResultCode.UserNameShouldNotBeWhiteSpace);
            }
            if (string.IsNullOrEmpty(userModel.UserName))
            {
                return ResultFactory.CreateUserResult(ResultCode.UserNameShouldNotBeEmpty);
            }
            if (string.IsNullOrEmpty(userModel.Password))
            {
                return ResultFactory.CreateUserResult(ResultCode.PasswordShouldNotBeEmpty);
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
                return ResultFactory.CreateUserResult(e);
            }
        }

	}
}
