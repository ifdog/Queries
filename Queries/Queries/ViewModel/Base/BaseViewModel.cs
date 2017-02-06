using System.ComponentModel;
using Queries.Structure;

namespace Queries.ViewModel.Base
{
	public class BaseViewModel:INotifyPropertyChanged
	{
		private readonly StatusManager _status = RunContext.Get<StatusManager>();

		public void SetStatus(string s)
		{
			_status.Status = s;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
