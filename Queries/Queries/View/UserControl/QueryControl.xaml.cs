using Common.Static;

namespace Queries.View.UserControl
{
	/// <summary>
	/// QueryControl.xaml 的交互逻辑
	/// </summary>
	public partial class QueryControl : System.Windows.Controls.UserControl
	{
		public QueryControl()
		{
			InitializeComponent();
		}

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.TextBox.Text = Excel.GetInstance();
        }
	}
}
