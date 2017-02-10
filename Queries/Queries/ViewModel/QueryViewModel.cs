using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using Common.Factory;
using Common.Static;
using Common.Structure;
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
		private object _tableLock = new object();

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
			SetPercents(Page);
			switch (e.PropertyName)
			{
				case nameof(Query):
					if (string.IsNullOrWhiteSpace(Query))
					{
						_data.Clear();
						return;
					}
					this.Page = 0;
					this._reachEnd = false;
					_client.Item.Query(new QueryParser(_query).QueryString, Page, PageLength)
						.ContinueWith(i =>
						{
							Application.Current.Dispatcher.BeginInvoke(new Action(() =>
							{
								var r = i.Result as ResultItemsModel;
								_data.Rows.Clear();
								if (r?.Items == null)
								{
									OnPropertyChanged(nameof(Data));
									return;
								}
								r.Items.ForEach(j =>
								{
									var s = Data.NewRow();
									for (var k = 0; k < _titles.Count; k++)
									{
										s[_titles[k]] = _modelProperties[k].GetValue(j)?.ToString();
									}
									_data.Rows.Add(s);
								});
								OnPropertyChanged(nameof(Data));
							}));
						});
					return;
				case nameof(LoadProceed):
					if (_reachEnd) return;
					Page++;
					_client.Item.Query(new QueryParser(_query).QueryString, Page, PageLength)
						.ContinueWith(i =>
						{
							Application.Current.Dispatcher.BeginInvoke(new Action(() =>
							{
								var r = i.Result as ResultItemsModel;
								if (Page > 0 && (r == null || r.Items.Count == 0)) _reachEnd = true;
								r?.Items.ForEach(j =>
								{
									var s = Data.NewRow();
									for (var k = 0; k < _titles.Count; k++)
									{
										s[_titles[k]] = _modelProperties[k].GetValue(j)?.ToString();
									}
									_data.Rows.Add(s);
								});
								OnPropertyChanged(nameof(Data));
							}));
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

		private DataTable _data = new DataTable();

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
