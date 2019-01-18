using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF_v1._0
{
	public class Order
	{
		// Название столбцов в таблице Заказы
		public int Id { get; set; }
		public double Price { get; set; }
		public DateTime Time { get; set; }
		public bool Ready { get; set; }

		// Внешний ключ с Product, связь много ко многим
		public ICollection<Product> Products { get; set; }

		public Order()
		{
			Products = new List<Product>();
		}
	}
}
