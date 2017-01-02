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
        private readonly ResultFactory _resultFactory = new ResultFactory();

        public ResultUserModel Register(UserModel user)
        {
            if (_userDal.Select().Any(i => i.UserName.Equals(user.UserName)))
                return _resultFactory.CreateUserResult(ResultCode.UserNameAlreadyExist);
            return _userDal.Insert(user)
                ? _resultFactory.CreateUserResult(ResultCode.Ok)
                : _resultFactory.CreateUserResult(ResultCode.DataBaseUndefinedException);
        }

        public ResultUserModel Login(UserModel user)
        {
            if (!_userDal.Select().Any(i => i.UserName.Equals(user.UserName)))
                return _resultFactory.CreateUserResult(ResultCode.UserNotExist);
            var u = _userDal.Select().FirstOrDefault(i => i.UserName.Equals(user.UserName));
            if (u == null || u.Password.Equals(user.Password))
                return _resultFactory.CreateUserResult(ResultCode.InvalidUserNameOrPassword);
            user.LastAccess = DateTime.Now;
            return _userDal.Update(user)
                ? _resultFactory.CreateUserResult(ResultCode.Ok)
                : _resultFactory.CreateUserResult(ResultCode.DataBaseUndefinedException);
        }

        public ResultUserModel UpdatePassword(UserModel user)
        {
            if (!_userDal.Select().Any(i => i.UserName.Equals(user.UserName)))
                return _resultFactory.CreateUserResult(ResultCode.UserNotExist);
            var u = _userDal.Select().FirstOrDefault(i => i.UserName.Equals(user.UserName));
            if (u == null) return _resultFactory.CreateUserResult(ResultCode.DataBaseUndefinedException);
            u.Password = user.Password;
            return _userDal.Update(user)
                ? _resultFactory.CreateUserResult(ResultCode.Ok)
                : _resultFactory.CreateUserResult(ResultCode.DataBaseUndefinedException);
        }
    }
}
