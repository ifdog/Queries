using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Service.Common.Factory;
using Service.Dal;
using Service.Dal.Base;
using Service.Model;
using Service.Model.Base;

namespace Service.Bll
{
    public class UserBll
    {
        private readonly IDal<User> _userDal = new BaseDal<User>();

        public ResultCode Register(User user)
        {
            if (_userDal.Select().Any(i => i.UserName.Equals(user.UserName))) return ResultCode.UserNameAlreadyExist;
            return _userDal.Insert(user)
                ? ResultCode.Ok
                : ResultCode.DataBaseUndefinedException;
        }

        public ResultCode Login(User user)
        {
            if (!_userDal.Select().Any(i => i.UserName.Equals(user.UserName))) return ResultCode.UserNotExist;
            var u = _userDal.Select().FirstOrDefault(i => i.UserName.Equals(user.UserName));
            if (u == null || u.Password.Equals(user.Password)) return ResultCode.InvalidUserNameOrPassword;
            user.LastAccess = DateTime.Now;
            return _userDal.Update(user) ? ResultCode.Ok : ResultCode.DataBaseUndefinedException;
        }

        public ResultCode UpdatePassword(User user)
        {
            if (!_userDal.Select().Any(i => i.UserName.Equals(user.UserName))) return ResultCode.UserNotExist;
            var u = _userDal.Select().FirstOrDefault(i => i.UserName.Equals(user.UserName));
            if (u == null) return ResultCode.DataBaseUndefinedException;
            u.Password = user.Password;
            return _userDal.Update(user) ? ResultCode.Ok : ResultCode.DataBaseUndefinedException;
        }
    }
}
