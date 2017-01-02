using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Structure.Base
{
    public abstract class BaseStatus:BaseObject
    {
        public abstract DateTime LogTime { get; set; }
        public abstract long ItemCount { get; set; }
        public abstract long UserCount { get; set; }
        public abstract long QueryCount { get; set; }

    }
}
