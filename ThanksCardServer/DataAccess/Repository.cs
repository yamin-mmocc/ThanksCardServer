using Microsoft.EntityFrameworkCore;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThanksCardServer.Helper;
using ThanksCardServer.Model;

namespace ThanksCardServer.DataAccess
{
    public class Repository : IPostRepository
    {
        ApplicationContext context;
        public Repository(ApplicationContext _context)
        {
            context = _context;
        }
        public async Task<List<Departments>> GetDepartments()
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

        public string CreateDepartments(Departments dept)
        {
            string result;

            // department check if the new department name is already taken
            if (context.Departments.Any(x => x.Department_Name == dept.Department_Name))
                result = "Department Name : " + dept.Department_Name + " is already exit.";

            else
            {
                context.Departments.Add(dept);
                context.SaveChanges();
                result = "Success";
            }

            return result;
        }

        public async Task<List<Users>> GetUsers()
        {
            if (context != null)
            {
                return await context.Users.ToListAsync();
            }

            return null;
        }

        //yamin comment start
        //public string CreateUsers(Users user)
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

                context.Users.Add(user);
                context.SaveChanges();
                result = "Success";
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
                                  Card_Style = card.Card_Style,
                                  IsActive = card.IsActive,
                                  timeStamp = card.timeStamp
                              }).ToListAsync();
            }

            return null;
        }

        public string CreateCards(Cards card)
        {
            string result;

            // department check if the new department name is already taken
            if (context.Cards.Any(x => x.Card_Type == card.Card_Type))
                result = "Card Type : " + card.Card_Type + " is already exit.";

            else
            {
                context.Cards.Add(card);
                context.SaveChanges();
                result = "Success";
            }

            return result;
        }

        public string CreateRoles(Roles role)
        {
            string result;

            // department check if the new department name is already taken
            if (context.Roles.Any(x => x.Role_Type == role.Role_Type))
                result = "Role Type : " + role.Role_Type + " is already exit.";

            else
            {
                context.Roles.Add(role);
                context.SaveChanges();
                result = "Success";
            }

            return result;
        }

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
    }
}
