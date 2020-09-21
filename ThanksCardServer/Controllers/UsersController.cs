using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ThanksCardServer.Model;
using Newtonsoft.Json;
using System;
using ThanksCardServer.DataAccess;

namespace ThanksCardServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase //YME created
    {        
        //YME add
        private readonly IPostRepository postRepository;
        public UsersController(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }
        [HttpPost] //YME add
        [Route("GetUsers")]
        public async Task<IActionResult> GetUser(UserDepartmentRole udr)
        {
            try
            {
                var user = await postRepository.GetUsers(udr.User_Name,udr.Department_Name);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost] //YME add
        [Route("CreateUser")]
        public string Register(Users user)
        {
            string result;
            try
            {
                // save 
                result = postRepository.CreateUsers(user,user.Password).ToString();
            }
            catch (Exception)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);
        }
        [HttpPost("authenticate")] //YME add
        public IActionResult Authenticate(Users userto)
        {
            var user = postRepository.Authenticate(userto.User_Name, userto.Password);

            if (user == null)
                return BadRequest(new { message = "Denied" });          
            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                User_ID = user.User_ID,
                Username = user.User_Name,
                Role_ID= user.Role_ID,
                Department_ID =user.Department_ID
            });
        }
        [HttpPost] //YME add
        [Route("DeleteUser")]
        public string Delete(Users user)
        {
            string result = "";
            try
            {
                if ( user != null)
                    // delete 
                    result = postRepository.DeleteUser(user).ToString();
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);
        }
        [HttpPost] //YME add
        [Route("ChangePassword")]
        public string PasswordChange(Users user)
        {
            string result ="";
            try
            {
                // save 
                result = postRepository.ChangePassword(user, user.Password).ToString();
            }
            catch (Exception)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);
        }
        [HttpPost] //YME add
        [Route("GetUserInfoByName")]
        public async Task<IActionResult> GetUserInfoByName(UserDepartmentRole udr)
        {
            try
            {
                var user = await postRepository.getUserInfoByName(udr.User_Name);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost] //YME add
        [Route("GetUserByDept")]
        public async Task<IActionResult> GetUserByDept(Users u)
        {
            try
            {
                var user = await postRepository.getUserByDept(u.Department_ID,u.User_Name);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}