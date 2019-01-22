using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ISFF
{
	// Класс-посредник между между БД и программой с помощью технологии Entity Framework
	public class ConnectionDB : DbContext
	{
		public ConnectionDB() : base("DefaultConnection")
		{ }

		public DbSet<Employee> Employees { get; set; } // Таблица Сотрудники
		public DbSet<Ingredient> Ingredients { get; set; } // Таблица Ингредиенты
		public DbSet<Product> Products { get; set; } // Таблица Товары
		public DbSet<Order> Orders { get; set; } // Таблица Заказы
        public DbSet<DoseIngredient> DoseIngredients { get; set; } // Таблица Дозы ингредиентов
        public DbSet<DoseProduct> DoseProducts { get; set; } // Таблица Дозы товаров
	}
}
