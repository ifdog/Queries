using Common.Structure.Base;

namespace Common.Structure
{
    public class RequestItemModel:BaseRequest
    {
        public override string Action { get; set; }
        public override string Parameter { get; set; }
        public ItemModel Item { get; set; }
    }
}
