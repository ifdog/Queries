using System;
using System.Collections.Generic;
using iBoxDB.LocalServer;

namespace Service.Model
{
    public class BoxDb<T> : IDataBase<T> ,IDisposable where T : class, new()
    {
        private readonly DB _databBase = new DB();
        private readonly AutoBox _autoBox;
        private readonly string _itemName;
        public BoxDb()
        {
            _itemName = typeof(T).Name;
            DB.Root("/data/");
            _databBase.GetConfig().EnsureTable<T>(_itemName, "Id");
            _autoBox = _databBase.Open();
        }

        public void Insert(T obj)
        {
            _autoBox.Insert(_itemName, obj);
        }

        public IEnumerable<T> Select()
        {
            return _autoBox.Select<T>($"from {_itemName}");
        }

        public void Update(T obj)
        {
            _autoBox.Update(_itemName, obj);
        }

        public void Delete(T obj)
        {
            _autoBox.Delete(_itemName, obj);
        }

        public void Dispose()
        {
            _databBase.Close();
        }
    }
}
