using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThanksCardServer.Model
{
    public class Departments
    {        
        public string Department_Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime timeStamp { get; set; }

        [Key]
        public long? Department_ID { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}
