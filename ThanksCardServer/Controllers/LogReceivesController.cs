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
    public class LogReceivesController : ControllerBase
    {
        private readonly IPostRepository postRepository;
        public LogReceivesController(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }
        [HttpPost] //YME add
        [Route("SaveReceives")]
        public string SaveData(LogReceives lr)
        {
            string result;
            try
            {
                // save 
                result = postRepository.SaveComposeToLogReceives(lr).ToString();
            }
            catch (Exception)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);
        }
        [HttpPost] //YME add
        [Route("GetInboxData")]
        public async Task<IActionResult> GetInboxData(InboxModel Inbox)
        {
            try
            {
                var ib = await postRepository.GetInboxData(Inbox);
                if (ib == null)
                {
                    return NotFound();
                }

                return Ok(ib);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost] //YME add
        [Route("SaveReplyMsgToLR")]
        public string SaveReplyMsg(LogReceives lr)
        {
            string result;
            try
            {
                // save 
                result = postRepository.SaveReplyMsgToLogReceives(lr).ToString();
            }
            catch (Exception)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);
        }
    }
}