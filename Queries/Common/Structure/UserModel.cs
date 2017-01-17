using System;
using Common.Attribute;
using Common.Structure.Base;

namespace Common.Structure
{
    public class UserModel : BaseUser
    {
        public override byte[] Id { get; set; }

        [SeenFromUi(@"用户名", 1)]
        public override string UserName { get; set; }

        [SeenFromUi(@"密码", 1)]
        public override string Password { get; set; }

        [SeenFromUi(@"实名", 1)]
        public override string RealName { get; set; }

        [SeenFromUi(@"权限", 1)]
        public override long Power { get; set; }

        [SeenFromUi(@"注册日期", 1)]
        public override DateTime RegDate { get; set; }

        [SeenFromUi(@"最近访问", 1)]
        public override DateTime LastAccess { get; set; }

        public string Tag { get; set; }
    }
}
