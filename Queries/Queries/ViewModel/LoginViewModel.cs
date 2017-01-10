using Common.Enums;
using Common.Factory;
using Common.Static;
using Common.Structure;
using Queries.ViewModel.Base;

namespace Queries.ViewModel
{
	public class LoginViewModel : BaseViewModel
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	    public string[] RunModes { get; set; } = {"Client Only", "Server Only", "Client/Server"};
        public RelayCommand OkCommand { get; set; }
		public RelayCommand RegisterCommand { get; set; }

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
		    this.ServerIp = config.ServerPath;
		    var reqPort = config.RequestPort;
		    var svrPort = config.ServerPort;

			OkCommand = new RelayCommand(() =>
			{
                StartService(ServerIp,svrPort,ServerIp,reqPort);
				var result = RunContext.Get<Client.Client>()
                .User.Login(new UserModel
				{
					UserName = this.UserName,
					Password = this.Password
				});
				this.StatusText = result.Information;
				if (result.ResultCode == ResultCode.Ok.ToInt())
				{
					IsPassOn = true;
				}
			});

			RegisterCommand = new RelayCommand(() =>
			{
                StartService(ServerIp, svrPort, ServerIp, reqPort);
                RunContext.GetNew<RegisterWindow>().Show();
			});
		}

	    public void StartService(string serverPath, int serverPort,string requestPath,int requestPort)
	    {
	        switch (RunMode)
	        {
                case 1:
                    
                    RunContext.TryAdd(() => new Client.Client(requestPath, requestPort));
                    break;
                case 2:
	                //RunContext.TryAdd(() => new Service.Service(serverPath, serverPorct));
                    RunContext.TryAdd(()=>new ServiceAfter(serverPath,serverPort));
                    RunContext.Get<ServiceAfter>().Run();
	                //RunContext.Get<Service.Service>().StartHosting();
                    break;
                case 3:
                    RunContext.TryAdd(() => new Client.Client(requestPath, requestPort));
                    //RunContext.TryAdd(() => new Service.Service(serverPath, serverPort));
                    //RunContext.Get<Service.Service>().StartHosting();
                    RunContext.TryAdd(() => new ServiceAfter(serverPath, serverPort));
                    RunContext.Get<ServiceAfter>().Run();
                    break;
	        }
	    }
	}
}
