using System;
using System.Collections.Generic;
using System.Linq;
using Common.Static;

namespace Common.Factory
{
    public class QueryParser
    {

        private readonly char[] _splitComma = { ',' };
        private readonly char[] _splitColon = { ':' };

	    public QueryParser(string queryString="")
	    {
		    QueryString = queryString;
	    }

        private string _queryString;

	    public string QueryString
	    {
		    get
		    {
			    _queryString = Queries.Count > 0
				    ? $"{QueryHead}@{string.Join(",", Queries.Select(i => $"{i.Key}:{i.Value}"))}"
				    : string.Empty;
			    return _queryString;
		    }
		    set
		    {
			    var x = value.Split(_splitComma, StringSplitOptions.RemoveEmptyEntries);
			    x.Select(i => i.Split(_splitColon, StringSplitOptions.RemoveEmptyEntries))
				    .Where(i => i.Length == 2).ForEach(i =>
				    {

					    Queries.Add(
						    AttributeHelper.GetSeenPropertyDict().ContainsKey(i[0]) ? AttributeHelper.GetSeenPropertyDict()[i[0]] : i[0],
						    i[1]);

				    });
			    if (Queries.Count > 0)
			    {
				    QueryHead = @"All";
			    }
			    else
			    {
				    QueryHead = @"Any";
				    AttributeHelper.GetSearchPropertyNames().ForEach(i => Queries.Add(i, value));
			    }
		    }
	    }

	    public string QueryHead { get; set; }

        public Dictionary<string, string> Queries { get; set; } = new Dictionary<string, string>();

    }
}
