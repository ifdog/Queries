using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model
{
    public interface IDataBase<T> where T : class
    {
        void Insert(T obj);
        IEnumerable<T> Select();
        void Update(T obj);
        void Delete(T obj);
    }
}
