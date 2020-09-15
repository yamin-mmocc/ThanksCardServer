using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThanksCardServer.Model
{
    public class LogSendInfo
    {
        public long? Sender_ID { get; set; }
        public long? Receiver_ID { get; set; }      
        public string Sender_DeptName { get; set; }
        public string Receiver_DeptName { get; set; }
        public long? Sender_DeptID { get; set; }
        public long? Receiver_DeptID { get; set; }
        public string Sender_Name { get; set; }
        public string Receiver_Name { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int count { get; set; }
        public int FromMonth { get; set; }
        public int ToMonth { get; set; }
        public int Year { get; set; }
    }
}
