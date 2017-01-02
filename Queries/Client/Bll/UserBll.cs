using Client.Dal;
using Common.Structure;
using Common.Structure.Base;
using RestSharp;

namespace Client.Bll
{
	public class UserBll
	{
		private readonly UserDal _userDal;
		public UserBll(RestClient restClient)
		{
			this._userDal = new UserDal(restClient);
		}

		public BaseResult Login(UserModel userModel)
		{
            var rum = new RequestUserModel()
            {
                Action = "Login",
                Parameter = string.Empty,
                User = userModel
            };
		    return _userDal.Post(rum);
		}

		public BaseResult Register(UserModel userModel)
		{
            var rum = new RequestUserModel()
            {
                Action = "Register",
                Parameter = string.Empty,
                User = userModel
            };
            return _userDal.Post(rum);
        }

		public BaseResult UpdatePassword(UserModel userModel)
		{
            var rum = new RequestUserModel()
            {
                Action = "UpdatePassword",
                Parameter = string.Empty,
                User = userModel
            };
            return _userDal.Post(rum);
        }

	}
}
