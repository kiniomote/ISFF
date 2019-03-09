using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class DoseProduct
    {
        public int Id { get; set; }
        public int CountProduct { get; set; }

        public int? ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }


        public bool CanCook()
        {
            bool canReady = true;
            foreach (DoseIngredient doseIngredient in Product.DoseIngredients)
            {
                if (doseIngredient.Ingredient.AmountNow < doseIngredient.CountIngredient * CountProduct)
                    canReady = false;
            }
            return canReady;
        }

        public void Cook()
        {
            foreach (DoseIngredient doseIngredient in Product.DoseIngredients)
            {
                doseIngredient.Ingredient.AmountNow -= doseIngredient.CountIngredient * CountProduct;
                doseIngredient.Ingredient.AmountUsed += doseIngredient.CountIngredient * CountProduct;
            }
        }

    }
}
