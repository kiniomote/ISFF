using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class CorrectDataService : ICorrect
    {
        public Dictionary<string, bool> CorrectProperties { get; set; }

        public bool IsCorrect()
        {
            return !CorrectProperties.Values.Contains(false);
        }

        public void CheckCorrect(string properties, string error)
        {
            if (error == string.Empty)
                CorrectProperties[properties] = true;
            else
                CorrectProperties[properties] = false;
        }

        public CorrectDataService(Dictionary<string, bool> properties)
        {
            CorrectProperties = properties;
        }
    }
}
