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
                return await context.Departments.ToListAsync();
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

        public string CreateUsers(Users user)
        {
            string result;

            // department check if the new department name is already taken
            if (context.Users.Any(x => x.User_Name == user.User_Name))
                result = "User Name : " + user.User_Name + " is already taken";

            else
            {
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
                return await context.Cards.ToListAsync();
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

        public object CreateRoles(Roles role)
        {
            throw new NotImplementedException();
        }

        public Task GetRoles()
        {
            throw new NotImplementedException();
        }
    }
}
