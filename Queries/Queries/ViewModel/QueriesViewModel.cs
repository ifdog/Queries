using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.Pages.Add(new Function
            {
                Name = "404",
                ViewId = "NotFound"
            });
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
    }
}
