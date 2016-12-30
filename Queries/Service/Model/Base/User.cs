using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model.Base
{
    public class User:Model
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RealName { get; set; }
        public long Power { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime LastAccess { get; set; }
    }
}
