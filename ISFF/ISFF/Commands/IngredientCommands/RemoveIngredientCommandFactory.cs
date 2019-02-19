using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class RemoveIngredientCommandFactory : CommonRelayCommandFactory
    {
        public override Action<object> Execute()
        {
            return param =>
            {
                KitParametrsIngredients kitParametrs = param as KitParametrsIngredients;
                kitParametrs.db.Ingredients.Remove(kitParametrs.SelectedIngredient);
                kitParametrs.Ingredients.Remove(kitParametrs.SelectedIngredient);
                kitParametrs.db.SaveChanges();
            };
        }
        public override Func<object, bool> CanExecute()
        {
            return param =>
            {
                bool enable = true;
                if (param is KitParametrsIngredients kitParametrs && kitParametrs.SelectedIngredient != null)
                {
                    if (kitParametrs.IsBusy)
                        enable = false;
                }
                else
                    enable = false;
                return enable;
            };
        }
    }
}
