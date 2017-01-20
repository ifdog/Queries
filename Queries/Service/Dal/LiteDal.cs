using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Attribute;
using Common.Structure;
using LiteDB;
using Service.Dal.Base;
using Service.Structure;
using Service.Structure.Base;

namespace Service.Dal
{
	public class LiteDal<T> : IDal<T>, IDisposable where T : DbModel, new()
	{
		private readonly LiteDatabase _database;
		private readonly LiteCollection<T> _collection;
		private readonly char[] _splitAt = {'@'};
		private readonly char[] _splitComma = {','};
		private readonly char[] _splitColon = {':'};
		private readonly string _dbItemPrefix;

		public LiteDal(string dbItemPrefix)
		{
			_dbItemPrefix = dbItemPrefix;
			_database = new LiteDatabase($"{typeof(T).Name}s.db");
			_collection = _database.GetCollection<T>(typeof(T).Name);

			var x = typeof(ItemDbModel).GetProperties()
				.Where(i=>i.GetCustomAttribute<TypeIndexedAttribute>()!=null)
				
				.SelectMany(i => i.PropertyType.GetProperties())
				.Where(i => i.GetCustomAttribute<IndexedAttribute>() != null)
				
				.ToList();

		}

		public void Insert(T obj)
		{
			obj.Id = ObjectId.NewObjectId().ToByteArray();
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
			if (w.Length < 2) return new List<T>();
			var x = w[1].Split(_splitComma, StringSplitOptions.RemoveEmptyEntries);
			if (x.Length == 0) return new List<T>();
			if (!x.All(i => i.Contains(':'))) return new List<T>();
			var y = x.Select(i => i.Split(_splitColon, StringSplitOptions.RemoveEmptyEntries)).ToArray();
			if (y.Any(i => i.Length != 2)) return new List<T>();
			switch (w[0])
			{
				case "All":
					return _collection.Find(
						y.Aggregate(Query.All(), (current, sx) => Query.And(current, Query.Contains($"{_dbItemPrefix}.{sx[0]}", sx[1]))),
						page*length, length);
				case "Any":
					if (y.Length == 1)
					{
						var sx = _collection.Find(Query.Contains($"{_dbItemPrefix}.{y[0][0]}", y[0][1]), page * length, length).ToArray();
						return sx;
					}
					var any = Query.Contains($"{_dbItemPrefix}.{y[0][0]}", y[0][1]);
					foreach (var i in y.Skip(1))
					{
						any=Query.And(any,Query.Contains($"{_dbItemPrefix}.{i[0]}", i[1]));
					}
					return _collection.Find(any, page*length, length);
				case "Exa":
					if (y.Length == 1)
						return _collection.Find(Query.EQ($"{_dbItemPrefix}.{y[0][0]}", y[0][1]), page * length, length).ToArray();
					var exa = Query.EQ(y[0][0], y[0][1]);
					foreach (var i in y.Skip(1))
					{
					exa = Query.And(exa, Query.Contains($"{_dbItemPrefix}.{i[0]}", i[1]));
					}
					return _collection.Find(exa, page * length, length);
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
