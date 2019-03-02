using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISFF
{
	public class Product : INameable
	{
		// Название столбцов в таблице Товары
		public int Id { get; set; } // Первичный ключ
		public string Name { get; set; }
		public double Price { get; set; }
		public double TimeCook { get; set; }
		public int Weight { get; set; }
		public string NameImage { get; set; }

        [NotMapped]
        public string Quantily { get; set; }

        // Внешний ключ с DoseProduct, связь один ко многим
        public ICollection<DoseProduct> DoseProducts { get; set; }

        // Внешний ключ с DoseIngredient, связь один ко многим
        public ICollection<DoseIngredient> DoseIngredients { get; set; }

		public Product()
		{
            DoseIngredients = new List<DoseIngredient>();
            DoseProducts = new List<DoseProduct>();
            Quantily = "шт";
		}

        public static Product Copy(Product product_copy)
        {
            Product product = new Product
            {
                Id = product_copy.Id,
                Name = product_copy.Name,
                Price = product_copy.Price,
                TimeCook = product_copy.TimeCook,
                Weight = product_copy.Weight,
                NameImage = product_copy.NameImage,
                DoseIngredients = DeepCopyCollection<DoseIngredient>.CopyToList(product_copy.DoseIngredients),
                DoseProducts = DeepCopyCollection<DoseProduct>.CopyToList(product_copy.DoseProducts)
            };
            return product;
        }

        public static void CopyProperties(Product product, Product product_copy)
        {
            product.Id = product_copy.Id;
            product.Name = product_copy.Name;
            product.Price = product_copy.Price;
            product.TimeCook = product_copy.TimeCook;
            product.Weight = product_copy.Weight;
            product.NameImage = product_copy.NameImage;
        }
    }
}
