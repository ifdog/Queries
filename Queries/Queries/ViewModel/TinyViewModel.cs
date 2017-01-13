using System.Collections.Generic;
using Queries.ViewModel.Base;

namespace Queries.ViewModel
{
	public class TinyViewModel:BaseViewModel
	{
		private Dictionary<string,string> _content = new Dictionary<string, string>();

		public Dictionary<string, string> Content
		{
			get { return _content; }
			set
			{
				_content = value;
				OnPropertyChanged(nameof(Content));
			}
		}

		private double _posX = 0;

		public double PosX
		{
			get { return _posX; }
			set
			{
				_posX = value;
				OnPropertyChanged(nameof(PosX));
			}
		}

		private double _posY = 0;

		public double PosY
		{
			get { return _posY; }
			set
			{
				_posY = value;
				OnPropertyChanged(nameof(PosY));
			}
		}

		private bool _show = false;

		public bool Show
		{
			get { return _show; }
			set
			{
				_show = value;
				OnPropertyChanged(nameof(Show));
			}
		}

	}
}
