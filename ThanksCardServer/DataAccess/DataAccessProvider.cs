using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThanksCardServer.Model;

namespace ThanksCardServer.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly ApplicationContext _context;

        public DataAccessProvider(ApplicationContext context)
        {
            _context = context;
        }

        public void AddDepartmentRecord(Departments dept)
        {
            _context.Departments.Add(dept);
            _context.SaveC();
        }

        public void UpdateDepartmentRecord(Departments dept)
        {
            _context.Departments.Update(dept);
            _context.SaveChanges();
        }

        public void DeleteDepartmentRecord(long id)
        {
            var entity = _context.Departments.FirstOrDefault(t => t.id == id);
            _context.Departments.Remove(entity);
            _context.SaveChanges();
        }

        public Departments GetDepartmentSingleRecord(long id)
        {
            return _context.Departments.FirstOrDefault(t => t.id == id);
        }

        public List<Departments> GetPatientRecords()
        {
            return _context.Departments.ToList();
        }
    }
}

    

