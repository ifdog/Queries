using System.Windows.Input;
using Common.Enums;
using Common.Factory;
using Queries.ViewModel.Base;
using Common.Static;

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

        public RelayCommand OkCommand { get; set; }

	    public RegisterViewModel()
	    {
	        OkCommand = new RelayCommand(OkAction,OkCanExecute);
	    }

	    private bool OkCanExecute()
	    {
	        if (string.IsNullOrWhiteSpace(this.UserName))
	        {
	            this.Status = @"用户名不能为空";
	            return false;
	        }
	        if (string.IsNullOrWhiteSpace(this.RealName))
	        {
	            this.Status = @"真实姓名不能为空";
	            return false;
	        }
	        if (string.IsNullOrWhiteSpace(this.Password1))
	        {
	            this.Status = @"密码不能为空";
	            return false;
	        }
	        if (!_password1.Equals(Password2))
	        {
	            this.Status = @"密码不匹配";
	            return false;
	        }
	        return true;
	    }

	    private void OkAction()
	    {
	        var r = RunContext.Get<Client.Client>().User.Register(
	            UserFactory.CreateNew(
	                this.UserName, this.Password1, this.RealName));
		    this.Status = r.ToResultCode().GetDescription();
	    }
	}
}
