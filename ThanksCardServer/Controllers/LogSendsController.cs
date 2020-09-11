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
    public class LogSendsController : ControllerBase
    {
        private readonly IPostRepository postRepository;
        public LogSendsController(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }

        [HttpPost] //YME add
        [Route("SaveSends")]
        public async Task<IActionResult> SaveData(LogSends ls)
        {
            try
            {
                var logsend = await postRepository.SaveComposeToLogSends(ls);
                if (logsend == null)
                {
                    return NotFound();
                }

                return Ok(logsend);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost] //YME add
        [Route("DeleteSends")]
        public string DeleteSend(LogSends ls)
        {
            string result = "";
            try
            {
                if (ls != null)
                    // delete 
                    result = postRepository.DeleteLogSend(ls).ToString();
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost] //YME add
        [Route("GetSendData")]
        public async Task<IActionResult> GetInboxData(SendModel send)
        {
            try
            {
                var s = await postRepository.GetSendData(send);
                if (s == null)
                {
                    return NotFound();
                }

                return Ok(s);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
