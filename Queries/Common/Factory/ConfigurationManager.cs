using Common.Structure;

namespace Common.Factory
{
	public class ConfigurationManager
	{
		public Configuration Parse()
		{
			return new Configuration
			{
				RunMode = 3,
				ServerPath = "192.168.1.4",
				ServerPort = 88,
				RequestPath = "192.168.1.4",
				RequestPort = 88,
				UserName = "ifdog",
				Password = "123456"
			};
		}
	}
}
