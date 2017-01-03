using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Common.Enums;
using Common.Static;
using Common.Structure;
using Queries.View;
using Queries.ViewModel.Base;

namespace Queries.ViewModel
{
	public class LoginVm : ViewModelBase<LoginWindow>
	{
		private string _userName;
		private string _password;
		private string _selectedDomain;
		private List<string> _domains;
		private bool _isClientMode;
		private bool _isCsMode;
		private bool _isServerMode;
		private string _statusText;
		private Client.Client _client;
		public string UserName { get; set; }
		public string Password { get; set; }
		public RelayCommand OkCommand { get; set; }
		public RelayCommand CancelCommand { get; set; }
		public RelayCommand RegisterCommand { get; set; }

		public bool IsClientMode
		{
			get { return _isClientMode; }
			set
			{
				_isClientMode = value;
				RaisePropertyChanged("IsClientMode");
			}
		}
		public bool IsCsMode
		{
			get { return _isCsMode; }
			set
			{
				_isCsMode = value;
				RaisePropertyChanged("IsCsMode");
			}
		}

		public bool IsServerMode
		{
			get { return _isServerMode; }
			set
			{
				_isServerMode = value;
				RaisePropertyChanged("IsServerMode");
			}
		}
		public List<string> Domain
		{
			get { return _domains; }
			set
			{
				_domains = value;
				RaisePropertyChanged("Domain");
			}
		}
		public string StatusText
		{
			get { return _statusText; }
			set
			{
				_statusText = value;
				RaisePropertyChanged("StatusText");
			}
		}
		public LoginVm(LoginWindow loginWindow, Window registerWindow, Client.Client client) : base(loginWindow)
		{
			_client = client;
			this.IsClientMode = true;
			OkCommand = new RelayCommand(() =>
			{
				var result = _client.User.Login(new UserModel
				{
					UserName = this.UserName,
					Password = this.Password
				});
				this.StatusText = result.Information;
				if (result.ResultCode!=ResultCode.Ok.ToInt())
				{
					
					return;
				}
				loginWindow.DialogResult = true;
				loginWindow.Close();
			});
			CancelCommand = new RelayCommand(() =>
			{
				loginWindow.DialogResult = false;
				loginWindow.Close();
			});
			RegisterCommand = new RelayCommand(registerWindow.Show);


			
		}
	}
}
