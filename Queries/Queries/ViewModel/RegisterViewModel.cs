using System;
using Common.Factory;
using Common.Structure;
using Queries.ViewModel.Base;

namespace Queries.ViewModel
{
	public class RegisterViewModel : BaseViewModel
	{
		private string _userName;

		public string UserName
		{
			get { return _userName; }
			set
			{
				_userName = value;
				OnPropertyChanged(nameof(UserName));
			}
		}

		private string _realName;

		public string RealName
		{
			get { return _realName; }
			set
			{
				_realName = value;
				OnPropertyChanged(nameof(RealName));
			}
		}

		private string _password1;

		public string Password1
		{
			get { return _password1; }
			set
			{
				_password1 = value;
				OnPropertyChanged(nameof(Password1));
			}
		}

		private string _password2;

		public string Password2
		{
			get { return _password2; }
			set
			{
				_password2 = value;
				OnPropertyChanged(nameof(Password2));
			}
		}

		private string _status;

		public string Status
		{
			get { return _status; }
			set
			{
				_status = value;
				OnPropertyChanged(nameof(Status));
			}
		}

		public RelayCommand OkCommand = new RelayCommand(MethodToExecute,CanExecuteEvaluator);

		private static bool CanExecuteEvaluator()
		{
			if (string.IsNullOrWhiteSpace(UserName))
			{
				
			}
		}

		private static void MethodToExecute()
		{
			throw new NotImplementedException();
		}
	}
}
