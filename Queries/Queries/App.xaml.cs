using System.Windows;
using Queries.View;

namespace Queries
{
	/// <summary>
	/// App.xaml 的交互逻辑
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
			var dialogResult = new LoginWindow().ShowDialog();
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
