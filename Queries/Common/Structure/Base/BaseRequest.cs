using System.Collections.Generic;

namespace Common.Structure.Base
{
    public abstract class BaseRequest
    {
        public abstract string Action { get; set; }
        public abstract string Parameter { get; set; }
    }
}
