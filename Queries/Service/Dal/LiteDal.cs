using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Service.Dal.Base;

namespace Service.Dal
{
	public class LiteDal<T> : IDal<T>, IDisposable where T : new()
	{
		private readonly LiteDatabase _database;
		private readonly LiteCollection<T> _collection;
		private readonly char[] _splitAt = {'@'};
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

		//All@Name:xxx,Spec:yyy,Brand:zzz		Search.And
		//Any@Name:xxx,Spec:yyy,Brand:zzz		Search.Or
		//Exa@Name:xxx,Spec:yyy,Brand:zzz		Search.Exactly
		public IEnumerable<T> Find(string query, int page = 0, int length = 50)
		{
			var w = query.Split(_splitAt, StringSplitOptions.RemoveEmptyEntries);
			if (w.Length < 2) return null;
			var x = w[1].Split(_splitComma, StringSplitOptions.RemoveEmptyEntries);
			if (x.Length == 0) return null;
			if (!x.All(i => i.Contains(':'))) return null;
			var y = x.Select(i => i.Split(_splitColon, StringSplitOptions.RemoveEmptyEntries)).ToArray();
			if (y.Any(i => i.Length != 2)) return null;
			switch (w[0])
			{
				case "All":
					return _collection.Find(
						y.Aggregate(Query.All(), (current, sx) => Query.And(current, Query.Contains(sx[0], sx[1]))), page*length, length);
				case "Any":
					return _collection.Find(
						y.Aggregate(Query.All(), (current, sx) => Query.Or(current, Query.Contains(sx[0], sx[1]))), page*length, length);
				case "Exa":
					return _collection.Find(
						y.Aggregate(Query.All(), (current, sx) => Query.And(current, Query.EQ(sx[0], sx[1]))), page*length, length);
				default:
					return null;
			}
		}

	public void Dispose()
		{
			_database.Dispose();
		}
	}
}
