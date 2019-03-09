using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class EditProductCommandFactory : ExtendedRelayCommandFactory
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
                KitParametrsProducts kitParametrs = param as KitParametrsProducts;
                kitParametrs.IsReadOnly = false;
                kitParametrs.EditProductExtendedCommand.TextCommand = ALTERNATIVE_TEXT_COMMAND;
                kitParametrs.EditProductExtendedCommand.State = ExtendedRelayCommand.STATE_ALTERNATIVE;
                kitParametrs.IsEnableCollection = false;
                kitParametrs.IsBusy = true;
                kitParametrs.ReservedCopySelectedProduct = Product.Copy(kitParametrs.SelectedProduct);
            };
        }
        public override Action<object> AlternativeExecute()
        {
            return param =>
            {
                KitParametrsProducts kitParametrs = param as KitParametrsProducts;
                int answer = DialogWindowService.OpenResponseDialogWindow(TEXT_DIALOG_WINDOW);
                if (answer == DialogViewModel.ANSWER_YES)
                {
                    if (!kitParametrs.SelectedProduct.CorrectData.IsCorrect())
                    {
                        DialogWindowService.OpenDialogWindow(DialogWindowService.MESSAGE_INCORRECT_DATA);
                        return;
                    }
                    kitParametrs.IsReadOnly = true;
                    kitParametrs.EditProductExtendedCommand.TextCommand = TEXT_COMMAND;
                    kitParametrs.EditProductExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                    kitParametrs.IsEnableCollection = true;
                    kitParametrs.IsBusy = false;
                    foreach(int id in kitParametrs.IdElementsForRemove)
                    {
                        kitParametrs.db.DoseIngredients.Remove(kitParametrs.db.DoseIngredients.Find(id));
                    }
                    kitParametrs.IdElementsForRemove.Clear();
                    kitParametrs.SelectedProduct.DoseIngredients = DeepCopyCollection<DoseIngredient>.CopyToList(kitParametrs.DoseIngredients);
                    kitParametrs.db.SaveChanges();
                }
                else
                {
                    if(answer == DialogViewModel.ANSWER_NO)
                    {
                        kitParametrs.IsReadOnly = true;
                        kitParametrs.EditProductExtendedCommand.TextCommand = TEXT_COMMAND;
                        kitParametrs.EditProductExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                        kitParametrs.IsEnableCollection = true;
                        kitParametrs.IsBusy = false;
                        Product.CopyProperties(kitParametrs.SelectedProduct, kitParametrs.ReservedCopySelectedProduct);
                        kitParametrs.IdElementsForRemove.Clear();
                        kitParametrs.SelectedProduct = Product.Copy(kitParametrs.ReservedCopySelectedProduct);
                        DeepCopyCollection<DoseIngredient>.CopyElementsFromCollection(kitParametrs.DoseIngredients, kitParametrs.SelectedProduct.DoseIngredients);
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
                if (param is KitParametrsProducts kitParametrs && kitParametrs.SelectedProduct != null)
                {
                    if (kitParametrs.IsBusy && kitParametrs.EditProductExtendedCommand.State != ExtendedRelayCommand.STATE_ALTERNATIVE)
                        enable = false;
                }
                else
                    enable = false;
                return enable;
            };
        }
    }
}
