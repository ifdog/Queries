using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common.Attribute;
using Common.Factory;
using LiteDB;
using NLog;
using Service.Dal.Base;
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

			var parser = new QueryParser(query);

		    switch (parser.QueryHead)
		    {
		        case "All":
                    return _collection.FindAll().Where(GetAllExpression(parser.Queries).Compile()).Skip(page * length).Take(length);
                case "Any":
                    return _collection.FindAll().Where(GetAnyExpression(parser.Queries).Compile()).Skip(page * length).Take(length);
                case "Exa":
                    return _collection.FindAll().Where(GetExaExpression(parser.Queries).Compile()).Skip(page * length).Take(length);
                default:
		            return null;
		    }
        }

        public static Expression<Func<T, bool>> GetAnyExpression(List<KeyValuePair<string,string>> pairs)
        {
            var searchItem = typeof(T).GetProperties().First(i => i.GetCustomAttribute<TypeIndexedAttribute>() != null).Name;
            var left = Expression.Parameter(typeof(T), "c");
            Expression expression = Expression.Constant(false);
            expression = pairs.Select(pair => Expression.Call(Expression.Property(Expression.Property(left, searchItem), pair.Key),
                typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                Expression.Constant(pair.Value)
            )).Aggregate<Expression, Expression>(expression, (current, right) => Expression.Or(right, current));
            return Expression.Lambda<Func<T, bool>>(expression, left);
        }

        public static Expression<Func<T, bool>> GetAllExpression(List<KeyValuePair<string, string>> pairs)
        {
            var searchItem = typeof(T).GetProperties().First(i => i.GetCustomAttribute<TypeIndexedAttribute>() != null).Name;
            var left = Expression.Parameter(typeof(T), "c");
            Expression expression = Expression.Constant(true);
            expression = pairs.Select(pair => Expression.Call(Expression.Property(Expression.Property(left, searchItem), pair.Key),
                typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                Expression.Constant(pair.Value)
            )).Aggregate<Expression, Expression>(expression, (current, right) => Expression.And(right, current));
            return Expression.Lambda<Func<T, bool>>(expression, left);
        }

        public static Expression<Func<T, bool>> GetExaExpression(List<KeyValuePair<string, string>> pairs)
        {
            var searchItem = typeof(T).GetProperties().First(i => i.GetCustomAttribute<TypeIndexedAttribute>() != null).Name;
            var left = Expression.Parameter(typeof(T), "c");
            Expression expression = Expression.Constant(true);
            expression = pairs.Select(pair => Expression.Call(Expression.Property(Expression.Property(left, searchItem), pair.Key),
                typeof(string).GetMethod("Equals", new[] { typeof(string) }),
                Expression.Constant(pair.Value)
            )).Aggregate<Expression, Expression>(expression, (current, right) => Expression.And(right, current));
           return Expression.Lambda<Func<T, bool>>(expression, left);
        }

        public void Dispose()
		{
			_database.Dispose();
		}
	}
}
