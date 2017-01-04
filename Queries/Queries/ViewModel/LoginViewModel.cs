using System.Collections.Generic;
using Common.Factory;
using Queries.ViewModel.Base;

namespace Queries.ViewModel
{
	public class LoginViewModel : BaseViewModel
	{
		private string _userName;
		private string _password;
		private string _selectedDomain;
		private List<string> _domains;
		private bool _isClientMode;
		private bool _isCsMode;
		private bool _isServerMode;
		private bool _isPassOn;
		private string _statusText;
		private readonly Client.Client _client;
		private readonly RegisterWindow _registerWindow;
		public string UserName { get; set; }
		public string Password { get; set; }
		public RelayCommand OkCommand { get; set; }
		public RelayCommand RegisterCommand { get; set; }

		public bool IsPassOn
		{
			get { return _isPassOn; }
			set
			{
				_isPassOn = value;
				OnPropertyChanged("IsPassOn");
			}
		}

		public bool IsClientMode
		{
			get { return _isClientMode; }
			set
			{
				_isClientMode = value;
				OnPropertyChanged("IsClientMode");
			}
		}
		public bool IsCsMode
		{
			get { return _isCsMode; }
			set
			{
				_isCsMode = value;
				OnPropertyChanged("IsCsMode");
			}
		}

		public bool IsServerMode
		{
			get { return _isServerMode; }
			set
			{
				_isServerMode = value;
				OnPropertyChanged("IsServerMode");
			}
		}
		public List<string> Domain
		{
			get { return _domains; }
			set
			{
				_domains = value;
				OnPropertyChanged("Domain");
			}
		}
		public string StatusText
		{
			get { return _statusText; }
			set
			{
				_statusText = value;
				OnPropertyChanged("StatusText");
			}
		}
		public LoginViewModel()
		{
			_client = RunContext.Get<Client.Client>();
			_registerWindow = RunContext.Get<RegisterWindow>();
			this.IsClientMode = true;
			OkCommand = new RelayCommand(() =>
			{
				//var result = _client.User.Login(new UserModel
				//{
				//	UserName = this.UserName,
				//	Password = this.Password
				//});
				//this.StatusText = result.Information;
				//if (result.ResultCode!=ResultCode.Ok.ToInt())
				//{

				//	return;
				IsPassOn = true;
			});
			RegisterCommand = new RelayCommand(() =>
			{
				_registerWindow.Show();
			});


			
		}


	}
}
