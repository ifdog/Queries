using Common.Enums;
using Common.Factory;
using Common.Static;
using Common.Structure;
using Queries.View;
using Queries.ViewModel.Base;
using ServiceAfter = Queries.Helpers.ServiceAfter;

namespace Queries.ViewModel
{
	public class LoginViewModel : BaseViewModel
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string[] RunModes { get; set; } = {"Client Only", "Server Only", "Client/Server"};
		public RelayCommand OkCommand { get; set; }
		public RelayCommand RegisterCommand { get; set; }
		public bool StartEmbeded { get; private set; }

		private bool _isPassOn;

		public bool IsPassOn
		{
			get { return _isPassOn; }
			set
			{
				_isPassOn = value;
				OnPropertyChanged(nameof(IsPassOn));
			}
		}

		private string _statusText;

		public string StatusText
		{
			get { return _statusText; }
			set
			{
				_statusText = value;
				OnPropertyChanged(nameof(StatusText));
			}
		}

		private string[] _ips = Net.GetIpAddresses();

		public string[] Ips
		{
			get { return _ips; }
			set
			{
				_ips = value;
				OnPropertyChanged(nameof(Ips));
			}
		}

		private string _selectIp;

		public string SelectIp
		{
			get { return _selectIp; }
			set
			{
				_selectIp = value;
				OnPropertyChanged(nameof(SelectIp));
			}
		}

		private string _serverIp;

		public string ServerIp
		{
			get { return _serverIp; }
			set
			{
				_serverIp = value;
				OnPropertyChanged(nameof(ServerIp));
			}
		}

		private int _runMode;

		public int RunMode
		{
			get { return _runMode; }
			set
			{
				_runMode = value;
				OnPropertyChanged(nameof(RunMode));
			}
		}

		public LoginViewModel()
		{
			var config = RunContext.Get<Configuration>();
			this.RunMode = config.RunMode;
			this.StartEmbeded = config.ServiceStartMode;
			this.ServerIp = config.ServerPath;
			var reqPort = config.RequestPort;
			var svrPort = config.ServerPort;

			OkCommand = new RelayCommand(() =>
			{
				StartService(ServerIp, svrPort, ServerIp, reqPort);
				var t = RunContext.Get<Client.Client>().User.Login(new UserModel
					{
						UserName = this.UserName,
						Password = this.Password
					});
				t.ContinueWith(i =>
				{
					this.StatusText = i.Result.Information;
					if (i.Result.ResultCode == ResultCode.Ok.ToInt())
					{
						IsPassOn = true;
					}
				});
				//t.Start();
			});

			RegisterCommand = new RelayCommand(() =>
			{
				StartService(ServerIp, svrPort, ServerIp, reqPort);
				RunContext.GetNew<RegisterWindow>().Show();
			});
		}

		public void StartService(string serverPath, int serverPort, string requestPath, int requestPort)
		{
			switch (RunMode)
			{
				case 1:

					RunContext.TryAdd(() => new Client.Client(requestPath, requestPort));
					break;
				case 2:
					RunContext.TryAdd(() => new ServiceAfter(StartEmbeded, serverPath, serverPort));
					RunContext.Get<ServiceAfter>().Run();
					break;
				case 3:
					RunContext.TryAdd(() => new Client.Client(requestPath, requestPort));
					RunContext.TryAdd(() => new ServiceAfter(StartEmbeded, serverPath, serverPort));
					RunContext.Get<ServiceAfter>().Run();
					break;
			}
		}
	}
}
