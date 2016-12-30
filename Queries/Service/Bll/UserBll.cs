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
        private readonly IDal<UserModel> _userDal = new BaseDal<UserModel>();
        private readonly UserFactory _userFactory = new UserFactory();

        public ResultCode Register(string userName, string password, string realName)
        {
            if (_userDal.Select().Any(i => i.UserName.Equals(userName))) return ResultCode.UserNameAlreadyExist;
            return _userDal.Insert(_userFactory.CreateNew(userName, password, realName))
                ? ResultCode.Ok
                : ResultCode.DataBaseUndefinedException;
        }

        public ResultCode Login(string userName, string password)
        {
            if (!_userDal.Select().Any(i => i.UserName.Equals(userName))) return ResultCode.UserNotExist;
            var user = _userDal.Select().FirstOrDefault(i => i.UserName.Equals(userName));
            if (user == null || !user.Password.Equals(password)) return ResultCode.InvalidUserNameOrPassword;
            user.LastAccess = DateTime.Now;
            return _userDal.Update(user) ? ResultCode.Ok : ResultCode.DataBaseUndefinedException;
        }

        public ResultCode UpdatePassword(string userName, string password)
        {
            if (!_userDal.Select().Any(i => i.UserName.Equals(userName))) return ResultCode.UserNotExist;
            var user = _userDal.Select().FirstOrDefault(i => i.UserName.Equals(userName));
            if (user == null) return ResultCode.DataBaseUndefinedException;
            user.Password = password;
            return _userDal.Update(user) ? ResultCode.Ok : ResultCode.DataBaseUndefinedException;
        }
    }
}
