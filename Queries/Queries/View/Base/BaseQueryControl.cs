using System.Windows;

namespace Queries.View.Base
{
	public class BaseQueryControl : System.Windows.Controls.UserControl
	{
		public bool LoadProceed
		{
			get { return (bool) GetValue(LoadProceedProperty); }
			set { SetValue(LoadProceedProperty, value); }
		}

		public static readonly DependencyProperty LoadProceedProperty =
			DependencyProperty.Register(nameof(LoadProceed), typeof(bool), typeof(BaseQueryControl));
	}
}
