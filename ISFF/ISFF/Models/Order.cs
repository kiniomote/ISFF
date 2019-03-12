using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISFF
{
	public class Order : ILogable
	{
        [NotMapped]
        public static Color COLOR_READY = Colors.Green; // Green
        [NotMapped]
        public static Color COLOR_NOT_READY = Colors.Red; // Red
        const string TEXT_NOT_ENOUGH = "Нехватает ингредиентов!";

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

        public override string ToString()
        {
            return "заказ №" + Id;
        }

        [NotMapped]
        public string TimeString
        {
            get { return Time.ToLongTimeString(); }
            set { }
        }

        public bool TryCompleteOrder()
        {
            bool canReady = true;
            foreach(DoseProduct doseProduct in DoseProducts)
            {
                if (!doseProduct.CanCook())
                    canReady = false;
            }
            if (!canReady)
            {
                DialogWindowService.OpenDialogWindow(TEXT_NOT_ENOUGH);
                return canReady;
            }
            foreach (DoseProduct doseProduct in DoseProducts)
            {
                doseProduct.Cook();
            }
            return canReady;
        }
	}
}
