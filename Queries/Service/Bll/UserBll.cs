using System;
using System.Linq;
using Common.Enums;
using Common.Factory;
using Common.Structure;
using Service.Dal;
using Service.Dal.Base;
using Service.Structure;


namespace Service.Bll
{
    public class UserBll
    {
		// private readonly LiteDal<UserDbModel> _userDal = new LiteDal<UserDbModel>("User");
		private readonly IDal<UserDbModel> _userDal = new PgDal<UserDbModel>();

		public ResultUserModel Register(UserModel user)
	    {
			if (_userDal.Find($"Exa@UserName:{user.UserName}").Any())
				return ResultFactory.CreateUserResult(ResultCode.UserNameAlreadyExist);
			_userDal.Insert(new UserDbModel
		    {
			    User = new UserModel
			    {
				    UserName = user.UserName,
					Password = user.Password,
					RealName = user.RealName,
					Power = 2,
					RegDate = DateTime.Now
			    }
		    });
		    return ResultFactory.CreateUserResult(ResultCode.Ok);
	    }

	    public ResultUserModel Login(UserModel user)
	    {
		    if (!_userDal.Find($"Exa@UserName:{user.UserName}").Any())
		    {
			    return ResultFactory.CreateUserResult(ResultCode.UserNotExist);
			}
			var u = _userDal.Find($"Exa@UserName:{user.UserName}").FirstOrDefault();
		    if (u == null || !u.User.Password.Equals(user.Password))
			    return ResultFactory.CreateUserResult(ResultCode.InvalidUserNameOrPassword);
		    u.User.LastAccess = DateTime.Now;
		    _userDal.Update(u);
		    return ResultFactory.CreateUserResult(ResultCode.Ok);
	    }

	    public ResultUserModel UpdatePassword(UserModel user)
	    {
		    if (!_userDal.Find($"Exa@UserName:{user.UserName}").Any())
			    return ResultFactory.CreateUserResult(ResultCode.UserNotExist);
		    var u = _userDal.Find($"Exa@UserName:{user.UserName}").FirstOrDefault();
		    if (u == null) return ResultFactory.CreateUserResult(ResultCode.DataBaseUndefinedException);
		    u.User.Password = user.Password;
		    _userDal.Update(u);
		    return ResultFactory.CreateUserResult(ResultCode.Ok);
	    }
    }
}
