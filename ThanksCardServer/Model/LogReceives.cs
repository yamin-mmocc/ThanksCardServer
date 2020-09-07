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
        [Key]
        public long? ReceiveLog_ID { get; set; }

        public int? Status_Code { get; set; }
        [ForeignKey("Status_Code")]
        public virtual Status Status { get; set; }

        public long? SendLog_ID { get; set; }
        [ForeignKey("SendLog_ID")]
        public virtual LogSends LogSends { get; set; }        
    }
}
