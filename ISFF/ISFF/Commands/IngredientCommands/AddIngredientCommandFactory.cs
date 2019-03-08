using System;
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
                kitParametrs.AddIngredientExtendedCommand.State = ExtendedRelayCommand.STATE_ALTERNATIVE;
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
                int answer = DialogWindowService.OpenResponseDialogWindow(TEXT_DIALOG_WINDOW);
                if (answer == DialogViewModel.ANSWER_YES)
                {
                    if (!kitParametrs.SelectedIngredient.CorrectData.IsCorrect())
                    {
                        DialogWindowService.OpenDialogWindow(DialogWindowService.MESSAGE_INCORRECT_DATA);
                        return;
                    }
                    kitParametrs.IsReadOnly = true;
                    kitParametrs.IsEnableCollection = true;
                    kitParametrs.IsBusy = false;
                    kitParametrs.AddIngredientExtendedCommand.TextCommand = TEXT_COMMAND;
                    kitParametrs.AddIngredientExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                    kitParametrs.db.Ingredients.Add(kitParametrs.SelectedIngredient);
                    kitParametrs.db.SaveChanges();
                }
                else
                {
                    if (answer == DialogViewModel.ANSWER_NO)
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
                if (CheckUserAccessService.IsNotAdministrator())
                    return false;
                bool enable = true;
                if (param is KitParametrsIngredients kitParametrs)
                {
                    if (kitParametrs.IsBusy && kitParametrs.AddIngredientExtendedCommand.State != ExtendedRelayCommand.STATE_ALTERNATIVE)
                        enable = false;
                }

                return enable;
            };
        }
    }
}
