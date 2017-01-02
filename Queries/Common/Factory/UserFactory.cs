using System;
using Common.Structure;

namespace Common.Factory
{
    public class UserFactory
    {
        public UserModel CreateNew(string userName, string password, string realName,long power = 2)
        {
            return new UserModel
            {
                UserName = userName,
                Password = password,
                RealName = realName,
                Power = power,
                RegDate = DateTime.Now,
                LastAccess = DateTime.MinValue,
                Tag = string.Empty
            };
        }
    }
}
