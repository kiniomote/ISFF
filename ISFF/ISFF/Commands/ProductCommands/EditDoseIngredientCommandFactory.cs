using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class EditDoseIngredientCommandFactory : CommonRelayCommandFactory
    {
        public override Action<object> Execute()
        {
            return param =>
            {
                KitParametrsProducts kitParametrs = param as KitParametrsProducts;

                List<INameable> items = new List<INameable>();
                foreach (Ingredient ingredient in kitParametrs.db.Ingredients.ToList())
                {
                    items.Add(ingredient);
                }
                KitParametrsDose kitParametrsDose = DialogWindowService.OpenDoseDialogWindow(items,
                    kitParametrs.SelectedDoseIngredient.Ingredient,
                    kitParametrs.SelectedDoseIngredient.CountIngredient);
                if (kitParametrsDose == null || kitParametrsDose.CountItems <= 0)
                    return;
                DoseIngredient doseIngredient = DoseIngredient.Copy(kitParametrs.SelectedDoseIngredient);
                if (kitParametrs.db.DoseIngredients.SingleOrDefault(c => c.Id == kitParametrs.SelectedDoseIngredient.Id) != null &&
                    !kitParametrs.IdElementsForRemove.Contains(kitParametrs.SelectedDoseIngredient.Id))
                    kitParametrs.IdElementsForRemove.Add(kitParametrs.SelectedDoseIngredient.Id);
                kitParametrs.DoseIngredients.Remove(kitParametrs.SelectedDoseIngredient);
                doseIngredient.Ingredient = (Ingredient)kitParametrsDose.SelectedItem;
                doseIngredient.CountIngredient = kitParametrsDose.CountItems;
                kitParametrs.SelectedDoseIngredient = doseIngredient;
                kitParametrs.DoseIngredients.Add(kitParametrs.SelectedDoseIngredient);
            };
        }
        public override Func<object, bool> CanExecute()
        {
            return param =>
            {
                if (CheckUserAccessService.IsNotAdministrator())
                    return false;
                bool enable = true;
                if (param is KitParametrsProducts kitParametrs && kitParametrs.SelectedDoseIngredient != null)
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
