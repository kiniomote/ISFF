using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ISFF_v1._0
{
	// Класс-посредник между между БД и программой с помощью технологии Entity Framework
	public class ConnectionDB : DbContext
	{
		public ConnectionDB()
		: base("Connection")
		{ }

		public DbSet<Employee> Employees { get; set; } // Таблица Сотрудники
		public DbSet<Ingredient> Ingredients { get; set; } // Таблица Ингредиенты
		public DbSet<Product> Products { get; set; } // Таблица Товары
		public DbSet<Order> Orders { get; set; } // Таблица Заказы
	}
}
