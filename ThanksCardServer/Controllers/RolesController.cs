using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ThanksCardServer.DataAccess;
using ThanksCardServer.Model;

namespace ThanksCardServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase //ATK created
    {
        //ATK add
        private readonly IPostRepository postRepository;
        public RolesController(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }
        [HttpGet] //ATK add
        [Route("GetRoles")]
        public async Task<IActionResult> GetRole()
        {
            try
            {
                var role = await postRepository.GetRoles();
                if (role == null)
                {
                    return NotFound();
                }
                return Ok(role);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost] //ATK add
        [Route("CreateRoles")]
        public string Register(Roles role)
        {            
            string result;
            try
            {
                // save 
                result = postRepository.CreateRoles(role).ToString();
            }
            catch (Exception)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);
        }
        [HttpPost] //YME add
        [Route("DeleteRole")]
        public string Delete(long RoleID)
        {
            string result = "";
            try
            {
                if (RoleID != 0)
                    // delete 
                    result = postRepository.DeleteRole(RoleID).ToString();
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost] //YME add
        [Route("UpdateRoleName")]
        public string Update(Roles role)
        {
            string result = "";
            try
            {
                if (role != null)
                    // update 
                    result = postRepository.UpdateRole(role).ToString();
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);
        }
    }
}