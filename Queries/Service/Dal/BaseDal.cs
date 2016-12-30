using System;
using System.Collections.Generic;
using Common.Static;
using iBoxDB.LocalServer;
using Service.Dal.Base;
using Service.Model.Base;

namespace Service.Dal
{
	public class BaseDal<T> : IDal<T>, IDisposable where T : Item, new()
	{
		private readonly DB _databBase = new DB();
		private readonly AutoBox _autoBox;
		private readonly string _itemName;

		public BaseDal(string dbPath = "data")
		{
			_itemName = typeof(T).Name;
			DB.Root($"/{dbPath}/");
			_databBase.GetConfig().EnsureTable<T>(_itemName, "Id");
			_autoBox = _databBase.Open();
		}

		public bool Insert(T obj)
		{
			return _autoBox.Insert(_itemName, obj);
		}

		public bool MultiInsert(IEnumerable<T> objects)
		{
			using (var cube = _autoBox.Cube())
			{
				objects.ForEach(i =>
				{
					_autoBox.Insert(_itemName, i);
				});
				return cube.Commit() == CommitResult.OK;
			}
		}

		public IEnumerable<T> Select()
		{
			return _autoBox.Select<T>($"from {_itemName}");
		}

		public bool Update(T obj)
		{
			return _autoBox.Update(_itemName, obj);
		}

		public bool MultiUpdate(IEnumerable<T> objects)
		{
			using (var cube = _autoBox.Cube())
			{
				objects.ForEach(i =>
				{
					_autoBox.Update(_itemName, i);
				});
				return cube.Commit() == CommitResult.OK;
			}
		}

		public bool Delete(T obj)
		{
			return _autoBox.Delete(_itemName, obj);
		}

		public bool MultiDelete(IEnumerable<T> objects)
		{
			using (var cube = _autoBox.Cube())
			{
				objects.ForEach(i =>
				{
					_autoBox.Delete(_itemName, i);
				});
				return cube.Commit() == CommitResult.OK;
			}
		}

		public bool BackUp(string path)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			_databBase.Close();
		}
	}
}
