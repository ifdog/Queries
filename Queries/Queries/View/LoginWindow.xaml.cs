using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
			_loginVm = new LoginVm(this);
		}
	}
}
