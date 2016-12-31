using System;

namespace Common.Structure.Base
{
    public class BaseUser:BaseObject
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RealName { get; set; }
        public long Power { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime LastAccess { get; set; }
    }
}
