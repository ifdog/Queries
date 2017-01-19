using Common.Structure;
using LiteDB;
using Service.Structure.Base;

namespace Service.Structure
{
	public class UserDbModel:DbModel
	{
		[BsonIndex]
		[BsonField("User")]
		public UserModel User { get; set; }
	}
}
