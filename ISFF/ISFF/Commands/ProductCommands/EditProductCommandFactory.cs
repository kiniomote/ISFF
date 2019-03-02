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
                kitParametrs.EditProductExtendedCommand.State = ExtendedRelayCommand.STATE_ACCEPT;
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
                DialogWindow dialogWindow = new DialogWindow(TEXT_DIALOG_WINDOW);
                if (dialogWindow.ShowDialog() == true)
                {
                    kitParametrs.IsReadOnly = true;
                    kitParametrs.EditProductExtendedCommand.TextCommand = TEXT_COMMAND;
                    kitParametrs.EditProductExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                    kitParametrs.IsEnableCollection = true;
                    kitParametrs.IsBusy = false;
                    kitParametrs.SelectedProduct.DoseIngredients = DeepCopyCollection<DoseIngredient>.CopyToList(kitParametrs.DoseIngredients);
                    Product editProduct = kitParametrs.db.Products.SingleOrDefault(c => c.Id == kitParametrs.SelectedProduct.Id);
                    editProduct = kitParametrs.SelectedProduct;
                    kitParametrs.db.SaveChanges();
                }
                else
                {
                    if(dialogWindow.Answer == DialogViewModel.ANSWER_NO)
                    {
                        kitParametrs.IsReadOnly = true;
                        kitParametrs.EditProductExtendedCommand.TextCommand = TEXT_COMMAND;
                        kitParametrs.EditProductExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                        kitParametrs.IsEnableCollection = true;
                        kitParametrs.IsBusy = false;
                        Product.CopyProperties(kitParametrs.SelectedProduct, kitParametrs.ReservedCopySelectedProduct);
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
                bool enable = true;
                if (param is KitParametrsProducts kitParametrs && kitParametrs.SelectedProduct != null)
                {
                    if (kitParametrs.IsBusy && kitParametrs.EditProductExtendedCommand.State != ExtendedRelayCommand.STATE_ACCEPT)
                        enable = false;
                }
                else
                    enable = false;
                return enable;
            };
        }
    }
}
