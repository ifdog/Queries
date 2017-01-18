using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Common.Attribute;
using Common.Static;
using Common.Structure;
using Queries.ViewModel.Base;

namespace Queries.ViewModel
{
    public class QueryViewModel : BaseViewModel
    {
        private readonly Client.Client _client;
		private readonly DataTable _data = new DataTable();

	    private readonly List<PropertyInfo> _modelProperties;
	    private readonly List<string> _titles;



		public DataView Items { get; set; }

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
		}

		private void QueryViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Equals(nameof(Query))) return;
            if (string.IsNullOrWhiteSpace(Query)) return;
			_data.Clear();
	        var x = _client.Item.Query(_query, Page, PageLength);
            if (x?.Items != null && x.Items.Count > 0)
            {
	            var c = _titles.Count;
                x.Items.ForEach(i =>
                {
                    var r = _data.NewRow();
                    for (int j = 0; j < c; j++)
                    {
                        r[_titles[j]] = _modelProperties[j].GetValue(i).ToString();
                    }
                    _data.Rows.Add(r);
                });
                Items = _data.DefaultView;
            }
            OnPropertyChanged(nameof(Items));
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

	    private int _page;

	    public int Page
	    {
		    get { return _page; }
		    set
		    {
			    _page = value;
			    OnPropertyChanged(nameof(Page));
		    }
	    }

	    private int _pageLength;

	    public int PageLength
	    {
		    get { return _pageLength; }
		    set
		    {
			    _pageLength = value;
			    OnPropertyChanged(nameof(PageLength));
		    }
	    }

	    public RelayCommand Get
	    {
		    get
		    {
			    return new RelayCommand(() =>
			    {
				    this.Query = Excel.GetInstance();
			    });
		    } 
	    }
    }
}
