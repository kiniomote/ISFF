using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class DoseViewModel
    {
        //_______________________________
        


        //_______________________________

        #region Construct

        public DoseViewModel(List<INameable> items, INameable selected, int count)
        {
            KitParametersDose = new KitParametrsDose(items, selected, count);
        }

        #endregion

        //_______________________________

        #region Data

        public KitParametrsDose KitParametersDose { get; set; }

        #endregion

        //_______________________________
    }
}
