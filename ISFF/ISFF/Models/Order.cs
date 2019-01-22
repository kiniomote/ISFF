using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
	public class Order
	{
		// Название столбцов в таблице Заказы
		public int Id { get; set; }
		public double Price { get; set; }
		public DateTime Time { get; set; }
		public bool Ready { get; set; }

        // Внешний ключ с DoseProduct, связь один ко многим
        public ICollection<DoseProduct> DoseProducts { get; set; }

		public Order()
		{
            DoseProducts = new List<DoseProduct>();
		}
	}
}
