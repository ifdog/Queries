using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Dal.Base;

namespace Service.Dal
{
	public class PgDal<T>:IDal<T>
	{
		public void Insert(T obj)
		{
			throw new NotImplementedException();
		}

		public void Update(T obj)
		{
			throw new NotImplementedException();
		}

		public void Delete(T obj)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<T> Find(string query, int page, int length)
		{
			throw new NotImplementedException();
		}
	}
}
