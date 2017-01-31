using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Structure;
using LiteDB;
using NLog;
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
	    private NLog.Logger _logger = LogManager.GetCurrentClassLogger();

		public LiteDal(string dbItemPrefix)
		{
			_dbItemPrefix = dbItemPrefix;
			_database = new LiteDatabase($"{typeof(T).Name}s.db");
			_collection = _database.GetCollection<T>(typeof(T).Name);


		     //typeof(T).GetProperties()
		     //   .Where(i => i.GetCustomAttribute<TypeIndexedAttribute>() != null).ToList()
		     //   .SelectMany(i =>
		     //   {
		     //       var p = i.GetCustomAttribute<BsonFieldAttribute>();
		     //       return i.PropertyType.GetProperties()
		     //           .Select(property => new {Name = p == null ? i.Name : p.Name, Property = property});
		     //   })
		     //   .Where(i => i.Property.GetCustomAttribute<IndexedAttribute>() != null)
		     //   .Select(i => $"{i.Name}.{i.Property.Name}")
		     //   .ForEach(i => _collection.EnsureIndex(i));

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
		    Func<T, bool> func;

		    switch (w[0])
		    {
		        case "All":
		            func = y.Aggregate(_head,
		                (current, i) => And(current, Contains(i[0].Split('.')[0], i[0].Split('.')[1], i[1])));
		            break;
		        case "Any":
		            func = i => (i as ItemDbModel).FlatItem.Spec.Contains("101");
		            break;
		        case "Exa":
		            func = y.Aggregate(_head,
		                (current, i) => And(current, Equals(i[0].Split('.')[0], i[0].Split('.')[1], i[1])));
		            break;
		        default:
		            return null;
		    }

            return _collection.FindAll().Where(func).Skip(page * length).Take(length);
        }

        private static Func<T, bool> And(Func<T, bool> funcA, Func<T, bool> funcB)
	    {
	        return arg => funcA(arg) && funcB(arg);
	    }

	    private static Func<T, bool> Or(Func<T, bool> funcA, Func<T, bool> funcB)
	    {
	        return arg => funcA(arg) || funcB(arg);
	    }

	    private static Func<T, bool> Contains(string className,string subName, string value)
	    {

	        return arg =>
	        {
	            var subProperty = typeof(T).GetProperty(className).GetValue(arg);
	            return subProperty.GetType().GetProperty(subName).GetValue(subProperty).ToString().Contains(value);
	        };
	    }

	    private static Func<T, bool> Equals(string className, string subName, string value)
	    {
            return arg =>
            {
                var subProperty = typeof(T).GetProperty(className).GetValue(arg);
                return subProperty.GetType().GetProperty(subName).GetValue(subProperty).ToString().Equals(value);
            };
        }

	    private readonly Func<T, bool> _head = arg => true;

	    public void Dispose()
		{
			_database.Dispose();
		}
	}
}
