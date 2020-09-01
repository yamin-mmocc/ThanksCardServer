using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThanksCardServer.Model;
namespace ThanksCardServer.DataAccess
{
    interface IDataAccessProvider
    {
        void AddDepartmentRecord(Departments dept);
        void UpdateDepartmentRecord(Departments dept);
        void DeleteDepartmentRecord(long id);
        Departments GetDepartmentSingleRecord(long id);
        List<Departments> GetPatientRecords();
    }
}
