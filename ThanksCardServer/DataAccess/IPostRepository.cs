using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThanksCardServer.Model;

namespace ThanksCardServer.DataAccess
{
    public interface IPostRepository
    {
        Task<List<Departments>> GetDepartments();
        string CreateDepartments(Departments dept);
        string DeleteDepartment(long DeptID);


        Task<List<Users>> GetUsers();
        string CreateUsers(Users user, string password);

        Task<List<Cards>> GetCards();
        string CreateCards(Cards card);

        Task<List<Roles>> GetRoles();
        string CreateRoles(Roles role);
       
        Users Authenticate(string username, string password);
    }
}
