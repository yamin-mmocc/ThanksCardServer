using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThanksCardServer.Model
{
    public class Cards
    {
        public string Card_Type { get; set; }
        public string Card_Style { get; set; }

        [Key]
        public long Card_ID { get; set; }

        public ICollection<LogSends> LogSends { get; set; }
    }
}
