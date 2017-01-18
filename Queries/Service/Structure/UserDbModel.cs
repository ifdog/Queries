using Common.Structure;
using LiteDB;

namespace Service.Structure
{
	public class UserDbModel:DbModel
	{
		[BsonIndex]
		[BsonField("User")]
		public UserModel User { get; set; }
	}
}
