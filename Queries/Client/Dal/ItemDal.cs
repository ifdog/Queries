using System.Linq;
using Common.Static;
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
		    _restClient.Proxy = null;
	    }

	    public ResultItemsModel Post(RequestItemsModel requestItemsModel)
	    {
            var request = new RestRequest("v2/items/{Action}", Method.POST)
            {
                RequestFormat = DataFormat.Json
            }.AddUrlSegment("Action", requestItemsModel.Action.ToLower())
              .AddJsonBody(requestItemsModel);
            var response = _restClient.Execute<ResultItemsModel>(request);
            return response.Data;
        }

	    public ResultItemsModel Query(string query,int page,int length)
	    {
	        var request = new RestRequest("v2/items/get/{Query}", Method.GET)
	            .AddUrlSegment("Query", query)
				.AddQueryParameter("Page",page.ToString())
				.AddQueryParameter("Length",length.ToString());
	        var response = _restClient.Execute(request);
            return Json.ToObject<ResultItemsModel>(response.Content);
	    }
	}
}
