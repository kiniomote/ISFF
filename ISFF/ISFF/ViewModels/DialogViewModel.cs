using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class DialogViewModel
    {
        //_______________________________

        #region Constants

        public const int ANSWER_YES = 0;
        public const int ANSWER_NO = 1;
        public const int ANSWER_CANCEL = 2;

        #endregion

        //_______________________________

        #region Construct

        public DialogViewModel(string text_message)
        {
            KitParametrs = new KitParametrsDialog(ANSWER_CANCEL, text_message);
            NoAnswerCommand = new CommonRelayCommand(new NoAnswerCommandFactory());
        }

        #endregion

        //_______________________________

        #region Data

        public KitParametrsDialog KitParametrs { get; set; }

        #endregion

        //_______________________________

        #region Commands

        public CommonRelayCommand NoAnswerCommand { get; set; }

        #endregion

        //_______________________________
    }
}
