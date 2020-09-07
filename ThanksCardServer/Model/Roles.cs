using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThanksCardServer.Model
{
    public class Roles //YME created
    {
        public string Role_Type { get; set; }
        public bool IsActive { get; set; }
        public DateTime timeStamp { get; set; }

        [Key]
        public long? Role_ID { get; set; }        

        public ICollection<Users> Users { get; set; }
    }
}
