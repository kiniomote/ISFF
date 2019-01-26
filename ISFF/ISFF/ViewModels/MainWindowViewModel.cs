using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace ISFF
{
    public class MainWindowViewModel
    {
        //_______________________________

        #region Conctruct

        public MainWindowViewModel()
        {
            KitParametrsMainWindow = new KitParametrsMainWindow();
        }

        #endregion

        //_______________________________

        #region DataClass

        public KitParametrsMainWindow KitParametrsMainWindow { get; set; }

        #endregion

        //_______________________________



        //_______________________________
    }
}
