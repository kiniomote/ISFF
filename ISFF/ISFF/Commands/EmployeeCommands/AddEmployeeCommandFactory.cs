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
                kitParametrs.AddEmployeeExtendedCommand.TextCommand = ALTERNATIVE_TEXT_COMMAND;
                kitParametrs.AddEmployeeExtendedCommand.State = ExtendedRelayCommand.STATE_ACCEPT;
                kitParametrs.Employees.Insert(0, new Employee());
                kitParametrs.SelectedEmployee = kitParametrs.Employees.First();
                kitParametrs.IsEnableCollection = false;
                kitParametrs.IsBusy = true;
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
                    kitParametrs.IsEnableCollection = true;
                    kitParametrs.IsBusy = false;
                    kitParametrs.AddEmployeeExtendedCommand.TextCommand = TEXT_COMMAND;
                    kitParametrs.AddEmployeeExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                }
                else
                {
                    if (dialogWindow.Answer == DialogViewModel.ANSWER_NO)
                    {
                        kitParametrs.IsReadOnly = true;
                        kitParametrs.IsEnableCollection = true;
                        kitParametrs.IsBusy = false;
                        kitParametrs.AddEmployeeExtendedCommand.TextCommand = TEXT_COMMAND;
                        kitParametrs.AddEmployeeExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                        kitParametrs.Employees.Remove(kitParametrs.SelectedEmployee);
                    }
                }
            };
        }
        public override Func<object, bool> CanExecute()
        {
            return param =>
            {
                bool enable = true;
                if (param is KitParametrsEmployees kitParametrs)
                {
                    if (kitParametrs.IsBusy && kitParametrs.AddEmployeeExtendedCommand.State != ExtendedRelayCommand.STATE_ACCEPT)
                        enable = false;
                }

                return enable;
            };
        }
    }
}
