using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;

namespace ISFF
{
    public class JsonLogerService<T> : ILogerService<T>
    {
        public List<T> ReadFromFile(string file_name)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));
            if (!File.Exists(file_name))
                File.WriteAllText(file_name, "null");
            using (FileStream fs = new FileStream(file_name, FileMode.OpenOrCreate))
            {
                List<T> jsonElements = (List<T>)jsonFormatter.ReadObject(fs);
                return jsonElements;
            }
        }

        public void WriteToFile(List<T> elements, string file_name)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));
            using (FileStream fs = new FileStream(file_name, FileMode.Append))
            {
                if (elements.Count != 0)
                    jsonFormatter.WriteObject(fs, elements);
            }
        }
    }
}
