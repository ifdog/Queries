using System.Collections.Generic;
using Common.Structure.Base;

namespace Common.Structure
{
    public class ResultItemsModel:BaseResult
    {
        public override int ResultCode { get; set; }
        public override string Information { get; set; }
        public List<ItemModel> Items { get; set; }
    }
}
