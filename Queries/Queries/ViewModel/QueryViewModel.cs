using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Common.Factory;
using Common.Static;
using Queries.Structure;
using Queries.ViewModel.Base;

namespace Queries.ViewModel
{
    public class QueryViewModel : BaseViewModel
    {
        private readonly Client.Client _client;
	    private readonly List<PropertyInfo> _modelProperties = AttributeHelper.GetSeenProperties();
	    private readonly List<string> _titles;
	    private readonly Dictionary<string, string> _modelDict = AttributeHelper.GetSeenPropertyDict();
	    private bool _reachEnd = false;

		public QueryViewModel()
	    {
		    _client = RunContext.Get<Client.Client>();
		    this.PropertyChanged += QueryViewModel_PropertyChanged;
	
		    this._titles = this._modelDict.Keys.ToList();
		    _titles.ForEach(i => _data.Columns.Add(i, typeof(string)));
			this.SetStatus("查询！");
	    }

	    private void QueryViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
	    {
		    switch (e.PropertyName)
		    {
				case nameof(Query):
					if (string.IsNullOrWhiteSpace(Query)) return;
					this.Page = 0;
				    this._reachEnd = false;
					var x = _client.Item.Query(new QueryParser(_query).QueryString, Page, PageLength);
					_data.Rows.Clear();
					x?.Items.ForEach(i =>
					{
						var r = _data.NewRow();
						for (var j = 0; j < _titles.Count; j++)
						{
							r[_titles[j]] = _modelProperties[j].GetValue(i)?.ToString();
						}
						_data.Rows.Add(r);
					});
					return;
				case nameof(LoadProceed):
					if (_reachEnd)return;
					Page++;
					var cont = _client.Item.Query(new QueryParser(_query).QueryString, Page, PageLength);
				    if (Page > 0 && (cont == null || cont.Items.Count == 0)) _reachEnd = true;
					cont?.Items.ForEach(i =>
					{
						var r = _data.NewRow();
						for (var j = 0; j < _titles.Count; j++)
						{
							r[_titles[j]] = _modelProperties[j].GetValue(i)?.ToString();
						}
						_data.Rows.Add(r);
					});
					return;
				default:
					return;
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
