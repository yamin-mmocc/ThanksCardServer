using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
//using ServiceStack;
using ThanksCardServer.DataAccess;
using ThanksCardServer.Helper;
using ThanksCardServer.Model;


namespace ThanksCardServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase //YME created
    {        
        //YME add
        private readonly IPostRepository postRepository;
        public DepartmentsController(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }
        [HttpGet] //YME add
        [Route("GetDepartments")]
        public async Task<IActionResult> GetDept()
        {
            try
            {
                var depart = await postRepository.GetDepartments();
                if (depart == null)
                {
                    return NotFound();
                }

                return Ok(depart);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost] //YME add
        [Route("CreateDept")]
        public string Register(Departments dept)
        {
            //IDictionary<string, string> response = new Dictionary<string, string>();
            string result;
            try
            {
                // save 
                result = postRepository.CreateDepartments(dept).ToString();            
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);
        }
        [HttpPost] //YME add
        [Route("DeleteDept")]
        public string Delete(long DeptID)
        {
            string result="";
            try
            {
                if(DeptID != 0)
                // delete 
                result = postRepository.DeleteDepartment(DeptID).ToString();
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost] //YME add
        [Route("UpdateDeptName")]
        public string Update(Departments dept)
        {
            string result = "";
            try
            {
                if (dept != null)
                    // update 
                    result = postRepository.UpdateDepartment(dept).ToString();
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);
        }
    }
}