using System.Collections.Generic;
using Queries.Structure;
using Queries.ViewModel.Base;

namespace Queries.ViewModel
{
    public class QueriesViewModel:BaseViewModel
    {
        public QueriesViewModel()
        {
            this.Pages.Add(new Function
            {
                Name = "查询",
                ViewId = "Query"
            });
            this.Pages.Add(new Function
            {
                Name = "导入",
                ViewId = "ItemsImport"
            });
			this.Pages.Add(new Function
			{
				Name = "测试",
				ViewId = "ForTesting"
			});

			RunContext.Get<StatusManager>().SetTarget(this);
        }

	    private List<Function> _pages = new List<Function>();

        public List<Function> Pages
        {
            get { return _pages; }
            set
            {
                _pages = value; 
                OnPropertyChanged(nameof(Pages));
            }

        }

	    private string _status;

	    public string Status
	    {
		    get { return _status; }
		    set
		    {
			    _status = value;
			    OnPropertyChanged(nameof(Status));
		    }
	    }
    }
}
