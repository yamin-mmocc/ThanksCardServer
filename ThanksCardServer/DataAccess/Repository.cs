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
                result = "Department Name : " + dept.Department_Name + " is already taken";

            else
            {
                context.Departments.Add(dept);
                context.SaveChanges();
                result = "Success";
            }

            return result;
        }
    }
}
