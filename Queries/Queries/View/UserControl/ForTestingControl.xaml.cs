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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Queries.View.UserControl
{
	/// <summary>
	/// ForTesting.xaml 的交互逻辑
	/// </summary>
	public partial class ForTestingControl
	{
		public ForTestingControl()
		{
			InitializeComponent();
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			RunContext.Get<Tiny>().Show();
		}
	}
}
