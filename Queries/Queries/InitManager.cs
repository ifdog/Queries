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
			if ((configuration.RunMode >> 1 & 1) == 1)
			{
				RunContext.Add(() =>
				{
					var service = new Service.Service(configuration.ServerPath, configuration.ServerPort);
					service.StartHosting();
					return service;
				});
				RunContext.Get<Service.Service>();
			}
			if ((configuration.RunMode & 1) == 1)
			{
				RunContext.Add(() => new Client.Client(configuration.RequestPath, configuration.RequestPort));
				RunContext.Get<Client.Client>();
			}
			RunContext.Add(()=> new RegisterWindow());
			RunContext.Add(()=>new LoginWindow());
		}
	}
}
