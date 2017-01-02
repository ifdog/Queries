using System.Collections.Generic;
using Common.Structure.Base;

namespace Common.Structure
{
    public class RequestItemsModel:BaseRequest
    {
        public override string Action { get; set; }
        public override string Parameter { get; set; }
        public List<ItemModel> Items { get; set; }
    }
}
