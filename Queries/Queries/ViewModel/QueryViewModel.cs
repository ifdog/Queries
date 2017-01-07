using System.Collections.Generic;
using Common.Structure;
using Queries.ViewModel.Base;

namespace Queries.ViewModel
{
    public class QueryViewModel:BaseViewModel
    {
        private string _query;
        private List<ItemModel> _items;
        private readonly Client.Client _client;

        public QueryViewModel()
        {
            _client = RunContext.Get<Client.Client>();
            this.PropertyChanged += QueryViewModel_PropertyChanged;
        }

        private void QueryViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Query)))
            {
                var x = _client.Item.Query(_query);
                if (x!= null)
                {
                    Items = x.Items;
                }
            }
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

        public List<ItemModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
    }
}
