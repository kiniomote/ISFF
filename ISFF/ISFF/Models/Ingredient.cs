using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
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

        // Внешний ключ с DoseIngredient, связь один ко многим
        public ICollection<DoseIngredient> DoseIngredients { get; set; }

		public Ingredient()
		{
            DoseIngredients = new List<DoseIngredient>();
		}
	}
}
