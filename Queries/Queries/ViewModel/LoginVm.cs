using System.Collections.Generic;
using Queries.View;
using Queries.ViewModel.Base;

namespace Queries.ViewModel
{
	public class LoginVm:ViewModelBase<LoginWindow>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public List<string> Domains { get; set; }
		public RelayCommand OkCommand { get; set; }
		public RelayCommand CancelCommand { get; set; }
		public RelayCommand RegisterCommand { get; set; }
		public RelayCommand CCommand { get; set; }
		public RelayCommand CsCommand { get; set; }
		public RelayCommand SCommand { get; set; }
		public string StatusText { get; set; }


		public LoginVm(LoginWindow view) : base(view)
		{
			OkCommand = new RelayCommand(() =>
			{
				view.DialogResult = true;
				view.Close();
			});

			CancelCommand = new RelayCommand(() =>
			{
				view.DialogResult = false;
				view.Close();
			});
		}
	}
}
