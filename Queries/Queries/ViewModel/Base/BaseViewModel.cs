using System.ComponentModel;
using Queries.Structure;

namespace Queries.ViewModel.Base
{
	public class BaseViewModel:INotifyPropertyChanged
	{
		private readonly StatusManager _status = RunContext.Get<StatusManager>();

		internal void SetStatus(string s)
		{
			_status.Status = s;
		}

		internal void SetPercents(double d)
		{
			_status.Percents = d;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
