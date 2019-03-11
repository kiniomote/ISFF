using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class RemoveImageCommandFactory : CommonRelayCommandFactory
    {
        public override Action<object> Execute()
        {
            return param =>
            {
                KitParametrsProducts kitParametrs = param as KitParametrsProducts;
                kitParametrs.Image = ConverterByteImage.ToBitmapImage(Properties.Resources.None_image);
            };
        }

        public override Func<object, bool> CanExecute()
        {
            return param =>
            {
                if (CheckUserAccessService.IsNotAdministrator())
                    return false;
                bool enable = true;
                if (param is KitParametrsProducts kitParametrs)
                {
                    if (!kitParametrs.IsBusy)
                        enable = false;
                }
                else
                    enable = false;
                return enable;
            };
        }
    }
}
