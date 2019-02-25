﻿using System;
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

                ChoseDoseWindow choseDoseWindow = new ChoseDoseWindow(items, 
                    kitParametrs.SelectedDoseIngredient.Ingredient, 
                    kitParametrs.SelectedDoseIngredient.CountIngredient);
                if (choseDoseWindow.ShowDialog() == false)
                    return;
            };
        }
        public override Func<object, bool> CanExecute()
        {
            return param =>
            {
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
