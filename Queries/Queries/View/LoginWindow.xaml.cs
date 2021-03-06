﻿using System;
using System.Windows;
using System.Windows.Controls;
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

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var password = (sender as PasswordBox);
            if (password != null)
            {
                PwSend.Text = password.Password;
            }
        }
    }
}
