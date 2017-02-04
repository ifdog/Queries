using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Common.Attribute;
using Common.Static;
using Common.Structure;

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
			if (e.ViewportHeight > 0 && e.VerticalOffset > 0 &&
			    e.VerticalOffset - e.ExtentHeight + e.ViewportHeight > -1)
			{
				this.LoadProceed = !this.LoadProceed;
			}
		}

		private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
		{
			if (AttributeHelper.GetSearchPropertyDescriptions().Contains(e.Column.Header))
			{
				if (this.TextBox.Text.StartsWith("All@") && !this.TextBox.Text.EndsWith(":"))
				{

				}
				else
				{
					this.TextBox.Text = $"All@{e.Column.Header}:";
				}
				this.TextBox.Focus();
				this.TextBox.CaretIndex = this.TextBox.Text.Length;
			}
			e.Handled = true;

		}
	}
}
