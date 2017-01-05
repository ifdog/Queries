using System.Collections.Generic;
using System.IO;
using Common.Static;
using Common.Structure;
using iBoxDB.LocalServer;

namespace Common.DataBase
{
	public static class ItemDb
	{
		private static DB _databBase = new DB();
		private static AutoBox _autoBox;
		private static string _itemName = "ItemModel";
		private static string dbPath = "item";

		 static ItemDb()
		{
			DB.Root($"./{dbPath}/");
			if (!Directory.Exists($"./{dbPath}/"))
			{
				Directory.CreateDirectory($"./{dbPath}/");
			}
			if (!File.Exists($"./{dbPath}/db1.box"))
			{
				var x = File.Create($"./{dbPath}/db1.box");
				x.Close();
			}
			_databBase.GetConfig().EnsureTable<ItemModel>(_itemName, "Id");
			_autoBox = _databBase.Open();
		}

		public static bool Insert(ItemModel obj)
		{
			return _autoBox.Insert(_itemName, obj);
		}

		public static bool MultiInsert(IEnumerable<ItemModel> objects)
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

		public static IEnumerable<ItemModel> Select()
		{
			return _autoBox.Select<ItemModel>($"from {_itemName}");
		}

		public static bool Update(ItemModel obj)
		{
			return _autoBox.Update(_itemName, obj);
		}

		public static bool MultiUpdate(IEnumerable<ItemModel> objects)
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

		public static bool Delete(ItemModel obj)
		{
			return _autoBox.Delete(_itemName, obj);
		}

		public static bool MultiDelete(IEnumerable<ItemModel> objects)
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
	}
}
