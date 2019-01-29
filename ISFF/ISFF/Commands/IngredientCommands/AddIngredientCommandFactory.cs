﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class AddIngredientCommandFactory : ExtendedRelayCommandFactory
    {
        public const string TEXT_COMMAND = "Добавить";
        public const string ALTERNATIVE_TEXT_COMMAND = "Подтвердить";
        public const string TEXT_DIALOG_WINDOW = "Сохранить новый ингредиент?";

        public override string TextCommand()
        {
            return TEXT_COMMAND;
        }
        public override bool State()
        {
            return ExtendedRelayCommand.STATE_NORMAL;
        }
        public override Action<object> Execute()
        {
            return param =>
            {
                KitParametrsIngredients kitParametrs = param as KitParametrsIngredients;
                kitParametrs.IsReadOnly = false;
                kitParametrs.AddIngredientExtendedCommand.TextCommand = ALTERNATIVE_TEXT_COMMAND;
                kitParametrs.AddIngredientExtendedCommand.State = ExtendedRelayCommand.STATE_ACCEPT;
                kitParametrs.Ingredients.Insert(0, new Ingredient());
                kitParametrs.SelectedIngredient = kitParametrs.Ingredients.First();
                kitParametrs.IsEnableCollection = false;
                kitParametrs.IsBusy = true;
            };
        }
        public override Action<object> AlternativeExecute()
        {
            return param =>
            {
                KitParametrsIngredients kitParametrs = param as KitParametrsIngredients;
                DialogWindow dialogWindow = new DialogWindow(TEXT_DIALOG_WINDOW);
                if (dialogWindow.ShowDialog() == true)
                {
                    kitParametrs.IsReadOnly = true;
                    kitParametrs.IsEnableCollection = true;
                    kitParametrs.IsBusy = false;
                    kitParametrs.AddIngredientExtendedCommand.TextCommand = TEXT_COMMAND;
                    kitParametrs.AddIngredientExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                }
                else
                {
                    if (dialogWindow.Answer == DialogViewModel.ANSWER_NO)
                    {
                        kitParametrs.IsReadOnly = true;
                        kitParametrs.IsEnableCollection = true;
                        kitParametrs.IsBusy = false;
                        kitParametrs.AddIngredientExtendedCommand.TextCommand = TEXT_COMMAND;
                        kitParametrs.AddIngredientExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                        kitParametrs.Ingredients.Remove(kitParametrs.SelectedIngredient);
                    }
                }
            };
        }
        public override Func<object, bool> CanExecute()
        {
            return param =>
            {
                bool enable = true;
                if (param is KitParametrsIngredients kitParametrs)
                {
                    if (kitParametrs.IsBusy && kitParametrs.AddIngredientExtendedCommand.State != ExtendedRelayCommand.STATE_ACCEPT)
                        enable = false;
                }

                return enable;
            };
        }
    }
}
