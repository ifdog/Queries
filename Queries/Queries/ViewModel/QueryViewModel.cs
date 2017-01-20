using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Common.Attribute;
using Common.Structure;
using Queries.ViewModel.Base;

namespace Queries.ViewModel
{
    public class QueryViewModel : BaseViewModel
    {
        private readonly Client.Client _client;

	    private readonly List<PropertyInfo> _modelProperties;
	    private readonly List<string> _titles;

		public RelayCommand Previous { get; private set; }
		public RelayCommand Next { get; private set; }

	    public QueryViewModel()
	    {
		    _client = RunContext.Get<Client.Client>();
		    this.PropertyChanged += QueryViewModel_PropertyChanged;
		    this._modelProperties = typeof(ItemModel)
			    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
			    .Where(i => i.GetCustomAttribute<SeenFromUiAttribute>() != null)
			    .OrderBy(i => i.GetCustomAttribute<SeenFromUiAttribute>().Squence)
			    .ToList();
		    this._titles = this._modelProperties
			    .Select(i => i.GetCustomAttribute<SeenFromUiAttribute>().Description)
			    .ToList();
		    _titles.ForEach(i => _data.Columns.Add(i, typeof(string)));
		    this.Previous = new RelayCommand(() =>
		    {
			    if (Page > 0) Page--;
		    });
		    this.Next = new RelayCommand(() => Page++);
	    }

	    private void QueryViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
	    {
		    if (!e.PropertyName.Equals(nameof(Query)) && !e.PropertyName.Equals(nameof(Page))) return;
		    if (string.IsNullOrWhiteSpace(Query)) return;
		    var prefix = Query.Contains(':') ? "All" : "Any";
			
		    var x = _client.Item.Query(_query, Page, PageLength);
		    _data.Clear();
		    if (x?.Items == null || x.Items.Count <= 0) return;
		    x.Items.ForEach(i =>
		    {
			    var r = _data.NewRow();
			    for (var j = 0; j < _titles.Count; j++)
			    {
				    r[_titles[j]] = _modelProperties[j].GetValue(i)?.ToString();
			    }
			    _data.Rows.Add(r);
		    });
	    }

	    private string _query;

		public string Query
        {
            get { return _query; }
            set
            {
                _query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

	    private int _page = 0;

	    public int Page
	    {
		    get { return _page; }
		    set
		    {
			    _page = value;
			    OnPropertyChanged(nameof(Page));
		    }
	    }

	    private int _pageLength = 50;

	    public int PageLength
	    {
		    get { return _pageLength; }
		    set
		    {
			    _pageLength = value;
			    OnPropertyChanged(nameof(PageLength));
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

		private  DataTable _data = new DataTable();

	    public DataTable Data
	    {
		    get { return _data; }
		    set
		    {
			    _data = value; 
			    OnPropertyChanged(nameof(Data));
		    }
	    }		
    }
}
