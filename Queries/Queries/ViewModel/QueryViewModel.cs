using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common.Static;
using Common.Structure;
using Queries.ViewModel.Base;

namespace Queries.ViewModel
{
    public class QueryViewModel:BaseViewModel
    {
        private string _query;
        private readonly Client.Client _client;

        public QueryViewModel()
        {
            _client = RunContext.Get<Client.Client>();
            this.PropertyChanged += QueryViewModel_PropertyChanged;
        }

        private void QueryViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Equals(nameof(Query))) return;
            var x = _client.Item.Query(_query);
            if (x!= null&&x.Items.Count>0)
            {
                var data = new DataTable();
                x.Items[0].ForEach(i=>data.Columns.Add(i,typeof(string)));
                var c = x.Items[0].Length;
                x.Items.Skip(1).ForEach(i =>
                {
                    var r = data.NewRow();
                    for (int j = 0; j < c; j++)
                    {
                        r[x.Items[0][j]] = i[j];
                    }
                    data.Rows.Add(r);
                });
                Items = data.DefaultView;
            }
            OnPropertyChanged(nameof(Items));
        }

        public string Query
        {
            get { return _query; }
            set
            {
                _query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        public DataView Items { get; set; }
    }
}
