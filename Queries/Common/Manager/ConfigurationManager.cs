using Common.Structure;

namespace Common.Manager
{
	public class ConfigurationManager
	{
		public Configuration Parse()
		{
			return new Configuration
			{
				RunMode = 3,
				ServerPath = "localhost",
				ServerPort = 88,
				RequestPath = "localhost",
				RequestPort = 88,
				UserName = "ifdog",
				Password = "123456"
			};
		}
	}
}
