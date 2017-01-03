using System.Windows;
using Queries.ViewModel;



namespace Queries.View
{
	/// <summary>
	/// LoginWindow.xaml 的交互逻辑
	/// </summary>
	public partial class LoginWindow : Window
	{
		private LoginVm _loginVm;
		public LoginWindow()
		{
			InitializeComponent();
			_loginVm = new LoginVm(this,new RegisterWindow(), new Client.Client("Http://localhost:88"));
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
