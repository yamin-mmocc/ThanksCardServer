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
        string DeleteDepartment(long? Department_ID);


        Task<List<UserDepartmentRole>> GetUsers(string name, string deptname);
        string CreateUsers(Users user, string password);
        string DeleteUser(long? User_ID);
        Task<List<UserDepartmentRole>> getUserInfoByName(string username);

        Task<List<Cards>> GetCards();
        string CreateCards(Cards card);
        string DeleteCard(long? cardID);

        Task<List<Roles>> GetRoles();
        string CreateRoles(Roles role);
        string DeleteRole(long? roleID);

        Users Authenticate(string username, string password);

        string ChangePassword(Users user,string currentPwd, string newPwd);
    }
}
