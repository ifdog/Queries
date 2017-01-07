using System;
using System.Windows.Media.Imaging;

namespace Queries.ViewModel.Base
{
	public class Function
	{
		public string Name { get; set; }
		public BitmapImage Icon { get; set; }
		public string ViewId { get; set; }
		public Action Action { get; set; }
	    public override string ToString()
	    {
	        return ViewId;
	    }
	}
}
