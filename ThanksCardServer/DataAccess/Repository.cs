using Microsoft.EntityFrameworkCore;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        //public async Task<List<UserDepartmentRole>> GetUsers() //YME deleted
        //{
        //    if (context != null)
        //    {
        //        //return await context.Departments.ToListAsync();
        //        return await (from u in context.Users
        //                      join d in context.Departments
        //                      on u.Department_ID equals d.Department_ID
        //                      join r in context.Roles
        //                      on u.Role_ID equals r.Role_ID
        //                      where u.IsActive == true                              
        //                      select new UserDepartmentRole
        //                      {
        //                          User_ID = u.User_ID,
        //                          User_Name = u.User_Name,
        //                          Password = u.Password,
        //                          IsActive = u.IsActive,
        //                          timeStamp = u.timeStamp,
        //                          Department_ID = d.Department_ID,                                 
        //                          Department_Name = d.Department_Name,
        //                          Role_ID= r.Role_ID,
        //                          Role_Type = r.Role_Type
        //                      }).ToListAsync();
        //    }

        //    return null;
        //}

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
                                  Role_Type = r.Role_Type
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
                                      Role_Type = r.Role_Type
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
                                      Role_Type = r.Role_Type
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
                                      Role_Type = r.Role_Type
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
                    result = "Successfully Saved";
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
                    result = "Successfully Saved";
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

        //yamin comment start
        //public Users Authenticate(string username, string password)
        //{
        //    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        //        return null;

        //    var user = context.Users.SingleOrDefault(x => x.User_Name == username);

        //    // check if username exists
        //    if (user == null)
        //        return null;

        //    // check if password is correct
        //    var pw = context.Users.SingleOrDefault(x => x.Password == password);
        //    if (pw == null)
        //        return null;

        //    // authentication successful
        //    return user;
        //}
        //yamin comment end

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
        public string ChangePassword(Users user, string currentPwd, string newPwd)
        {
            string result = "";
            if (string.IsNullOrWhiteSpace(currentPwd) || string.IsNullOrWhiteSpace(newPwd))
                result = "Password is required";

            var u = context.Users.SingleOrDefault(x => x.User_Name == user.User_Name);

            // check if username exists
            if (u == null)
                result = "User Name does not exit";
            else
            {
                // check if password is correct
                if (!VerifyPasswordHash(currentPwd, user.PasswordHash, user.PasswordSalt))
                    result = "Current Password is Wrong";
                else
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(newPwd, out passwordHash, out passwordSalt);
                    Users users = context.Users
                                       .First(i => i.User_Name == user.User_Name);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    context.SaveChanges();
                    result = "Password Changed Successfully";
                }                
            }
            return result;
        }

        //YME add
        public async Task<List<UserDepartmentRole>> getUserInfoByName(string username)
        {
            if (context != null)
            {
                //return await context.Cards.ToListAsync();

                return await (from u in context.Users
                              join d in context.Departments
                              on u.Department_ID equals d.Department_ID
                              join r in context.Roles
                              on u.Role_ID equals r.Role_ID
                              where u.IsActive == true &&
                                   u.User_Name == username
                              select new UserDepartmentRole
                              {
                                  User_ID = u.User_ID,
                                  User_Name = u.User_Name,
                                  Department_ID = u.Department_ID,
                                  Department_Name = d.Department_Name,
                                  Role_ID = u.Role_ID,
                                  Role_Type = r.Role_Type
                              }).ToListAsync();
            }

            return null;
        }

        //YME add
        public async Task<List<Users>> getUserByDept(string deptname)
        {
            if (context != null)
            {
                //return await context.Cards.ToListAsync();

                return await (from u in context.Users
                              join d in context.Departments
                              on u.Department_ID equals d.Department_ID
                              where u.IsActive == true &&
                              d.Department_Name == deptname
                              select new Users
                              {
                                  User_ID = u.User_ID,
                                  User_Name = u.User_Name
                              }).ToListAsync();
            }

            return null;
        }

        //ATK add
        public  string getCardTotal()
        {
            if (context != null)
            {
                //return await context.Cards.ToListAsync();

                //return await (from u in context.Users
                //              join ls in context.LogSends
                //              on u.User_ID equals ls.Receiver_ID
                //              join d in context.Departments
                //              on u.Department_ID equals d.Department_ID
                //              where u.IsActive == true &&
                //              ls.timeStamp == DateTime.Now
                //              group ls by ls.Receiver_ID into newGroup
                //              //orderby newGroup.Key
                //              //  select newGroup;

                //            select new UserLogSends
                //              { 
                //                  count = context.LogSends.Count(ls.Card_ID),

                //                  User_ID = u.User_ID,
                //                  User_Name = u.User_Name
                //              }).ToListAsync();

                var count = ((from u in context.Users
                              join ls in context.LogSends
                              on u.User_ID equals ls.Receiver_ID
                              join d in context.Departments
                              on u.Department_ID equals d.Department_ID
                              where u.IsActive == true &&
                              ls.timeStamp == DateTime.Now
                              group ls by ls.Receiver_ID into newGroup
                              select new 
                              {
                                  User_ID = newGroup.Key,
                                  //User_Name = newGroup
                                  CardTotal = newGroup.Count()
                              })).Count().ToString();
                return count;
            }

            return null;
        }

    }  
}
