using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThanksCardServer.Model
{
    public class Status  //YME created
    {
        public string Status_Name { get; set; }
        [Key]
        public int? Status_Code { get; set; } 
        public ICollection<LogSends> LogSends { get; set; }
        public ICollection<LogReceives> LogReceives { get; set; }
    }
}