using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThanksCardServer.Model;

namespace ThanksCardServer.DataAccess
{
    public interface IPostRepository //YME created
    {
        Task<List<Departments>> GetDepartments(); //YME add
        string CreateDepartments(Departments dept); //YME add
        string DeleteDepartment(long? Department_ID); //YME add


        Task<List<UserDepartmentRole>> GetUsers(string name, string deptname); //YME add
        string CreateUsers(Users user, string password); //YME add
        string DeleteUser(long? User_ID); //YME add
        Task<List<UserDepartmentRole>> getUserInfoByName(string username); //YME add

        Task<List<Cards>> GetCards(); //YME add
        string CreateCards(Cards card); //YME add
        string DeleteCard(long? cardID); //YME add

        Task<List<Roles>> GetRoles(); //YME add
        string CreateRoles(Roles role); //YME add
        string DeleteRole(long? roleID); //YME add

        Users Authenticate(string username, string password); //YME add

        string ChangePassword(Users user,string currentPwd, string newPwd); //YME add
    }
}
