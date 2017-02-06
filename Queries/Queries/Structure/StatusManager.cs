using Queries.ViewModel;

namespace Queries.Structure
{
	public class StatusManager
	{
		private QueriesViewModel _target;

		public string Status
		{
			get { return _target.Status; }
			set { _target.Status = value; }
		}

		public double Percents
		{
			get { return _target.Percents; }
			set { _target.Percents = value; }
		}

		public void SetTarget(QueriesViewModel vm)
		{
			_target = vm;
		}
	}
}
