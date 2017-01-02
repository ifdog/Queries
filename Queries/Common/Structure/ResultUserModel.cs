using Common.Structure.Base;

namespace Common.Structure
{
    public class ResultUserModel:BaseResult
    {
        public override int ResultCode { get; set; }
        public override string Information { get; set; }
        public UserModel User { get; set; }
    }
}
