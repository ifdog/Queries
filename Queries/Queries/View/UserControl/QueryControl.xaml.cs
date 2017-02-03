using System.Windows;
using System.Windows.Controls;

namespace Queries.View.UserControl
{
	/// <summary>
	/// QueryControl.xaml 的交互逻辑
	/// </summary>
	public partial class QueryControl :Base.BaseQueryControl
	{
		public QueryControl()
		{
			InitializeComponent();
		}

		private void DataGrid_OnScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			if (!LoadProceed && e.ViewportHeight > 0 && e.VerticalOffset > 0 &&
			    e.VerticalOffset - e.ExtentHeight + e.ViewportHeight > -1)
			{
				this.LoadProceed = true;
			}
		}
	}
}
