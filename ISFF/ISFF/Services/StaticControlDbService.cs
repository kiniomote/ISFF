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
            db.Database.ExecuteSqlCommand(
                "ALTER TABLE dbo.DoseIngredients " +
                "ADD CONSTRAINT DoseIngredients_Ingredients " +
                "FOREIGN KEY (IngredientId) " +
                "REFERENCES dbo.Ingredients (Id) " +
                "ON DELETE SET NULL");
            //db.Database.ExecuteSqlCommand(
            //    "ALTER TABLE dbo.DoseIngredients " +
            //    "ADD CONSTRAINT DoseIngredients_Products " +
            //    "FOREIGN KEY (ProductId) " +
            //    "REFERENCES dbo.Products (Id) " +
            //    "ON DELETE SET NULL");
            //db.Database.ExecuteSqlCommand(
            //    "ALTER TABLE dbo.DoseProducts " +
            //    "ADD CONSTRAINT DoseProducts_Orders " +
            //    "FOREIGN KEY (OrderId) " +
            //    "REFERENCES dbo.Orders (Id) " +
            //    "ON DELETE SET NULL");
            db.Database.ExecuteSqlCommand(
                "ALTER TABLE dbo.DoseProducts " +
                "ADD CONSTRAINT DoseProducts_Products " +
                "FOREIGN KEY (ProductId) " +
                "REFERENCES dbo.Products (Id) " +
                "ON DELETE SET NULL");

        } // Загрузка базы данных

        public static void SaveChangesDataBase()
		{
			db.SaveChanges();
		} // Сохранение изменений
	}
}
