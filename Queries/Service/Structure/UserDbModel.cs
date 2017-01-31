using Common.Attribute;
using Common.Structure;
using LiteDB;
using Service.Structure.Base;

namespace Service.Structure
{
	public class UserDbModel:DbModel
	{
		[BsonField("User")]
		[TypeIndexed]
		public UserModel User { get; set; }
	}
}
