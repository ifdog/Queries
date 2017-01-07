using System.Windows;
using Common.Factory;
using Queries.View;
using RunContext = Queries.RunContext;

namespace Queries
{
	/// <summary>
	/// App.xaml 的交互逻辑
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			InitManager.Init();
			Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
			var dialogResult = RunContext.Get<LoginWindow>().ShowDialog();
			if (dialogResult.HasValue && dialogResult.Value)
			{
				base.OnStartup(e);
				Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
			}
			else
			{
				Shutdown();
			}
		}
	}
}
