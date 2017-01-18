using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using LiteDB;
using Service.Dal.Base;

namespace Service.Dal
{
	public class LiteDal<T> : IDal<T>,IDisposable where T :  new()
	{
		private readonly LiteDatabase _database;
		private readonly LiteCollection<T> _collection;
		private readonly char[] _splitAt =  {'@'};
		private readonly char[] _splitComma = {','};
		private readonly char[] _splitColon = {':'};

		public LiteDal()
		{
			_database = new LiteDatabase($"{typeof(T).Name}s.db");
			_collection = _database.GetCollection<T>(typeof(T).Name);
		}

		public void Insert(T obj)
		{
			_collection.Insert(obj);
		}

		public void Update(T obj)
		{
			 _collection.Update(obj);
		}

		public void Delete(T obj)
		{
			throw new NotImplementedException();
		}

		//Each@Name:xxx,Spec:yyy,Brand:zzz		Search.And
		//Any @Name:xxx,Spec:yyy,Brand:zzz		Search.Or
		public IEnumerable<T> Find(string query, int page = 0, int length = 50)
		{
			var w = query.Split(_splitAt, StringSplitOptions.RemoveEmptyEntries);
			if (w.Length < 2) return null;
			var x = w[1].Split(_splitComma, StringSplitOptions.RemoveEmptyEntries);
			if (x.Length == 0) return null;
			if (!x.All(i => i.Contains(':'))) return null;
			var y = x.Select(i => i.Split(_splitColon, StringSplitOptions.RemoveEmptyEntries)).ToArray();
			var q = Query.Where(y[0][0], i => i.AsString.Contains(y[0][1]));
			if (y.Length > 1)
			{
				var z = y.Skip(1).ToArray();
				switch (w[0])
				{
					case "Each":
						q = z.Aggregate(q, (current, sx) => Query.And(current, Query.Where(sx[0], i => i.AsString.Contains(sx[1]))));
						break;
					case "Any":
						q = z.Aggregate(q, (current, sx) => Query.Or(current, Query.Where(sx[0], i => i.AsString.Contains(sx[1]))));
						break;
					default:
						return null;
				}
			}
			return _collection.Find(q, page*length, length);
		}


		public void Dispose()
		{
			_database.Dispose();
		}
	}
}
