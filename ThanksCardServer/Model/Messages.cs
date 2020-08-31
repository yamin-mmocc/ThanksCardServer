using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThanksCardServer.Model
{
    public class Messages
    {
        public string Message_Text { get; set; }

        [Key]
        public long Message_ID { get; set; }
        
        public ICollection<LogSends> LogSends { get; set; }
    }
}
