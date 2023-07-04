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
                kitParametrs.EditIngredientExtendedCommand.State = ExtendedRelayCommand.STATE_ALTERNATIVE;
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
                int answer = DialogWindowService.OpenResponseDialogWindow(TEXT_DIALOG_WINDOW);
                if (answer == DialogViewModel.ANSWER_YES)
                {
                    if (!kitParametrs.SelectedIngredient.CorrectData.IsCorrect())
                    {
                        DialogWindowService.OpenDialogWindow(DialogWindowService.MESSAGE_INCORRECT_DATA);
                        return;
                    }
                    kitParametrs.IsReadOnly = true;
                    kitParametrs.EditIngredientExtendedCommand.TextCommand = TEXT_COMMAND;
                    kitParametrs.EditIngredientExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                    kitParametrs.IsEnableCollection = true;
                    kitParametrs.IsBusy = false;
                    kitParametrs.db.Ingredients.Update(kitParametrs.SelectedIngredient);
                    kitParametrs.db.SaveChanges();
                    Record record = new Record(kitParametrs.SelectedIngredient, Record.Action.Edit);
                    record.WriteRecordToFile(new JsonLogerService<Record>(JsonLogerService<Record>.FILE_NAME_INGREDIENT));
                }
                else
                {
                    if (answer == DialogViewModel.ANSWER_NO)
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
                if (CheckUserAccessService.IsNotAdministrator())
                    return false;
                bool enable = true;
                if (param is KitParametrsIngredients kitParametrs && kitParametrs.SelectedIngredient != null)
                {
                    if (kitParametrs.IsBusy && kitParametrs.EditIngredientExtendedCommand.State != ExtendedRelayCommand.STATE_ALTERNATIVE)
                        enable = false;
                }
                else
                    enable = false;
                return enable;
            };
        }
    }
}
