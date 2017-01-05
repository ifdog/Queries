using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using Service.Dal.Base;

namespace Common.DataBase
{
	public class LiteDal<T> : IDal<T> where T : Common.Structure.Base.BaseObject, new()
	{
		private LiteDatabase _database;
		private LiteCollection<T> _collection;
		public LiteDal()
		{
			_database = new LiteDatabase($"{typeof(T).Name}.db");
			_collection = _database.GetCollection<T>(typeof(T).Name);
		}

		public bool BackUp(string path)
		{
			throw new NotImplementedException();
		}

		public bool Delete(T obj)
		{
			throw new NotImplementedException();
		}

		public bool Insert(T obj)
		{
			_collection.Insert(obj);
		}

		public bool MultiDelete(IEnumerable<T> objects)
		{
			throw new NotImplementedException();
		}

		public bool MultiInsert(IEnumerable<T> objects)
		{
			throw new NotImplementedException();
		}

		public bool MultiUpdate(IEnumerable<T> objects)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<T> Select()
		{
			throw new NotImplementedException();
		}

		public bool Update(T obj)
		{
			_collection.Update(obj);
		}
	}
}
