using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public interface ICorrect
    {
        Dictionary<string, bool> CorrectProperties { get; set; }
        bool IsCorrect();
    }
}
