using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ISFF
{
    [DataContract]
    public class Record
    {
        public enum Action
        {
            Add = 1,
            Edit = 2,
            Remove = 3,
            Ready = 4
        };

        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string ActionUser { get; set; }
        [DataMember]
        public string ObjectAction { get; set; }
        [DataMember]
        public DateTime TimeAction { get; set; }

        public Record(ILogable element, Action action)
        {
            ObjectAction = element.ToString();
            switch (action)
            {
                case Action.Add:
                    ActionUser = "добавил";
                    break;
                case Action.Edit:
                    ActionUser = "изменил";
                    break;
                case Action.Remove:
                    ActionUser = "удалил";
                    break;
                case Action.Ready:
                    ActionUser = "выполнил";
                    break;
            }
            TimeAction = DateTime.Now;
            UserName = CheckUserAccessService.User.Login;
        }

        public override string ToString()
        {
            return TimeAction.ToLongTimeString() + " - " + UserName + " " + ActionUser + " " + ObjectAction;
        }

        public string String
        {
            get { return ToString(); }
            set { }
        }

        public void WriteRecordToFile(ILogerService<Record> logerService)
        {
            logerService.WriteToFile(this);
        }
    }
}
