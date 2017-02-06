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

		public void SetTarget(QueriesViewModel vm)
		{
			_target = vm;
		}
	}
}
