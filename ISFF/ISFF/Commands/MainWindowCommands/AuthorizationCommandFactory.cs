using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class AuthorizationCommandFactory : ExtendedRelayCommandFactory
    {
        public const string TEXT_COMMAND = "Вход";
        public const string ALTERNATIVE_TEXT_COMMAND = "Выход";
        public const string TEXT_DIALOG_WINDOW = "Вы точно хотите выйти?";
        public const string TEXT_WRONG_LOGIN = "Неправильный логин или пароль";

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
                KitParametrsMainWindow kitParametrs = param as KitParametrsMainWindow;
                CheckUserAccessService.Authorization(kitParametrs.Login, kitParametrs.Password);
                if(CheckUserAccessService.User == CheckUserAccessService.DefaultUser)
                {
                    DialogWindowService.OpenDialogWindow(TEXT_WRONG_LOGIN);
                }
                else
                {
                    kitParametrs.AuthorizationCommand.TextCommand = ALTERNATIVE_TEXT_COMMAND;
                    kitParametrs.AuthorizationCommand.State = ExtendedRelayCommand.STATE_ALTERNATIVE;
                    kitParametrs.IsEnable = false;
                    kitParametrs.Password = string.Empty;
                }
            };
        }

        public override Action<object> AlternativeExecute()
        {
            return param =>
            {
                if (DialogWindowService.OpenDialogWindowYesNo(TEXT_DIALOG_WINDOW) == false)
                    return;
                KitParametrsMainWindow kitParametrs = param as KitParametrsMainWindow;
                kitParametrs.Login = string.Empty;
                kitParametrs.AuthorizationCommand.TextCommand = TEXT_COMMAND;
                kitParametrs.AuthorizationCommand.State = ExtendedRelayCommand.STATE_NORMAL;
                kitParametrs.IsEnable = true;
                CheckUserAccessService.AuthorizationDefault();
            };
        }

        public override Func<object, bool> CanExecute()
        {
            return null;
        }
    }
}

