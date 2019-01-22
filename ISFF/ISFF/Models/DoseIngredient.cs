using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class DoseIngredient
    {
        public int Id { get; set; }
        public int CountIngredient { get; set; }

        public int? IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } 
        
        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
