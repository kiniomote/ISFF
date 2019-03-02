using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class AddProductCommandFactory : ExtendedRelayCommandFactory
    {
        public const string TEXT_COMMAND = "Добавить";
        public const string ALTERNATIVE_TEXT_COMMAND = "Подтвердить";
        public const string TEXT_DIALOG_WINDOW = "Сохранить новый товар?";

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
                kitParametrs.AddProductExtendedCommand.TextCommand = ALTERNATIVE_TEXT_COMMAND;
                kitParametrs.AddProductExtendedCommand.State = ExtendedRelayCommand.STATE_ACCEPT;
                kitParametrs.Products.Insert(0, new Product());
                kitParametrs.SelectedProduct = kitParametrs.Products.First();
                kitParametrs.IsEnableCollection = false;
                kitParametrs.IsBusy = true;
            };
        }
        public override Action<object> AlternativeExecute()
        {
            return param =>
            {
                KitParametrsProducts kitParametrs = param as KitParametrsProducts;
                DialogWindow dialogWindow = new DialogWindow(TEXT_DIALOG_WINDOW);
                if(dialogWindow.ShowDialog() == true)
                {
                    kitParametrs.IsReadOnly = true;
                    kitParametrs.IsEnableCollection = true;
                    kitParametrs.IsBusy = false;
                    kitParametrs.AddProductExtendedCommand.TextCommand = TEXT_COMMAND;
                    kitParametrs.AddProductExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                    kitParametrs.SelectedProduct.DoseIngredients = DeepCopyCollection<DoseIngredient>.CopyToList(kitParametrs.DoseIngredients);
                    kitParametrs.db.Products.Add(kitParametrs.SelectedProduct);
                    kitParametrs.db.SaveChanges();
                }
                else
                {
                    if (dialogWindow.Answer == DialogViewModel.ANSWER_NO)
                    {
                        kitParametrs.IsReadOnly = true;
                        kitParametrs.IsEnableCollection = true;
                        kitParametrs.IsBusy = false;
                        kitParametrs.AddProductExtendedCommand.TextCommand = TEXT_COMMAND;
                        kitParametrs.AddProductExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                        kitParametrs.DoseIngredients.Clear();
                        kitParametrs.Products.Remove(kitParametrs.SelectedProduct);
                    }
                }
            };
        }
        public override Func<object, bool> CanExecute()
        {
            return param =>
            {
                bool enable = true;
                if (param is KitParametrsProducts kitParametrs)
                {
                    if (kitParametrs.IsBusy && kitParametrs.AddProductExtendedCommand.State != ExtendedRelayCommand.STATE_ACCEPT)
                        enable = false;
                }

                return enable;
            };
        }
    }
}
