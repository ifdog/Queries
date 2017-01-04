using Common.Factory;
using Queries.View;

namespace Queries
{
	public static class InitManager
	{
		public static void Init()
		{
			var configurationManager = new ConfigurationManager();
			RunContext.Add(configurationManager);
			var configuration = configurationManager.Parse();
			RunContext.Add(configuration);
			if ((configuration.RunMode >> 1 & 1) == 1)
			{
				var service = new Service.Service(configuration.ServerPath, configuration.ServerPort);
				RunContext.Add(service);
			}
			if ((configuration.RunMode & 1) == 1)
			{
				var client = new Client.Client(configuration.RequestPath, configuration.RequestPort);
				RunContext.Add(client);
			}

			var registerView = new RegisterWindow();
			RunContext.Add(registerView);
			var loginView = new LoginWindow();
			RunContext.Add(loginView);
			var updatePasswordView = new UpdatePasswordWindow();
			RunContext.Add(updatePasswordView);
			var queryView = new QueryWindow();
			RunContext.Add(queryView);
			var addItemView = new AddItemWindow();
			RunContext.Add(addItemView);
			var importItemsView = new ImportItemsWindow();
			RunContext.Add(importItemsView);
			var updateItemView = new UpdateItemWindow();
			RunContext.Add(updateItemView);
		}
	}
}
