using Common.Factory;
using Queries.View;

namespace Queries
{
	public static class InitManager
	{
		public static void Init()
		{
			RunContext.Add(() => new ConfigurationManager());
			RunContext.Add(() => RunContext.Get<ConfigurationManager>().Parse());
			RunContext.Add(() => new RegisterWindow());
			RunContext.Add(() => new LoginWindow());
		}
	}
}
