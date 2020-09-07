﻿using System;
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
            //IDictionary<string, string> response = new Dictionary<string, string>();
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
            //added
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
    }
}
