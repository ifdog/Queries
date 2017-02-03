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
	    private readonly Dictionary<string, string> _modelDict;

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
		    this._modelDict = this._modelProperties
			    .Select(i => new
			    {
				    i.GetCustomAttribute<SeenFromUiAttribute>().Description, i.Name
				})
			    .ToDictionary(i => i.Description, i => i.Name);
		    this._titles = this._modelDict.Keys.ToList();
		    _titles.ForEach(i => _data.Columns.Add(i, typeof(string)));
	    }

	    private void QueryViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
	    {
		    if (e.PropertyName.Equals(nameof(Query)))
		    {
				if (string.IsNullOrWhiteSpace(Query)) return;
				var prefix = Query.Contains(':') ? "All" : "Any";
			    this.Page = 0;
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
		    if (e.PropertyName.Equals(nameof(LoadProceed)))
		    {
			    Page++;
			    var cont = _client.Item.Query(_query, Page, PageLength);
			    if (cont?.Items == null || cont.Items.Count <= 0) return;
			    cont.Items.ForEach(i =>
			    {
				    var r = _data.NewRow();
				    for (var j = 0; j < _titles.Count; j++)
				    {
					    r[_titles[j]] = _modelProperties[j].GetValue(i)?.ToString();
				    }
				    _data.Rows.Add(r);
			    });
		    }
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

	    private bool _loadProceed = false;

	    public bool LoadProceed
	    {
		    get { return _loadProceed; }
		    set
		    {
			    _loadProceed = value;
			    OnPropertyChanged(nameof(LoadProceed));
		    }
	    }
    }
}
