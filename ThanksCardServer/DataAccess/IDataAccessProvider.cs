using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThanksCardServer.Helper;
using ThanksCardServer.Model;
namespace ThanksCardServer.DataAccess
{
    public interface IDataAccessProvider
    {
        IEnumerable<Departments> GetAll();
        Departments GetById(long id);
        Departments Create(Departments dept);
        void Update(Departments dept);
        void Delete(long id);
    }

    public class DataAccessProvider : IDataAccessProvider
    {
        private ApplicationContext  _context;

        public DataAccessProvider(ApplicationContext context)
        {
            _context = context;
        }



        public IEnumerable<Departments> GetAll()
        {
            return _context.Departments;
        }

        public Departments GetById(long id)
        {
            return _context.Departments.Find(id);
        }

        public Departments Create(Departments dept)
        {            
            _context.Departments.Add(dept);
            _context.SaveChanges();

            return dept;
        }

        public void Update(Departments dept)
        {
            var department = _context.Departments.Find(dept.Department_ID);

            if (department == null)
                throw new AppException("Department not found");

            if (dept.Department_Name != department.Department_Name)
            {
                // username has changed so check if the new username is already taken
                if (_context.Departments.Any(x => x.Department_Name == dept.Department_Name))
                    throw new AppException("Department Name " + dept.Department_Name + " is already taken");
            }

            // update user properties
            department.Department_Name = dept.Department_Name;
            department.timeStamp = dept.timeStamp;
                        
            _context.Departments.Update(dept);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var department = _context.Departments.Find(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
            }
        }
    }
}
