using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThanksCardServer.Model
{
    public class UserDepartmentRole //YME created
    {
        public long? User_ID { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime timeStamp { get; set; }
        public long? Department_ID { get; set; }
        public string Department_Name { get; set; }
        public long? Role_ID { get; set; }
        public string Role_Type { get; set; }
        public bool IsAdmin { get; set; }
    }
}
