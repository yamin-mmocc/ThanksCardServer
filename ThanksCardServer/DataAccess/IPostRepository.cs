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

        Task<List<Users>> GetUsers();
        string CreateUsers(Users user);

        Task<List<Cards>> GetCards();
        string CreateCards(Cards card);
        object CreateRoles(Roles role);
        Task GetRoles();
    }
}
