using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class IngredientsViewModel
    {
        //_______________________________

        #region Conctruct

        public IngredientsViewModel()
        {
            KitParametrsIngredients = new KitParametrsIngredients();
        }

        #endregion

        //_______________________________

        #region DataClass

        public KitParametrsIngredients KitParametrsIngredients { get; set; }

        #endregion

        //_______________________________



        //_______________________________
    }
}
