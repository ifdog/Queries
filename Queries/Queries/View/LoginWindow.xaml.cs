using System;
using System.Windows;
using Queries.View.Base;
using Queries.ViewModel;



namespace Queries.View
{
	/// <summary>
	/// LoginWindow.xaml 的交互逻辑
	/// </summary>
	public partial class LoginWindow : Lwindow
	{
		public LoginWindow()
		{
			InitializeComponent();
		}

		internal override void IsPassOnHandler(object sender, EventArgs eventArgs)
		{
			this.DialogResult = true;
			this.Close();
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
