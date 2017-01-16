using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Queries.Structure;
using Queries.ViewModel.Base;

namespace Queries.ViewModel
{
	public class TinyViewModel:BaseViewModel
	{
		public TinyViewModel()
		{
			var x = new ObservableCollection<TinyShowItem>
			{
				new TinyShowItem
				{
					Name = "aaaa",
					Detail = "bbbb"
				},
				new TinyShowItem
				{
					Name = "axxx",
					Detail = "bxxb"
				},
				new TinyShowItem
				{
					Name = "asss",
					Detail = "bbsb"
				}
			};
			Content = x;
		}

		private ObservableCollection<TinyShowItem> _content = new ObservableCollection<TinyShowItem>();
		public ObservableCollection<TinyShowItem> Content
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
