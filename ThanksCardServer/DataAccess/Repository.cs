using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ThanksCardServer.Helper;
using ThanksCardServer.Model;

namespace ThanksCardServer.DataAccess
{
    public class Repository : IPostRepository //YME created
    {
        ApplicationContext context;
        public Repository(ApplicationContext _context)
        {
            context = _context;
        }
        public async Task<List<Departments>> GetDepartments() //YME add
        {
            if (context != null)
            {
                //return await context.Departments.ToListAsync();
                return await (from dept in context.Departments
                              where dept.IsActive == true
                              select new Departments
                              {
                                  Department_ID = dept.Department_ID,
                                  Department_Name = dept.Department_Name,
                                  IsActive = dept.IsActive,
                                  timeStamp = dept.timeStamp
                              }).ToListAsync();
            }

            return null;
        }

        public string CreateDepartments(Departments dept) //YME add
        {
            string result;

            // department check if the new department name is already taken
            if (context.Departments.Any(x => x.Department_Name == dept.Department_Name))
                result = "Department Name : " + dept.Department_Name + " is already exit.";

            else
            {
                try
                {
                    context.Departments.Add(dept);
                    context.SaveChanges();
                    result = "Successfully Saved";
                }catch(Exception ex)
                {
                    result = "Somethings Wrong";
                }
            }

            return result;
        }

        public async Task<List<UserDepartmentRole>> GetUsers(string name, string deptname) //YME add
        {
            if (context != null)
            {
                //return await context.Departments.ToListAsync();
                if (name != "" && deptname != "")
                {
                    return await (from u in context.Users
                                  join d in context.Departments
                                  on u.Department_ID equals d.Department_ID
                                  join r in context.Roles
                                  on u.Role_ID equals r.Role_ID
                                  where u.IsActive == true &&
                                  u.User_Name.ToLower().Contains(name.ToLower()) &&
                                  d.Department_Name == deptname
                                  select new UserDepartmentRole
                              {
                                  User_ID = u.User_ID,
                                  User_Name = u.User_Name,
                                  Password = u.Password,
                                  IsActive = u.IsActive,
                                  timeStamp = u.timeStamp,
                                  Department_ID = d.Department_ID,
                                  Department_Name = d.Department_Name,
                                  Role_ID = r.Role_ID,
                                  Role_Type = r.Role_Type,
                                  IsAdmin = u.IsAdmin
                              }).ToListAsync();
                }
                else if(name != "")
                {
                    return await (from u in context.Users
                                  join d in context.Departments
                                  on u.Department_ID equals d.Department_ID
                                  join r in context.Roles
                                  on u.Role_ID equals r.Role_ID
                                  where u.IsActive == true &&
                                  u.User_Name.ToLower().Contains(name.ToLower())                                   
                                  select new UserDepartmentRole
                                  {
                                      User_ID = u.User_ID,
                                      User_Name = u.User_Name,
                                      Password = u.Password,
                                      IsActive = u.IsActive,
                                      timeStamp = u.timeStamp,
                                      Department_ID = d.Department_ID,
                                      Department_Name = d.Department_Name,
                                      Role_ID = r.Role_ID,
                                      Role_Type = r.Role_Type,
                                      IsAdmin = u.IsAdmin
                                  }).ToListAsync();
                }
                else if(deptname != "")
                {
                    return await (from u in context.Users
                                  join d in context.Departments
                                  on u.Department_ID equals d.Department_ID
                                  join r in context.Roles
                                  on u.Role_ID equals r.Role_ID
                                  where u.IsActive == true &&
                                  d.Department_Name == deptname
                                  select new UserDepartmentRole
                                  {
                                      User_ID = u.User_ID,
                                      User_Name = u.User_Name,
                                      Password = u.Password,
                                      IsActive = u.IsActive,
                                      timeStamp = u.timeStamp,
                                      Department_ID = d.Department_ID,
                                      Department_Name = d.Department_Name,
                                      Role_ID = r.Role_ID,
                                      Role_Type = r.Role_Type,
                                      IsAdmin = u.IsAdmin
                                  }).ToListAsync();
                }
                else
                {
                    return await (from u in context.Users
                                  join d in context.Departments
                                  on u.Department_ID equals d.Department_ID
                                  join r in context.Roles
                                  on u.Role_ID equals r.Role_ID
                                  where u.IsActive == true 
                                  select new UserDepartmentRole
                                  {
                                      User_ID = u.User_ID,
                                      User_Name = u.User_Name,
                                      Password = u.Password,
                                      IsActive = u.IsActive,
                                      timeStamp = u.timeStamp,
                                      Department_ID = d.Department_ID,
                                      Department_Name = d.Department_Name,
                                      Role_ID = r.Role_ID,
                                      Role_Type = r.Role_Type,
                                      IsAdmin = u.IsAdmin
                                  }).ToListAsync();
                }
            }

            return null;
        }

