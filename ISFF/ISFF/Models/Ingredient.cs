using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISFF
{
	public class Ingredient : INameable, IDataErrorInfo, ILogable
    {
        #region Constant

        const string NAME = "Name";
        const string AMOUNT_NOW = "AmountNow";
        const string AMOUNT_USED = "AmountUsed";
        const string QUANTILY = "Quantily";
        const string PRICE = "Price";
        const string WEIGHT = "Weight";

        #endregion

        #region DataEntity
        // Название столбцов в таблице Ингредиенты
        public int Id { get; set; }
		public string Name { get; set; }
		public double AmountNow { get; set; }
		public double AmountUsed { get; set; }
		public string Quantily { get; set; }
        public int Weight { get; set; }
		public double Price { get; set; }

        // Внешний ключ с DoseIngredient, связь один ко многим
        public ICollection<DoseIngredient> DoseIngredients { get; set; }

		public Ingredient()
		{
            DoseIngredients = new List<DoseIngredient>();
            CorrectData = new CorrectDataService(new Dictionary<string, bool>
            {
                { NAME, false }, { AMOUNT_NOW, false}, { AMOUNT_USED, false}, { QUANTILY, false }, { PRICE, false}, {WEIGHT, false}
            });
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
                        if (Name == null || Name == string.Empty )
                            error = "Поле должно быть заполнено";
                        break;
                    case QUANTILY:
                        if (Quantily == null || Quantily == string.Empty )
                            error = "Поле должно быть заполнено";
                        break;
                    case AMOUNT_NOW:
                        if (AmountNow < 0)
                            error = "Некорректное указанное количество";
                        break;
                    case AMOUNT_USED:
                        if (AmountUsed < 0)
                            error = "Некорректное указанное количество";
                        break;
                    case PRICE:
                        if (Price <= 0)
                            error = "Цена не может быть отрицательной или нулевой";
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

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region CopyMethods

        public static Ingredient Copy(Ingredient ingredient_copy)
        {
            Ingredient ingredient = new Ingredient
            {
                Id = ingredient_copy.Id,
                Name = ingredient_copy.Name,
                AmountNow = ingredient_copy.AmountNow,
                AmountUsed = ingredient_copy.AmountUsed,
                Quantily = ingredient_copy.Quantily,
                Price = ingredient_copy.Price,
                Weight = ingredient_copy.Weight
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
            ingredient.Weight = ingredient_copy.Weight;
        }

        #endregion
    }
}
