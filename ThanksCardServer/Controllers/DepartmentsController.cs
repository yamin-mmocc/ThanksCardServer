using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ThanksCardServer.DataAccess;
using ThanksCardServer.Helper;
using ThanksCardServer.Model;

namespace ThanksCardServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public DepartmentsController(ApplicationContext context)
        {
            _context = context;
        }
        //private readonly IDataAccessProvider _dataAccessProvider;

        //public DepartmentsController(IDataAccessProvider dataAccessProvider)
        //{
        //    _dataAccessProvider = dataAccessProvider;
        //}

        //// GET: api/Departments
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Departments>>> GetDepartments()
        //{
        //    return await _context.Departments.ToListAsync();
        //}

        //// GET: api/Departments/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Departments>> GetDepartments(long? id)
        //{
        //    var departments = await _context.Departments.FindAsync(id);

        //    if (departments == null)
        //    {
        //        return NotFound();
        //    }

        //    return departments;
        //}

        //// PUT: api/Departments/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDepartments(long? id, Departments departments)
        //{
        //    if (id != departments.Department_ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(departments).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DepartmentsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Departments
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Departments>> PostDepartments(Departments departments)
        //{
        //    _context.Departments.Add(departments);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDepartments", new { id = departments.Department_ID }, departments);
        //}

        //// DELETE: api/Departments/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Departments>> DeleteDepartments(long? id)
        //{
        //    var departments = await _context.Departments.FindAsync(id);
        //    if (departments == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Departments.Remove(departments);
        //    await _context.SaveChangesAsync();

        //    return departments;
        //}

        //private bool DepartmentsExists(long? id)
        //{
        //    return _context.Departments.Any(e => e.Department_ID == id);
        //}

        //[HttpGet]
        //public IEnumerable<Departments> Get()
        //{
        //    return _dataAccessProvider.GetDepartmentRecords();
        //}

        //[HttpPost]
        //public IActionResult Create([FromBody] Departments dept)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Guid obj = Guid.NewGuid();
        //        //dept.Department_ID = obj.ToString();
        //        _dataAccessProvider.AddDepartmentRecord(dept);
        //        return Ok();
        //    }
        //    return BadRequest();
        //}

        //[HttpGet("{id}")]
        //public Departments Details(long id)
        //{
        //    return _dataAccessProvider.GetDepartmentSingleRecord(id);
        //}

        //[HttpPut]
        //public IActionResult Edit([FromBody] Departments dept)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _dataAccessProvider.UpdateDepartmentRecord(dept);
        //        return Ok();
        //    }
        //    return BadRequest();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteConfirmed(long id)
        //{
        //    var data = _dataAccessProvider.GetDepartmentSingleRecord(id);
        //    if (data == null)
        //    {
        //        return NotFound();
        //    }
        //    _dataAccessProvider.GetDepartmentSingleRecords(id);
        //    return Ok();
        //}

        //yamin start
        //private IDataAccessProvider _dataAccessProvider;       
        //private readonly AppSettings _appSettings;

        //public DepartmentsController(
        //    IDataAccessProvider dataAccessProvider,            
        //    IOptions<AppSettings> appSettings)
        //{
        //    _dataAccessProvider = dataAccessProvider;            
        //    _appSettings = appSettings.Value;
        //}

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var department = _dataAccessProvider.GetAll();            
        //    return Ok();
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetById(long id)
        //{
        //    var department = _dataAccessProvider.GetById(id);            
        //    return Ok();
        //}


        //[HttpDelete("{id}")]
        //public IActionResult Delete(long id)
        //{
        //    _dataAccessProvider.Delete(id);
        //    return Ok();
        //}

        [HttpPost]
        [Route("CreateDept")]
        public string Register(Departments dept)
        {
            var department = new Departments();
            department.Department_Name = dept.Department_Name;
            department.IsActive = dept.IsActive;
            department.timeStamp = dept.timeStamp;
            _context.Departments.Add(department);
            
            IDictionary<string, string> result = new Dictionary<string, string>();
            try
            {
                _context.SaveChangesAsync();
                result["status"] = "200";
                result["message"] = "Success";
            }
            catch
            {
                result["status"] = "404";
                result["message"] = "Failed";
            }
            return JsonConvert.SerializeObject(result);
            //try
            //{
            //    // save 
            //    //_dataAccessProvider.Create(dept);
            //    return "OK";
            //}
            //catch (AppException ex)
            //{
            //    // return error message if there was an exception
            //    return "NotOK";
            //}
        }

    }
}
