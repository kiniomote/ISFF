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
        public const string FILE_NAME_EMPLOYEE = "EmployeeLog.json";
        public const string FILE_NAME_INGREDIENT = "IngredientLog.json";
        public const string FILE_NAME_PRODUCT = "ProductLog.json";
        public const string FILE_NAME_ORDER = "OrderLog.json";

        private string fileName;

        public JsonLogerService(string file_name)
        {
            fileName = file_name;
        }

        public List<T> ReadFromFile()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));
            if (!File.Exists(fileName))
                File.WriteAllText(fileName, "null");
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                List<T> jsonElements = ListRecords(jsonFormatter.ReadObject(fs));
                return jsonElements;
            }
        }

        public void WriteToFile(T element)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));
            List<T> listElements = ReadFromFile();
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                if (element != null)
                {
                    listElements.Add(element);
                    jsonFormatter.WriteObject(fs, listElements);
                }
            }
        }

        private List<T> ListRecords(object read_obj)
        {
            List<T> list = new List<T>();

            if (read_obj is T obj)
                list.Add(obj);
            else
                if(read_obj != null)
                    list = (List<T>)read_obj;

            return list;
        }
    }
}
