using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace ISFF
{
	// Чтение базы данных и сохранение изменений в ней
	public static class StaticControlDbService
	{
		public static ConnectionDB db;

		public static void LoadDataBase()
		{
			db = new ConnectionDB();
			db.Employees.Load();
			db.Ingredients.Load();
			db.Products.Load();
			db.Orders.Load();
		} // Загрузка базы данных

		public static void SaveChangesDataBase()
		{
			db.SaveChanges();
		} // Сохранение изменений
	}
}
