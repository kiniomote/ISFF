using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public interface ILogerService<T>
    {
        List<T> ReadFromFile();
        void WriteToFile(T element);
    }
}
