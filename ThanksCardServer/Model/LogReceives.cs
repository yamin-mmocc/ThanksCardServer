using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThanksCardServer.Model
{
    public class LogReceives //YME created
    {
        public DateTime CreatedDateTime { get; set; }
        public string MessageText { get; set; }
        public string replyMsg { get; set; }

        [Key]
        public long? ReceiveLog_ID { get; set; }

        public int? Status_Code { get; set; }
        [ForeignKey("Status_Code")]
        public virtual Status Status { get; set; }

        public long? Card_ID { get; set; }
        [ForeignKey("Card_ID")]
        public virtual Cards Cards { get; set; }

        public long? Sender_ID { get; set; }
        public virtual Users From { get; set; }
        public long? Receiver_ID { get; set; }
        public virtual Users To { get; set; }

        public long? SendLog_ID { get; set; }
        [ForeignKey("SendLog_ID")]
        public LogSends LogSends { get; set; }
    }
}
