using Common.Factory;
using Queries.Structure;
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
			RunContext.Add(() => new Tiny());
			RunContext.Add(() => new StatusManager());
		}
	}
}