        //yamin comment start
        //public string CreateUsers(Users user) //YME deleted
        //{
        //    string result;

        //    // department check if the new department name is already taken
        //    if (context.Users.Any(x => x.User_Name == user.User_Name))
        //        result = "User Name : " + user.User_Name + " is already taken";

        //    else
        //    {
        //        context.Users.Add(user);
        //        context.SaveChanges();
        //        result = "Success";
        //    }

        //    return result;
        //}
        //yamin comment end

        //YME add
        public string CreateUsers(Users user, string password) 
        {
            // validation
            string result;
            if (string.IsNullOrWhiteSpace(password))
                result = "Password is required";

            if (context.Users.Any(x => x.User_Name == user.User_Name))
                result = "User Name : " + user.User_Name + " is already taken";
            else
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                try
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    result = "Success";
                }
                catch(Exception ex)
                {
                    result = "Somethings Wrong";
                }
            }
            return result;
        }

        public async Task<List<Cards>> GetCards()
        {
            if (context != null)
            {
                //return await context.Cards.ToListAsync();

                return await (from card in context.Cards
                              where card.IsActive == true
                              select new Cards
                              {
                                  Card_ID = card.Card_ID,
                                  Card_Type = card.Card_Type,
                                  Card_Style = card.Card_Style,
                                  IsActive = card.IsActive,
                                  timeStamp = card.timeStamp
                              }).ToListAsync();
            }

            return null;
        }

        //YME add
        public string CreateCards(Cards card)
        {
            string result;

            // department check if the new department name is already taken
            if (context.Cards.Any(x => x.Card_Type == card.Card_Type))
                result = "Card Type : " + card.Card_Type + " is already exit.";

            else
            {
                try
                {
                    context.Cards.Add(card);
                    context.SaveChanges();
                    result = "Success";
                }catch(Exception ex)
                {
                    result = "Somethings Wrong";
                }
            }

            return result;
        }

        //YME add
        public string CreateRoles(Roles role)
        {
            string result;

            // department check if the new department name is already taken
            if (context.Roles.Any(x => x.Role_Type == role.Role_Type))
                result = "Role Type : " + role.Role_Type + " is already exit.";

            else
            {
                try
                {
                    context.Roles.Add(role);
                    context.SaveChanges();
                    result = "Successfully Saved";
                }catch(Exception ex)
                {
                    result = "Somethings Wrong";
                }
            }

            return result;
        }

        //YME add
        public async Task<List<Roles>> GetRoles()
        {
            if (context != null)
            {
                //return await context.Roles.ToListAsync();

                return await (from role in context.Roles
                              where role.IsActive == true
                              select new Roles
                              {
                                  Role_ID = role.Role_ID,
                                  Role_Type = role.Role_Type,
                                  IsActive = role.IsActive,
                                  timeStamp = role.timeStamp                                  
                              }).ToListAsync();
            }

            return null;
        }

        //YME add
        public Users Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = context.Users.SingleOrDefault(x => x.User_Name == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        //YME add
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        //YME add
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        //YME add
        public string DeleteDepartment(long? DeptID)
        {
            string result = "";

            if (context != null)
            {
                try
                {                   
                    Departments dept = context.Departments
                                       .First(i => i.Department_ID == DeptID);
                    dept.IsActive = false;
                    context.SaveChanges();
                    result = "Successfully Deleted";                    
                }catch(Exception ex)
                {
                    result = "Something Wrong";
                }
            }

            return result;
        }

        //YME add
        public string DeleteUser(long? User_ID)
        {
            string result = "";

            if (context != null)
            {
                try
                {
                    Users user = context.Users
                                       .First(i => i.User_ID == User_ID);
                    user.IsActive = false;
                    context.SaveChanges();
                    result = "Successfully Deleted";
                }
                catch (Exception ex)
                {
                    result = "Something Wrong";
                }
            }

            return result;
        }

        //YME add
        public string DeleteCard(long? cardID)
        {
            string result = "";

            if (context != null)
            {
                try
                {
                    Cards card = context.Cards
                                       .First(i => i.Card_ID == cardID);
                    card.IsActive = false;
                    context.SaveChanges();
                    result = "Successfully Deleted";
                }
                catch (Exception ex)
                {
                    result = "Something Wrong";
                }
            }

            return result;
        }

        //YME add
        public string DeleteRole(long? roleID)
        {
            string result = "";

            if (context != null)
            {
                try
                {
                    Roles role = context.Roles
                                       .First(i => i.Role_ID == roleID);
                    role.IsActive = false;
                    context.SaveChanges();
                    result = "Successfully Deleted";
                }
                catch (Exception ex)
                {
                    result = "Something Wrong";
                }
            }

            return result;
        }

        //YME add
        public string ChangePassword(Users user, string newPwd)
        {
            string result = "";
            //if (string.IsNullOrWhiteSpace(currentPwd) || string.IsNullOrWhiteSpace(newPwd))
            //    result = "Password is required";
            // check if username exists
            if (!context.Users.Any(x => x.User_Name == user.User_Name))           
                result = "User Name does not exit";
            else
            {
                // check if password is correct
                //if (!context.Users.Any(x => x.Password == currentPwd))
                //    result = "Current Password is Wrong";
                //else
                //{
                    //byte[] passwordHash, passwordSalt;
                    //CreatePasswordHash(newPwd, out passwordHash, out passwordSalt);
                    //Users users = context.Users
                    //                   .First(i => i.User_Name == user.User_Name);
                    //user.Password = newPwd;
                    //user.PasswordHash = passwordHash;
                    //user.PasswordSalt = passwordSalt;
                    //Users u = context.Users.Single(u => u.User_Name == user.User_Name);                    
                    //u.Password = newPwd;
                    //u.PasswordHash = passwordHash;
                    //u.PasswordSalt = passwordSalt;
                    // Query for a specific customer.
                    try
                    {
                        var u =
                            (from c in context.Users
                             where c.User_Name == user.User_Name
                             select c).First();
                    // Change the name of the contact.
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(newPwd, out passwordHash, out passwordSalt);
                    u.Password = newPwd;
                    u.PasswordHash = passwordHash;
                    u.PasswordSalt = passwordSalt;
                    context.SaveChanges();
                        result = "Success";
                    }
                    catch(Exception ex)
                    {
                        result = "Error";
                    }
                //}                
            }
            return result;
        }

        //YME add
        public async Task<List<UserDepartmentRole>> getUserInfoByName(string username)
        {
            if (context != null)
            {
                //return await context.Cards.ToListAsync();
                try
                {
                    return await (from u in context.Users
                                  join d in context.Departments
                                  on u.Department_ID equals d.Department_ID
                                  join r in context.Roles
                                  on u.Role_ID equals r.Role_ID
                                  where u.IsActive == true &&
                                  u.User_Name.ToLower() == username.ToLower()
                                  select new UserDepartmentRole
                                  {
                                      User_ID = u.User_ID,
                                      User_Name = u.User_Name,
                                      Password = u.Password,
                                      IsActive = u.IsActive,
                                      timeStamp = u.timeStamp,
                                      Department_ID = d.Department_ID,
                                      Department_Name = d.Department_Name,
                                      Role_ID = r.Role_ID,
                                      Role_Type = r.Role_Type,
                                      IsAdmin = u.IsAdmin
                                  }).ToListAsync();
                }
                catch(Exception ex)
                {

                }
            }

            return null;
        }

        //YME add        
        public async Task<List<Users>> getUserByDept(long? deptid,string username)
        {
            if (context != null)
            {
                return await (from u in context.Users
                              join d in context.Departments
                              on u.Department_ID equals d.Department_ID
                              where u.IsActive == true &&
                              d.Department_ID == deptid &&
                              !u.User_Name.ToLower().Contains(username.ToLower())
                              select new Users
                              {
                                  User_ID = u.User_ID,
                                  User_Name = u.User_Name
                              }).ToListAsync();
            }

            return null;
        }


        public async Task<List<LogSends>> SaveComposeToLogSends(LogSends ls)
        {
            //var logsend;
            try
            {
                context.LogSends.Add(ls);
                context.SaveChanges();
                //result = "Success";
                return await context.LogSends.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //YME add
        public string DeleteLogSend(LogSends ls)
        {
            string result = "";

            if (context != null)
            {
                try
                {
                    LogSends logsend = context.LogSends
                                       .First(i => i.SendLog_ID == ls.SendLog_ID);                   
                    context.SaveChanges();
                    result = "Successfully Deleted";
                }
                catch (Exception ex)
                {
                    result = "Something Wrong";
                }
            }

            return result;
        }

        public async Task<List<LogReceives>> SaveComposeToLogReceives(LogReceives lr)
        {
            try
            {
                    context.LogReceives.Add(lr);
                    context.SaveChanges();
                    //result = "Success";
                    return await context.LogReceives.ToListAsync();

            }
            catch (Exception ex)
            {
                return null;
            }                        
        }

        //Yamin Add
        public async Task<List<InboxModel>> GetInboxData(InboxModel inbox)
        {
            if (context != null)
            {
                return await (from lr in context.LogReceives
                              join u in context.Users
                              on lr.Receiver_ID equals u.User_ID
                              join c in context.Cards
                              on lr.Card_ID equals c.Card_ID
                              join d in context.Departments
                              on u.Department_ID equals d.Department_ID
                              where lr.Status_Code == 2 &&
                              lr.Sender_ID == inbox.User_ID
                              select new InboxModel
                              {
                                  Receiver_ID = lr.Receiver_ID,
                                  CreatedDateTime = lr.CreatedDateTime,
                                  Card_ID = lr.Card_ID,
                                  Card_Type = c.Card_Type,
                                  Card_Style = c.Card_Style,
                                  Department_Name = d.Department_Name,
                                  User_Name = u.User_Name,
                                  MessageText = lr.MessageText,
                                  Status_Code = lr.Status_Code,
                                  SendLog_ID = lr.SendLog_ID,
                                  replyMsg = lr.replyMsg
                              }).ToListAsync();

            }

            return null;
        }

        public async Task<List<SendModel>> GetSendData(SendModel send)
        {
            if (context != null)
            {
                return await (from ls in context.LogSends
                              join u in context.Users
                              on ls.Receiver_ID equals u.User_ID
                              join c in context.Cards
                              on ls.Card_ID equals c.Card_ID
                              join d in context.Departments
                              on u.Department_ID equals d.Department_ID
                              where ls.Status_Code == 1 &&
                              ls.Sender_ID == send.User_ID
                              select new SendModel
                              {
                                  Receiver_ID = ls.Receiver_ID,
                                  CreatedDateTime = ls.CreatedDateTime,
                                  Card_ID = ls.Card_ID,
                                  Card_Type = c.Card_Type,
                                  Card_Style = c.Card_Style,
                                  Department_Name = d.Department_Name,
                                  User_Name = u.User_Name,
                                  Status_Code = ls.Status_Code,
                                  MessageText = ls.MessageText,
                                  replyMsg = ls.replyMsg,
                                  SendLog_ID = ls.SendLog_ID
                              }).ToListAsync();
            }

            return null;
        }

        //yamin add
        public string SaveReplyMsgToLogSends(LogSends ls)
        {
            string result = "";
            try
            {
                var logsend =
                    (from logs in context.LogSends
                     where logs.SendLog_ID == ls.SendLog_ID
                     select logs).First();
                // update the reply message.
                logsend.replyMsg = ls.replyMsg;
                context.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return result;
        }

        //yamin add
        public string SaveReplyMsgToLogReceives(LogReceives lr)
        {
            string result = "";
            try
            {
                var logreceive =
                    (from logrev in context.LogReceives
                     where logrev.SendLog_ID == lr.SendLog_ID
                     select logrev).First();
                // Update the reply message.
                logreceive.replyMsg = lr.replyMsg;
                context.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return result;
        }

        //yamin add
        //public string GetCardTotal(LogSends logsend, string deptname)
        //{
        //    if(context != null)
        //    {
        //        var count = ((from u in context.Users
        //                      join ls in context.LogSends
        //                      on u.User_ID equals ls.Receiver_ID
        //                      join d in context.Departments
        //                      on u.Department_ID equals d.Department_ID
        //                      where u.IsActive == true &&
        //                      ls.CreatedDateTime.Month == DateTime.Now.Month
        //                      group ls by ls.Receiver_ID into newGroup
        //                      select new
        //                      {
        //                          User_ID = newGroup.Key,
        //                          CardTotal = newGroup.Count()
        //                      })).Count().ToString();
        //        return count;
        //    }
        //    return null;
        //}

        public DataTable GetCardTotal(int Frommonth, int Tomonth, int year, long? deptid)
        {
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            //dt2.Columns.Add("SenderID", typeof(long));
            //dt2.Columns.Add("SenderName", typeof(string));
            //dt2.Columns.Add("SenderDeptName", typeof(string));
            dt2.Columns.Add("ReceiverID", typeof(long));
            dt2.Columns.Add("ReceiverName", typeof(string));
            dt2.Columns.Add("ReceiverDeptName", typeof(string));
            dt2.Columns.Add("TotalCount", typeof(int));

            //dt3 column            
            dt3.Columns.Add("SenderDeptName", typeof(string));            
            dt3.Columns.Add("ReceiverDeptName", typeof(string));
            dt3.Columns.Add("TotalCount", typeof(int));
            var groupbyResult = new List<LogSendInfo>(); ;
            if (context != null)
            {
                if (Frommonth != 0 && Tomonth != 0 && year != 0)
                {
                    groupbyResult = ((from u in context.Users
                                      join ls in context.LogSends
                                      on u.User_ID equals ls.Receiver_ID
                                      join d in context.Departments
                                      on u.Department_ID equals d.Department_ID
                                      where u.IsActive == true &&
                                      ls.CreatedDateTime.Year == year &&
                                      ls.CreatedDateTime.Month >= Frommonth &&
                                      ls.CreatedDateTime.Month <= Tomonth
                                      select new LogSendInfo
                                      {
                                          Receiver_ID = ls.Receiver_ID,
                                          Sender_ID = ls.Sender_ID,
                                          CreatedDateTime = ls.CreatedDateTime,
                                          Sender_DeptID = ls.Sender_DeptID,
                                          Sender_DeptName = "",
                                          Receiver_DeptID = ls.Receiver_DeptID,
                                          Receiver_DeptName = "",
                                          Sender_Name = "",
                                          Receiver_Name = "",
                                          count = 0
                                      })).ToList();
                }
                else if (Frommonth != 0 && Tomonth != 0)
                {
                    groupbyResult = ((from u in context.Users
                                      join ls in context.LogSends
                                      on u.User_ID equals ls.Receiver_ID
                                      join d in context.Departments
                                      on u.Department_ID equals d.Department_ID
                                      where u.IsActive == true &&
                                      ls.CreatedDateTime.Month >= Frommonth &&
                                      ls.CreatedDateTime.Month <= Tomonth
                                      select new LogSendInfo
                                      {
                                          Receiver_ID = ls.Receiver_ID,
                                          Sender_ID = ls.Sender_ID,
                                          CreatedDateTime = ls.CreatedDateTime,
                                          Sender_DeptID = ls.Sender_DeptID,
                                          Sender_DeptName = "",
                                          Receiver_DeptID = ls.Receiver_DeptID,
                                          Receiver_DeptName = "",
                                          Sender_Name = "",
                                          Receiver_Name = "",
                                          count = 0
                                      })).ToList();
                }
                else if (year != 0)
                {
                    groupbyResult = ((from u in context.Users
                                      join ls in context.LogSends
                                      on u.User_ID equals ls.Receiver_ID
                                      join d in context.Departments
                                      on u.Department_ID equals d.Department_ID
                                      where u.IsActive == true &&
                                      ls.CreatedDateTime.Year == year
                                      select new LogSendInfo
                                      {
                                          Receiver_ID = ls.Receiver_ID,
                                          Sender_ID = ls.Sender_ID,
                                          CreatedDateTime = ls.CreatedDateTime,
                                          Sender_DeptID = ls.Sender_DeptID,
                                          Sender_DeptName = "",
                                          Receiver_DeptID = ls.Receiver_DeptID,
                                          Receiver_DeptName = "",
                                          Sender_Name = "",
                                          Receiver_Name = "",
                                          count = 0
                                      })).ToList();
                }
                else if(year != 0 && Frommonth != 0 && Tomonth != 0)
                {
                    groupbyResult = ((from u in context.Users
                                      join ls in context.LogSends
                                      on u.User_ID equals ls.Receiver_ID
                                      join d in context.Departments
                                      on u.Department_ID equals d.Department_ID
                                      where u.IsActive == true &&
                                      ls.CreatedDateTime.Month <= Frommonth &&
                                      ls.CreatedDateTime.Month >= Tomonth &&
                                      ls.CreatedDateTime.Year == year
                                      select new LogSendInfo
                                      {
                                          Receiver_ID = ls.Receiver_ID,
                                          Sender_ID = ls.Sender_ID,
                                          CreatedDateTime = ls.CreatedDateTime,
                                          Sender_DeptID = ls.Sender_DeptID,
                                          Sender_DeptName = "",
                                          Receiver_DeptID = ls.Receiver_DeptID,
                                          Receiver_DeptName = "",
                                          Sender_Name = "",
                                          Receiver_Name = "",
                                          count = 0
                                      })).ToList();
                }
                else if( Frommonth != 0 && Tomonth != 0)
                {
                    groupbyResult = ((from u in context.Users
                                      join ls in context.LogSends
                                      on u.User_ID equals ls.Receiver_ID
                                      join d in context.Departments
                                      on u.Department_ID equals d.Department_ID
                                      where u.IsActive == true &&
                                      ls.CreatedDateTime.Month <= Frommonth &&
                                      ls.CreatedDateTime.Month >= Tomonth 
                                      select new LogSendInfo
                                      {
                                          Receiver_ID = ls.Receiver_ID,
                                          Sender_ID = ls.Sender_ID,
                                          CreatedDateTime = ls.CreatedDateTime,
                                          Sender_DeptID = ls.Sender_DeptID,
                                          Sender_DeptName = "",
                                          Receiver_DeptID = ls.Receiver_DeptID,
                                          Receiver_DeptName = "",
                                          Sender_Name = "",
                                          Receiver_Name = "",
                                          count = 0
                                      })).ToList();
                }
                else if( year != 0)
                {
                    groupbyResult = ((from u in context.Users
                                      join ls in context.LogSends
                                      on u.User_ID equals ls.Receiver_ID
                                      join d in context.Departments
                                      on u.Department_ID equals d.Department_ID
                                      where u.IsActive == true &&
                                      ls.CreatedDateTime.Year == year
                                      select new LogSendInfo
                                      {
                                          Receiver_ID = ls.Receiver_ID,
                                          Sender_ID = ls.Sender_ID,
                                          CreatedDateTime = ls.CreatedDateTime,
                                          Sender_DeptID = ls.Sender_DeptID,
                                          Sender_DeptName = "",
                                          Receiver_DeptID = ls.Receiver_DeptID,
                                          Receiver_DeptName = "",
                                          Sender_Name = "",
                                          Receiver_Name = "",
                                          count = 0
                                      })).ToList();
                }
                //else if (deptid != 0)
                //{
                //    groupbyResult = ((from u in context.Users
                //                      join ls in context.LogSends
                //                      on u.User_ID equals ls.Receiver_ID
                //                      join d in context.Departments
                //                      on u.Department_ID equals d.Department_ID
                //                      where u.IsActive == true &&
                //                      ls.Sender_DeptID == deptid
                //                      select new LogSendInfo
                //                      {
                //                          Receiver_ID = ls.Receiver_ID,
                //                          Sender_ID = ls.Sender_ID,
                //                          CreatedDateTime = ls.CreatedDateTime,
                //                          Sender_DeptID = ls.Sender_DeptID,
                //                          Sender_DeptName = "",
                //                          Receiver_DeptID = ls.Receiver_DeptID,
                //                          Receiver_DeptName = "",
                //                          Sender_Name = "",
                //                          Receiver_Name = "",
                //                          count = 0
                //                      })).ToList();
                //}
                else
                {
                    groupbyResult = ((from ls in context.LogSends
                                      //from u in context.Users
                                      join u in context.Users
                                      on ls.Receiver_ID equals u.User_ID
                                      join d in context.Departments
                                      on u.Department_ID equals d.Department_ID
                                      where u.IsActive == true &&
                                      ls.CreatedDateTime.Month == DateTime.Now.Month
                                      select new LogSendInfo
                                      {
                                          Receiver_ID = ls.Receiver_ID,
                                          Sender_ID = ls.Sender_ID,
                                          CreatedDateTime = ls.CreatedDateTime,
                                          Sender_DeptID = ls.Sender_DeptID,
                                          Sender_DeptName = "",
                                          Receiver_DeptID = ls.Receiver_DeptID,
                                          Receiver_DeptName = "",
                                          Sender_Name = "",
                                          Receiver_Name = "",
                                          count = 0
                                      })).ToList();
                }
            }
            if (deptid == 0)
            {
                foreach (var item in groupbyResult)
                {
                    var rCount = (from p in context.LogSends
                                  where p.Receiver_ID == item.Receiver_ID 
                                  group p by
                                  new
                                  {
                                      d_Id = p.Receiver_ID
                                  } into s
                                  orderby s.Count() descending
                                  select new
                                  {
                                      dIDs = s.Key.d_Id,
                                      cnt = s.Count()
                                  }).FirstOrDefault();
                    var sendername = (from c in context.LogSends
                                      join u in context.Users
                                      on c.Sender_ID equals u.User_ID
                                      where c.Sender_ID == item.Sender_ID
                                      select
                                         u.User_Name
                                     ).FirstOrDefault();
                    var receivername = (from c in context.LogSends
                                        join u in context.Users
                                        on c.Receiver_ID equals u.User_ID
                                        where c.Receiver_ID == item.Receiver_ID
                                        select
                                            u.User_Name
                                      ).FirstOrDefault();
                    var senderdept = (from c in context.LogSends
                                      join d in context.Departments
                                      on c.Sender_DeptID equals d.Department_ID
                                      where c.Sender_DeptID == item.Sender_DeptID
                                      select
                                          d.Department_Name
                                      ).FirstOrDefault();
                    var receiverdept = (from c in context.LogSends
                                        join d in context.Departments
                                        on c.Receiver_DeptID equals d.Department_ID
                                        where c.Receiver_DeptID == item.Receiver_DeptID
                                        select
                                            d.Department_Name
                                      ).FirstOrDefault();
                    item.count = rCount == null ? 0 : rCount.cnt;
                    DataRow row = dt2.NewRow();
                    //row["SenderID"] = item.Sender_ID;
                    //row["SenderName"] = sendername;
                    //row["SenderDeptName"] = senderdept;
                    row["ReceiverID"] = item.Receiver_ID;
                    row["ReceiverName"] = receivername;
                    row["ReceiverDeptName"] = receiverdept;
                    row["TotalCount"] = item.count;

                    bool isDistinct = true;
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        // check if both rows are equal
                        if (Enumerable.SequenceEqual(dt2.Rows[i].ItemArray, row.ItemArray))
                        {
                            // it already exists
                            isDistinct = false;
                            break;
                        }
                        bool exists = dt2.AsEnumerable().Where(c => c.Field<long>("ReceiverID").Equals(item.Receiver_ID)).Count() > 0;
                        if (exists)
                        {
                            isDistinct = false;
                            break;
                        }
                    }

                    if (isDistinct)
                    {
                        dt2.Rows.Add(row);
                    }                    
                }
                if (dt2.Rows.Count != 0)
                {
                    return dt2;
                }
                else
                    return null;
            }
            else
            {
                foreach (var item in groupbyResult)
                {
                    var rCount = (from p in context.LogSends
                                  //join d in context.Departments
                                  //on p.Sender_DeptID equals d.Department_ID
                                  where p.Receiver_DeptID == item.Receiver_DeptID &&
                                  p.Sender_DeptID == item.Sender_DeptID
                                  group p by
                                  new
                                  {
                                      d_Id = p.Receiver_DeptID
                                  } into s
                                  orderby s.Count() descending
                                  select new
                                  {
                                      dIDs = s.Key.d_Id,
                                      cnt = s.Count()
                                  }).FirstOrDefault();
                    var senderdept = (from c in context.LogSends
                                      join d in context.Departments
                                      on c.Sender_DeptID equals d.Department_ID
                                      where c.Sender_DeptID == item.Sender_DeptID
                                      select
                                          d.Department_Name
                                      ).FirstOrDefault();
                    var receiverdept = (from c in context.LogSends
                                        join d in context.Departments
                                        on c.Receiver_DeptID equals d.Department_ID
                                        where c.Receiver_DeptID == item.Receiver_DeptID
                                        select
                                            d.Department_Name
                                      ).FirstOrDefault();
                    item.count = rCount == null ? 0 : rCount.cnt;
                    DataRow row = dt3.NewRow();                    
                    row["SenderDeptName"] = senderdept;                                  
                    row["ReceiverDeptName"] = receiverdept;
                    row["TotalCount"] = item.count;

                    bool isDistinct = true;
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        // check if both rows are equal
                        if (Enumerable.SequenceEqual(dt3.Rows[i].ItemArray, row.ItemArray))
                        {
                            // it already exists
                            isDistinct = false;
                            break;
                        }
                    }

                    if (isDistinct)
                    {
                        dt3.Rows.Add(row);
                    }
                }
                if (dt3.Rows.Count != 0)
                {
                    return dt3;
                }
                else
                    return null;
            }            
        }

        //yamin add
        public string DeleteReplyMsgFromLogSend(LogSends ls)
        {
            string result = "";
            try
            {
                var logsend =
                    (from logs in context.LogSends
                     where logs.SendLog_ID == ls.SendLog_ID
                     select logs).First();
                // update the reply message.
                logsend.replyMsg = ls.replyMsg;
                context.SaveChanges();
                result = "Success";
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return result;
        }

        public DataTable GetDetailData(long? Rec_ID)
        {
            DataTable dtdetail = new DataTable();
            dtdetail.Columns.Add("SenderID", typeof(long));
            dtdetail.Columns.Add("SenderName", typeof(string));
            dtdetail.Columns.Add("SenderDeptName", typeof(string));
            dtdetail.Columns.Add("ReceiverID", typeof(long));
            dtdetail.Columns.Add("ReceiverName", typeof(string));
            dtdetail.Columns.Add("ReceiverDeptName", typeof(string));

            var groupbyResult = new List<LogSendInfo>(); ;
            if (context != null)
            {

                groupbyResult = ((from ls in context.LogSends
                                      //from u in context.Users
                                  join u in context.Users
                                  on ls.Receiver_ID equals u.User_ID
                                  join d in context.Departments
                                  on u.Department_ID equals d.Department_ID
                                  where u.IsActive == true &&
                                  ls.CreatedDateTime.Month == DateTime.Now.Month &&
                                  ls.Receiver_ID == Rec_ID
                                  select new LogSendInfo
                                  {
                                      Receiver_ID = ls.Receiver_ID,
                                      Sender_ID = ls.Sender_ID,
                                      CreatedDateTime = ls.CreatedDateTime,
                                      Sender_DeptID = ls.Sender_DeptID,
                                      Sender_DeptName = "",
                                      Receiver_DeptID = ls.Receiver_DeptID,
                                      Receiver_DeptName = "",
                                      Sender_Name = "",
                                      Receiver_Name = ""
                                  })).ToList();
                foreach (var item in groupbyResult)
                {
                    var sendername = (from c in context.LogSends
                                      join u in context.Users
                                      on c.Sender_ID equals u.User_ID
                                      where c.Sender_ID == item.Sender_ID
                                      select
                                         u.User_Name
                                     ).FirstOrDefault();
                    var receivername = (from c in context.LogSends
                                        join u in context.Users
                                        on c.Receiver_ID equals u.User_ID
                                        where c.Receiver_ID == item.Receiver_ID
                                        select
                                            u.User_Name
                                      ).FirstOrDefault();
                    var senderdept = (from c in context.LogSends
                                      join d in context.Departments
                                      on c.Sender_DeptID equals d.Department_ID
                                      where c.Sender_DeptID == item.Sender_DeptID
                                      select
                                          d.Department_Name
                                      ).FirstOrDefault();
                    var receiverdept = (from c in context.LogSends
                                        join d in context.Departments
                                        on c.Receiver_DeptID equals d.Department_ID
                                        where c.Receiver_DeptID == item.Receiver_DeptID
                                        select
                                            d.Department_Name
                                      ).FirstOrDefault();
                    //item.count = rCount == null ? 0 : rCount.cnt;
                    DataRow row = dtdetail.NewRow();
                    row["SenderID"] = item.Sender_ID;
                    row["SenderName"] = sendername;
                    row["SenderDeptName"] = senderdept;
                    row["ReceiverID"] = item.Receiver_ID;
                    row["ReceiverName"] = receivername;
                    row["ReceiverDeptName"] = receiverdept;
                    //row["TotalCount"] = item.count;

                    bool isDistinct = true;
                    for (int i = 0; i < dtdetail.Rows.Count; i++)
                    {
                        // check if both rows are equal
                        if (Enumerable.SequenceEqual(dtdetail.Rows[i].ItemArray, row.ItemArray))
                        {
                            // it already exists
                            isDistinct = false;
                            break;
                        }
                        //bool exists = dtdetail.AsEnumerable().Where(c => c.Field<long>("ReceiverID").Equals(item.Receiver_ID)).Count() > 0;
                        //if (exists)
                        //{
                        //    isDistinct = false;
                        //    break;
                        //}
                    }

                    if (isDistinct)
                    {
                        dtdetail.Rows.Add(row);
                    }
                }
            }
            if (dtdetail.Rows.Count != 0)
            {
                return dtdetail;
            }
            else
                return null;
        }
            
    }  
}
