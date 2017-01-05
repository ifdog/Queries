using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Common.DataBase.Base
{
	public interface IDal<T> where T : Structure.Base.BaseObject
	{
		void Insert(T obj);
		IEnumerable<T> Find(Expression<Func<T, bool>> pridecate, int skip, int max);
		void Update(T obj);
		void Delete(T obj);
		void BackUp(string path);
	}
}
