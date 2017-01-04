using System;
using System.ComponentModel;
using System.Windows;

namespace Queries.View.Base
{
	public class Lwindow : Window
	{
		public Lwindow()
		{
			var descriptor = DependencyPropertyDescriptor.FromProperty(Lwindow.IsPassOnProperty, typeof(Lwindow));
			descriptor.AddValueChanged(this, IsPassOnHandler);
		}

		internal virtual void IsPassOnHandler(object sender, EventArgs eventArgs)
		{

		}

		public bool IsPassOn
		{
			get { return (bool) GetValue(IsPassOnProperty); }
			set { SetValue(IsPassOnProperty, value); }
		}

		public static readonly DependencyProperty IsPassOnProperty =
			DependencyProperty.Register(nameof(IsPassOn), typeof(bool), typeof(Lwindow));
	}
}
