using Common.Structure.Base;

namespace Common.Structure
{
    public class RequestUserModel:BaseRequest
    {
        public override string Action { get; set; }
        public override string Parameter { get; set; }
        public UserModel User { get; set; }
    }
}
