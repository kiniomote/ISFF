using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class EditIngredientCommandFactory : ExtendedRelayCommandFactory
    {
        public const string TEXT_COMMAND = "Изменить";
        public const string ALTERNATIVE_TEXT_COMMAND = "Подтвердить";
        public const string TEXT_DIALOG_WINDOW = "Сохранить изменения?";

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
                kitParametrs.EditIngredientExtendedCommand.TextCommand = ALTERNATIVE_TEXT_COMMAND;
                kitParametrs.EditIngredientExtendedCommand.State = ExtendedRelayCommand.STATE_ACCEPT;
                kitParametrs.IsEnableCollection = false;
                kitParametrs.IsBusy = true;
                kitParametrs.ReservedCopySelectedIngredient = Ingredient.Copy(kitParametrs.SelectedIngredient);
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
                    kitParametrs.EditIngredientExtendedCommand.TextCommand = TEXT_COMMAND;
                    kitParametrs.EditIngredientExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                    kitParametrs.IsEnableCollection = true;
                    kitParametrs.IsBusy = false;
                    Ingredient editIngredient = kitParametrs.db.Ingredients.SingleOrDefault(c => c.Id == kitParametrs.SelectedIngredient.Id);
                    editIngredient = kitParametrs.SelectedIngredient;
                    kitParametrs.db.SaveChanges();
                }
                else
                {
                    if (dialogWindow.Answer == DialogViewModel.ANSWER_NO)
                    {
                        kitParametrs.IsReadOnly = true;
                        kitParametrs.EditIngredientExtendedCommand.TextCommand = TEXT_COMMAND;
                        kitParametrs.EditIngredientExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                        kitParametrs.IsEnableCollection = true;
                        kitParametrs.IsBusy = false;
                        Ingredient.CopyProperties(kitParametrs.SelectedIngredient, kitParametrs.ReservedCopySelectedIngredient);
                        kitParametrs.SelectedIngredient = Ingredient.Copy(kitParametrs.ReservedCopySelectedIngredient);
                    }
                }
            };
        }
        public override Func<object, bool> CanExecute()
        {
            return param =>
            {
                bool enable = true;
                if (param is KitParametrsIngredients kitParametrs && kitParametrs.SelectedIngredient != null)
                {
                    if (kitParametrs.IsBusy && kitParametrs.EditIngredientExtendedCommand.State != ExtendedRelayCommand.STATE_ACCEPT)
                        enable = false;
                }
                else
                    enable = false;
                return enable;
            };
        }
    }
}
