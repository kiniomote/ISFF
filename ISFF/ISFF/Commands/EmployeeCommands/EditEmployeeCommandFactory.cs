using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class EditEmployeeCommandFactory : ExtendedRelayCommandFactory
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
                KitParametrsEmployees kitParametrs = param as KitParametrsEmployees;
                kitParametrs.IsReadOnly = false;
                kitParametrs.EditEmployeeExtendedCommand.TextCommand = ALTERNATIVE_TEXT_COMMAND;
                kitParametrs.EditEmployeeExtendedCommand.State = ExtendedRelayCommand.STATE_ALTERNATIVE;
                kitParametrs.IsEnableCollection = false;
                kitParametrs.IsBusy = true;
                kitParametrs.ReservedCopySelectedEmployee = Employee.Copy(kitParametrs.SelectedEmployee);
            };
        }
        public override Action<object> AlternativeExecute()
        {
            return param =>
            {
                KitParametrsEmployees kitParametrs = param as KitParametrsEmployees;
                int answer = DialogWindowService.OpenResponseDialogWindow(TEXT_DIALOG_WINDOW);
                if (answer == DialogViewModel.ANSWER_YES)
                {
                    if (!kitParametrs.SelectedEmployee.CorrectData.IsCorrect())
                    {
                        DialogWindowService.OpenDialogWindow(DialogWindowService.MESSAGE_INCORRECT_DATA);
                        return;
                    }
                    kitParametrs.IsReadOnly = true;
                    kitParametrs.EditEmployeeExtendedCommand.TextCommand = TEXT_COMMAND;
                    kitParametrs.EditEmployeeExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                    kitParametrs.IsEnableCollection = true;
                    kitParametrs.IsBusy = false;
                    Employee editEmployee = kitParametrs.db.Employees.SingleOrDefault(c => c.Id == kitParametrs.SelectedEmployee.Id);
                    editEmployee = kitParametrs.SelectedEmployee;
                    kitParametrs.db.SaveChanges();
                    Record record = new Record(kitParametrs.SelectedEmployee, Record.Action.Edit);
                    record.WriteRecordToFile(new JsonLogerService<Record>(JsonLogerService<Record>.FILE_NAME_EMPLOYEE));
                }
                else
                {
                    if(answer == DialogViewModel.ANSWER_NO)
                    {
                        kitParametrs.IsReadOnly = true;
                        kitParametrs.EditEmployeeExtendedCommand.TextCommand = TEXT_COMMAND;
                        kitParametrs.EditEmployeeExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                        kitParametrs.IsEnableCollection = true;
                        kitParametrs.IsBusy = false;
                        Employee.CopyProperties(kitParametrs.SelectedEmployee, kitParametrs.ReservedCopySelectedEmployee);
                        kitParametrs.SelectedEmployee = Employee.Copy(kitParametrs.ReservedCopySelectedEmployee);
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
                if (param is KitParametrsEmployees kitParametrs && kitParametrs.SelectedEmployee != null)
                {
                    if (kitParametrs.IsBusy && kitParametrs.EditEmployeeExtendedCommand.State != ExtendedRelayCommand.STATE_ALTERNATIVE)
                        enable = false;
                }
                else
                    enable = false;
                return enable;
            };
        }
    }
}
