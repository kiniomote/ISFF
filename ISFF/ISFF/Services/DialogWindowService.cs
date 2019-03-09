using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public static class DialogWindowService
    {
        public const string MESSAGE_INCORRECT_DATA = "Не все поля заполнены правильно";

        public static int OpenResponseDialogWindow(string text)
        {
            ResponseDialogWindow responseDialog = new ResponseDialogWindow(text);
            responseDialog.ShowDialog();
            return responseDialog.Answer;
        }

        public static bool? OpenDialogWindowYesNo(string text)
        {
            CommonDialogWindow commonDialog = new CommonDialogWindow(text);
            return commonDialog.ShowDialog();
        }

        public static void OpenDialogWindow(string text)
        {
            MessageWindow messageWindow = new MessageWindow(text);
            messageWindow.ShowDialog();
        }

        public static KitParametrsDose OpenDoseDialogWindow(List<INameable> items, INameable selected = null, int count = 0)
        {
            KitParametrsDose kitDose = null;
            ChoseDoseWindow choseDoseWindow = new ChoseDoseWindow(items, selected, count);
            if (choseDoseWindow.ShowDialog() == false)
                return kitDose;
            DoseViewModel doseViewModel = choseDoseWindow.DataContext as DoseViewModel;
            kitDose = doseViewModel.KitParametersDose;
            return kitDose;
        }


    }
}
