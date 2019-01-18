using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF_v1._0
{
	public class Ingredient
	{
		// Название столбцов в таблице Ингредиенты
		public int Id { get; set; }
		public string Name { get; set; }
		public double AmountNow { get; set; }
		public double AmountUsed { get; set; }
		public string Quantily { get; set; }
		public int Demand { get; set; }
		public double Price { get; set; }

		// Внешний ключ с Product, связь много ко многим
		public ICollection<Product> Products { get; set; }

		public Ingredient()
		{
			Products = new List<Product>();
		}
	}
}
