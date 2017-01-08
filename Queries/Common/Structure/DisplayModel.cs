using System.Collections.Generic;
using Common.Structure.Base;

namespace Common.Structure
{
    public class DisplayModel:BaseResult
    {
        public override int ResultCode { get; set; }
        public override string Information { get; set; }
        public List<string[]> Items { get; set; }
    }
}
