using System;
using Common.Attribute;
using Common.Structure.Base;

namespace Common.Structure
{
    public class UserModel : BaseUser
    {
        public override long Id { get; set; }

        [Seen(@"用户名", 1)]
        public override string UserName { get; set; }

        [Seen(@"密码", 1)]
        public override string Password { get; set; }

        [Seen(@"实名", 1)]
        public override string RealName { get; set; }

        [Seen(@"权限", 1)]
        public override long Power { get; set; }

        [Seen(@"注册日期", 1)]
        public override DateTime RegDate { get; set; }

        [Seen(@"最近访问", 1)]
        public override DateTime LastAccess { get; set; }

        public string Tag { get; set; }
    }
}
