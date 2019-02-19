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
        public double Weight { get; set; }
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

        public static Ingredient Copy(Ingredient ingredient_copy)
        {
            Ingredient ingredient = new Ingredient
            {
                Id = ingredient_copy.Id,
                Name = ingredient_copy.Name,
                AmountNow = ingredient_copy.AmountNow,
                Weight = ingredient_copy.Weight,
                AmountUsed = ingredient_copy.AmountUsed,
                Quantily = ingredient_copy.Quantily,
                Demand = ingredient_copy.Demand,
                Price = ingredient_copy.Price
            };
            return ingredient;
        }

        public static void CopyProperties(Ingredient ingredient, Ingredient ingredient_copy)
        {
            ingredient.Id = ingredient_copy.Id;
            ingredient.Name = ingredient_copy.Name;
            ingredient.AmountNow = ingredient_copy.AmountNow;
            ingredient.Weight = ingredient_copy.Weight;
            ingredient.AmountUsed = ingredient_copy.AmountUsed;
            ingredient.Quantily = ingredient_copy.Quantily;
            ingredient.Demand = ingredient_copy.Demand;
            ingredient.Price = ingredient_copy.Price;
        }
    }
}
