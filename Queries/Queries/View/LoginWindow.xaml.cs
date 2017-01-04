using System.Windows;
using Queries.ViewModel;



namespace Queries.View
{
	/// <summary>
	/// LoginWindow.xaml 的交互逻辑
	/// </summary>
	public partial class LoginWindow : Window
	{
		private LoginViewModel _loginVm;
		public LoginWindow()
		{
			InitializeComponent();
			_loginVm = new LoginViewModel();
		}

		private void Expander_Expanded(object sender, RoutedEventArgs e)
		{
			this.Height = 400;
		}

		private void Expander_Collapsed(object sender, RoutedEventArgs e)
		{
			this.Height = 260;
		}
	}
}
