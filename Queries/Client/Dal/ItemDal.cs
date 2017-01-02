using Common.Structure;
using RestSharp;

namespace Client.Dal
{
	public class ItemDal
	{
	    private readonly RestClient _restClient;

	    public ItemDal(RestClient restClient)
	    {
	        this._restClient = restClient;
	    }

	    public ResultItemsModel Post(RequestItemModel requestItemModel)
	    {
            var request = new RestRequest("v2/items/{Action}", Method.POST)
            {
                RequestFormat = DataFormat.Json
            }.AddUrlSegment("Action", requestItemModel.Action.ToLower())
              .AddJsonBody(requestItemModel);
            var response = _restClient.Execute<ResultItemsModel>(request);
            return response.Data;
        }

	    public ResultItemsModel Query(string query)
	    {
	        var request = new RestRequest("v2/items/{Query}", Method.GET)
	            .AddUrlSegment("Query", query);
	        var response = _restClient.Execute<ResultItemsModel>(request);
	        return response.Data;
	    }
	}
}
