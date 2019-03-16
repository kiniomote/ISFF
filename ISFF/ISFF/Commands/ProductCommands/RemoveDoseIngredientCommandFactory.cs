using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class RemoveDoseIngredientCommandFactory : CommonRelayCommandFactory
    {
        public override Action<object> Execute()
        {
            return param =>
            {
                KitParametrsProducts kitParametrs = param as KitParametrsProducts;
                if (kitParametrs.db.DoseIngredients.GetItem(kitParametrs.SelectedDoseIngredient.Id) != null &&
                    !kitParametrs.IdElementsForRemove.Contains(kitParametrs.SelectedDoseIngredient.Id))
                    kitParametrs.IdElementsForRemove.Add(kitParametrs.SelectedDoseIngredient.Id);
                kitParametrs.DoseIngredients.Remove(kitParametrs.SelectedDoseIngredient);
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
