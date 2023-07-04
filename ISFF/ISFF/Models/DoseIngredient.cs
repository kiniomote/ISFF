using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class DoseIngredient : IEntity
    {
        public int Id { get; set; }
        public int CountIngredient { get; set; }

        public int? IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } 
        
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public static DoseIngredient Copy(DoseIngredient dose_copy)
        {
            DoseIngredient dose = new DoseIngredient
            {
                Id = dose_copy.Id,
                Ingredient = dose_copy.Ingredient,
                Product = dose_copy.Product,
                IngredientId = dose_copy.IngredientId,
                ProductId = dose_copy.ProductId
            };
            return dose;
        }

        public static void CopyProperties(DoseIngredient dose, DoseIngredient dose_copy)
        {
            dose.Id = dose_copy.Id;
            dose.Ingredient = dose_copy.Ingredient;
            dose.Product = dose_copy.Product;
            dose.IngredientId = dose_copy.IngredientId;
            dose.ProductId = dose_copy.ProductId;
        }
    }
}
