using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class EmployeesViewModel 
    {
        //_______________________________

        #region Conctruct

        public EmployeesViewModel()
        {
            KitParametrsEmployees = new KitParametrsEmployees();
        }

        #endregion

        //_______________________________

        #region DataClass

        public KitParametrsEmployees KitParametrsEmployees { get; set; }

        #endregion

        //_______________________________

        

        //_______________________________
    }
}
