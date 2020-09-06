using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThanksCardServer.Model
{
    public class Users
    {
        public string User_Name { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime timeStamp { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [Key]
        public long? User_ID { get; set; }

        public long? Role_ID { get; set; }
        [ForeignKey("Role_ID")]
        public virtual Roles Roles { get; set; }

        public long? Department_ID { get; set; }
        [ForeignKey("Department_ID")]
        public Departments Departments { get; set; }
    }
}
