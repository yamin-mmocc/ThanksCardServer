using System;
using System.Collections.Generic;
using System.Data;
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
        string UpdateDepartment(Departments dept); //YME add
        Task<List<UserDepartmentRole>> GetUsers(string name, string deptname); //YME add
        string CreateUsers(Users user, string password); //YME add
        string DeleteUser(Users user); //YME add
        Task<List<UserDepartmentRole>> getUserInfoByName(string username); //YME add
        //Task<List<Users>> getUserByDept(string deptname); //YME add
        Task<List<Users>> getUserByDept(long? deptid,string username); //YME add
        Task<List<Cards>> GetCards(); //YME add
        string CreateCards(Cards card); //YME add
        string DeleteCard(long? cardID); //YME add
        Task<List<Roles>> GetRoles(); //YME add
        string CreateRoles(Roles role); //YME add
        string DeleteRole(long? roleID); //YME add
        string UpdateRole(Roles role);
        Users Authenticate(string username, string password); //YME add
        string ChangePassword(Users user, string newPwd); //YME add
        Task<List<LogSends>> SaveComposeToLogSends(LogSends ls);//YME add
        string DeleteLogSend(LogSends ls); //YME add
        Task<List<LogReceives>> SaveComposeToLogReceives(LogReceives lr);//YME add
        Task<List<InboxModel>> GetInboxData(InboxModel inbox); //YME add
        Task<List<SendModel>> GetSendData(SendModel send); //YME add
        Task<List<LogSends>> SaveReplyMsgToLogSends(LogSends ls);//YME add
        string DeleteReplyMsgFromLogSend(LogSends ls); //YME add
        string SaveReplyMsgToLogReceives(LogReceives lr); //YME add
        DataTable GetCardTotal(int Frommonth, int Tomonth, int year, long? deptid); //YME add
        DataTable GetDetailData(long? Rec_ID);
    }
}