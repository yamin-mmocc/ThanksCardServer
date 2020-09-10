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
        public string SaveData(LogSends ls)
        {
            //IDictionary<string, string> response = new Dictionary<string, string>();
            string result;
            try
            {
                // save 
                result = postRepository.SaveComposeToLogSends(ls).ToString();
            }
            catch (Exception)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);

        }

    }
}
