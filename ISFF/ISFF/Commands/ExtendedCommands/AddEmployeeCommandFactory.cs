using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class AddEmployeeCommandFactory : ExtendedRelayCommandFactory
    {
        public const string TEXT_COMMAND = "Добавить";
        public const string ALTERNATIVE_TEXT_COMMAND = "Подтвердить";
        public const string TEXT_DIALOG_WINDOW = "Сохранить нового сотрудника?";

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
                KitParametrsEmployees kitParametrs = param as KitParametrsEmployees;
                kitParametrs.IsReadOnly = false;
                kitParametrs.TextAddButton = ALTERNATIVE_TEXT_COMMAND;
            };
        }
        public override Action<object> AlternativeExecute()
        {
            return param =>
            {
                KitParametrsEmployees kitParametrs = param as KitParametrsEmployees;
                DialogWindow dialogWindow = new DialogWindow(TEXT_DIALOG_WINDOW);
                if(dialogWindow.ShowDialog() == true)
                {
                    kitParametrs.IsReadOnly = true;
                    kitParametrs.TextAddButton = TEXT_COMMAND;

                }
                else
                {
                    
                }
            };
        }
        public override Func<object, bool> CanExecute()
        {
            return null;
        }
    }
}
