using System;
using System.Linq;
using Common.Enums;
using Common.Factory;
using Common.Structure;
using Common.Structure.Base;
using Service.Dal;
using Service.Dal.Base;

namespace Service.Bll
{
    public class UserBll
    {
        private readonly IDal<UserModel> _userDal = new BaseDal<UserModel>();
        private readonly ResultFactory ResultFactory = new ResultFactory();

        public ResultUserModel Register(UserModel user)
        {
            if (_userDal.Select().Any(i => i.UserName.Equals(user.UserName)))
                return ResultFactory.CreateUserResult(ResultCode.UserNameAlreadyExist);
            return _userDal.Insert(user)
                ? ResultFactory.CreateUserResult(ResultCode.Ok)
                : ResultFactory.CreateUserResult(ResultCode.DataBaseUndefinedException);
        }

        public ResultUserModel Login(UserModel user)
        {
            if (!_userDal.Select().Any(i => i.UserName.Equals(user.UserName)))
                return ResultFactory.CreateUserResult(ResultCode.UserNotExist);
            var u = _userDal.Select().FirstOrDefault(i => i.UserName.Equals(user.UserName));
            if (u == null || u.Password.Equals(user.Password))
                return ResultFactory.CreateUserResult(ResultCode.InvalidUserNameOrPassword);
            user.LastAccess = DateTime.Now;
            return _userDal.Update(user)
                ? ResultFactory.CreateUserResult(ResultCode.Ok)
                : ResultFactory.CreateUserResult(ResultCode.DataBaseUndefinedException);
        }

        public ResultUserModel UpdatePassword(UserModel user)
        {
            if (!_userDal.Select().Any(i => i.UserName.Equals(user.UserName)))
                return ResultFactory.CreateUserResult(ResultCode.UserNotExist);
            var u = _userDal.Select().FirstOrDefault(i => i.UserName.Equals(user.UserName));
            if (u == null) return ResultFactory.CreateUserResult(ResultCode.DataBaseUndefinedException);
            u.Password = user.Password;
            return _userDal.Update(user)
                ? ResultFactory.CreateUserResult(ResultCode.Ok)
                : ResultFactory.CreateUserResult(ResultCode.DataBaseUndefinedException);
        }
    }
}
