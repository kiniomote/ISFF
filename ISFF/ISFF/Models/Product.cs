using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ISFF
{
	public class Product : INameable, IDataErrorInfo
    {
        #region Constant

        const string NAME = "Name";
        const string TIME_COOK = "TimeCook";
        const string WEIGHT = "Weight";
        const string PRICE = "Price";
        const string DEFAULT_QANTILY = "шт.";

        #endregion

        #region DataEntity
        // Название столбцов в таблице Товары
        public int Id { get; set; } // Первичный ключ
		public string Name { get; set; }
		public double Price { get; set; }
		public double TimeCook { get; set; }
		public int Weight { get; set; }
		public byte[] Image { get; set; }

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
            Quantily = DEFAULT_QANTILY;
            CorrectData = new CorrectDataService(new Dictionary<string, bool>
            {
                { NAME, false }, { TIME_COOK, false}, { WEIGHT, false}, { PRICE, false}
            });
            Image = ConverterByteImage.ImageToByte(ConverterByteImage.ToBitmapImage(Properties.Resources.None_image));
        }

        #endregion

        #region Validation

        [NotMapped]
        public CorrectDataService CorrectData { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case NAME:
                        if(Name == null || Name == string.Empty)
                            error = "Поле должно быть заполнено";
                        break;
                    case PRICE:
                        if (Price <= 0)
                            error = "Цена не может быть отрицательной или нулевой";
                        break;
                    case TIME_COOK:
                        if (TimeCook < 0)
                            error = "Время готовки не может быть отрицательным";
                        break;
                    case WEIGHT:
                        if (Weight <= 0)
                            error = "Вес не может быть отрицательным или нулевым";
                        break;
                }
                CorrectData.CheckCorrect(columnName, error);
                return error;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Methods
        
        [NotMapped]
        public double PriceIngredients
        {
            get
            {
                double priceIngredients = 0;
                foreach (DoseIngredient doseIngredient in DoseIngredients)
                {
                    priceIngredients += doseIngredient.Ingredient.Price * doseIngredient.CountIngredient;
                }
                return priceIngredients;
            }
            set { }
        }

        [NotMapped]
        public int WeightIngredients
        {
            get
            {
                int weightIngredients = 0;
                foreach (DoseIngredient doseIngredient in DoseIngredients)
                {
                    weightIngredients += doseIngredient.Ingredient.Weight * doseIngredient.CountIngredient;
                }
                return weightIngredients;
            }
            set { }
        }
        
        #endregion

        #region CopyMethods

        public static Product Copy(Product product_copy)
        {
            Product product = new Product
            {
                Id = product_copy.Id,
                Name = product_copy.Name,
                Price = product_copy.Price,
                TimeCook = product_copy.TimeCook,
                Weight = product_copy.Weight,
                Image = product_copy.Image,
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
            product.Image = product_copy.Image;
        }

        #endregion
    }
}
