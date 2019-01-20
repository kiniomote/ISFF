using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
	public class Product
	{
		// Название столбцов в таблице Товары
		public int Id { get; set; } // Первичный ключ
		public string Name { get; set; }
		public double Price { get; set; }
		public DateTime TimeCook { get; set; }
		public int Weight { get; set; }
		public string NameImage { get; set; }

		// Внешний ключ с Order, связь много ко многим
		public ICollection<Order> Orders { get; set; }

		// Внешний ключ с Ingredient, связь много ко многим
		public ICollection<Ingredient> Ingredients { get; set; }

		public Product()
		{
			Ingredients = new List<Ingredient>();
			Orders = new List<Order>();
		}
	}
}
