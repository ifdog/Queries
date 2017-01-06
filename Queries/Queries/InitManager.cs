using System;
using Common.Factory;
using Common.Structure;
using Queries.View;

namespace Queries
{
	public static class InitManager
	{
		public static void Init()
		{
			RunContext.Add(() => new ConfigurationManager());
			RunContext.Add(()=> RunContext.Get<ConfigurationManager>().Parse());
			var configuration = RunContext.Get<Configuration>();
			
			RunContext.Add(()=> new RegisterWindow());
			RunContext.Add(()=>new LoginWindow());
		}
	}
}
