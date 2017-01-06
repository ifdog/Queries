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
	    private ObservableCollection<TreeNode> _treeNodes;

	    public ObservableCollection<TreeNode> TreeNodes
	    {
			get { return _treeNodes; }
		    set
		    {
			    _treeNodes = value;
			    OnPropertyChanged(nameof(TreeNodes));
		    }
		    
	    }
    }
}
