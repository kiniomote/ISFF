using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ISFF
{
	public class Ingredient : INameable, IDataErrorInfo
	{
		// Название столбцов в таблице Ингредиенты
		public int Id { get; set; }
		public string Name { get; set; }
		public double AmountNow { get; set; }
		public double AmountUsed { get; set; }
		public string Quantily { get; set; }
		public double Price { get; set; }

        // Внешний ключ с DoseIngredient, связь один ко многим
        public ICollection<DoseIngredient> DoseIngredients { get; set; }

		public Ingredient()
		{
            DoseIngredients = new List<DoseIngredient>();
		}

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "AmountNow":
                        if (AmountNow < 0)
                            error = "Некорректное указанное количество";
                        break;
                    case "AmountUsed":
                        if (AmountUsed < 0)
                            error = "Некорректное указанное количество";
                        break;
                    case "Price":
                        if (Price <= 0)
                            error = "Цена не может быть отрицательной или нулевой";
                        break;
                }
                return error;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public static Ingredient Copy(Ingredient ingredient_copy)
        {
            Ingredient ingredient = new Ingredient
            {
                Id = ingredient_copy.Id,
                Name = ingredient_copy.Name,
                AmountNow = ingredient_copy.AmountNow,
                AmountUsed = ingredient_copy.AmountUsed,
                Quantily = ingredient_copy.Quantily,
                Price = ingredient_copy.Price
            };
            return ingredient;
        }

        public static void CopyProperties(Ingredient ingredient, Ingredient ingredient_copy)
        {
            ingredient.Id = ingredient_copy.Id;
            ingredient.Name = ingredient_copy.Name;
            ingredient.AmountNow = ingredient_copy.AmountNow;
            ingredient.AmountUsed = ingredient_copy.AmountUsed;
            ingredient.Quantily = ingredient_copy.Quantily;
            ingredient.Price = ingredient_copy.Price;
        }
    }
}
