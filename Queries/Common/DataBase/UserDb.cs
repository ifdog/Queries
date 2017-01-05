using System;
using System.Collections.Generic;
using System.IO;
using Common.Static;
using Common.Structure;
using iBoxDB.LocalServer;
using Service.Dal.Base;

namespace Common.DataBase
{
	public static class UserDb 
	{
		private static DB _databBase = new DB();
		private static AutoBox _autoBox;
		private static string _itemName = "UserModel";
		private static string dbPath = "user";

		static UserDb()
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
			_databBase.GetConfig().EnsureTable<UserModel>(_itemName, "Id");
			_autoBox = _databBase.Open();
		}

		public static bool Insert(UserModel obj)
		{
			return _autoBox.Insert(_itemName, obj);
		}

		public static bool MultiInsert(IEnumerable<UserModel> objects)
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

		public static IEnumerable<UserModel> Select()
		{
			return _autoBox.Select<UserModel>($"from {_itemName}");
		}

		public static bool Update(UserModel obj)
		{
			return _autoBox.Update(_itemName, obj);
		}

		public static bool MultiUpdate(IEnumerable<UserModel> objects)
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

		public static bool Delete(UserModel obj)
		{
			return _autoBox.Delete(_itemName, obj);
		}

		public static bool MultiDelete(IEnumerable<UserModel> objects)
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
