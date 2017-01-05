using System;
using System.Linq;
using Common.Enums;
using Common.Factory;
using Common.Structure;
using Service.Dal;


namespace Service.Bll
{
    public class UserBll
    {
	    readonly LiteDal<UserModel> _userDal = new LiteDal<UserModel>("UserName");

	    public ResultUserModel Register(UserModel user)
	    {
		    if (_userDal.Find(i => i.UserName.Equals(user.UserName)).Any())
			    return ResultFactory.CreateUserResult(ResultCode.UserNameAlreadyExist);
		    _userDal.Insert(user);
		    return ResultFactory.CreateUserResult(ResultCode.Ok);
	    }

	    public ResultUserModel Login(UserModel user)
	    {
		    if (!_userDal.Find(i => i.UserName.Equals(user.UserName)).Any())
			    return ResultFactory.CreateUserResult(ResultCode.UserNotExist);
		    var u = _userDal.Find(i => i.UserName.Equals(user.UserName)).FirstOrDefault();
		    if (u == null || !u.Password.Equals(user.Password))
			    return ResultFactory.CreateUserResult(ResultCode.InvalidUserNameOrPassword);
		    u.LastAccess = DateTime.Now;
		    _userDal.Update(u);
		    return ResultFactory.CreateUserResult(ResultCode.Ok);
	    }

	    public ResultUserModel UpdatePassword(UserModel user)
	    {
		    if (!_userDal.Find(i => i.UserName.Equals(user.UserName)).Any())
			    return ResultFactory.CreateUserResult(ResultCode.UserNotExist);
		    var u = _userDal.Find(i => i.UserName.Equals(user.UserName)).FirstOrDefault();
		    if (u == null) return ResultFactory.CreateUserResult(ResultCode.DataBaseUndefinedException);
		    u.Password = user.Password;
		    _userDal.Update(u);
		    return ResultFactory.CreateUserResult(ResultCode.Ok);
	    }
    }
}
