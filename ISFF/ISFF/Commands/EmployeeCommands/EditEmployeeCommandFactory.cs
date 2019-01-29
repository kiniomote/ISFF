﻿using System;
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
                kitParametrs.EditEmployeeExtendedCommand.State = ExtendedRelayCommand.STATE_ACCEPT;
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
                if (dialogWindow.ShowDialog() == true)
                {
                    kitParametrs.IsReadOnly = true;
                    kitParametrs.EditEmployeeExtendedCommand.TextCommand = TEXT_COMMAND;
                    kitParametrs.EditEmployeeExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                    kitParametrs.IsEnableCollection = true;
                    kitParametrs.IsBusy = false;
                }
                else
                {
                    if(dialogWindow.Answer == DialogViewModel.ANSWER_NO)
                    {
                        kitParametrs.IsReadOnly = true;
                        kitParametrs.EditEmployeeExtendedCommand.TextCommand = TEXT_COMMAND;
                        kitParametrs.EditEmployeeExtendedCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                        kitParametrs.IsEnableCollection = true;
                        kitParametrs.IsBusy = false;
                    }
                }
            };
        }
        public override Func<object, bool> CanExecute()
        {
            return param =>
            {
                bool enable = true;
                if (param is KitParametrsEmployees kitParametrs && kitParametrs.SelectedEmployee != null)
                {
                    if (kitParametrs.IsBusy && kitParametrs.EditEmployeeExtendedCommand.State != ExtendedRelayCommand.STATE_ACCEPT)
                        enable = false;
                }
                else
                    enable = false;
                return enable;
            };
        }
    }
}