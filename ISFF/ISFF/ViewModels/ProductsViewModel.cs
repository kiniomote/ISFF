using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class ProductsViewModel
    {
        //_______________________________

        #region Conctruct

        public ProductsViewModel(IGenericRepository db)
        {
            KitParametrsProducts = new KitParametrsProducts(db);
        }

        #endregion

        //_______________________________

        #region DataClass

        public KitParametrsProducts KitParametrsProducts { get; set; }

        #endregion

        //_______________________________



        //_______________________________
    }
}
