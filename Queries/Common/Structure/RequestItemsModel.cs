using System.Collections.Generic;
using Common.Structure.Base;

namespace Common.Structure
{
    public class RequestItemsModel:BaseRequest
    {
        public List<ItemModel> Items { get; set; }
    }
}
