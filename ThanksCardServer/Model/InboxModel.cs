using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThanksCardServer.Model
{
    public class InboxModel
    {
        public DateTime CreatedDateTime { get; set; }
        public string replyMsg { get; set; }
        public long? ReceiveLog_ID { get; set; }
        public long? Card_ID { get; set; }
        public int? Status_Code { get; set; }
        public long? Sender_ID { get; set; }
        public long? Receiver_ID { get; set; }
        public long? User_ID { get; set; }
        public string User_Name { get; set; }
        public long? Department_ID { get; set; }
        public string Department_Name { get; set; }
        public string Card_Type { get; set; }
        public string Card_Style { get; set; }
        public string MessageText { get; set; }
        public long? SendLog_ID { get; set; }
    }
}