using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Structure;
using RestSharp;

namespace Client.Dal
{
	public class UserDal
	{
	    private string _serverDomain;
	    private RestClient _restClient;
	    private RestRequest _restRequest;

	    public UserDal(RestClient restClient)
        {
            _restClient = restClient;
            _restRequest = new RestRequest("v2/users/{action}");
        }

	    public ResultUserModel Register(UserModel userModel)
	    {
	        _restRequest.AddUrlSegment("action", "register");
            _restRequest.Method = Method.POST;
	        _restRequest.AddJsonBody(userModel);
	        return _restClient.Execute<ResultUserModel>(_restRequest).Data;
	    }
	}
}
