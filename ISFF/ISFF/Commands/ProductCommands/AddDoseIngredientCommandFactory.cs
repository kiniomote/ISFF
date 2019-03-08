using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class AddDoseIngredientCommandFactory : CommonRelayCommandFactory
    {
        public override Action<object> Execute()
        {
            return param =>
            {
                KitParametrsProducts kitParametrs = param as KitParametrsProducts;

                List<INameable> items = new List<INameable>();
                foreach (Ingredient ingredient in kitParametrs.db.Ingredients)
                {
                    items.Add(ingredient);
                }

                KitParametrsDose kitParametrsDose = DialogWindowService.OpenDoseDialogWindow(items);
                if (kitParametrsDose == null)
                    return;
                kitParametrs.DoseIngredients.Add(new DoseIngredient()
                {
                    Product = kitParametrs.SelectedProduct,
                    Ingredient = (Ingredient)kitParametrsDose.SelectedItem,
                    CountIngredient = kitParametrsDose.CountItems
                });
            };
        }
        public override Func<object, bool> CanExecute()
        {
            return param =>
            {
                if (CheckUserAccessService.IsNotAdministrator())
                    return false;
                bool enable = true;
                if (param is KitParametrsProducts kitParametrs)
                {
                    if (!kitParametrs.IsBusy)
                        enable = false;
                }
                else
                    enable = false;
                return enable;
            };
        }
    }
}
