using System;

namespace Common.Structure.Base
{
    public abstract class BaseUser:BaseObject
    {
        public abstract string UserName { get; set; }
        public abstract string Password { get; set; }
        public abstract string RealName { get; set; }
        public abstract long Power { get; set; }
        public abstract DateTime RegDate { get; set; }
        public abstract DateTime LastAccess { get; set; }
        
    }
}
