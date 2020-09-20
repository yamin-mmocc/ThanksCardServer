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
    public class CardsController : ControllerBase  //YME created
    {        
        //yamin add
        private readonly IPostRepository postRepository;
        public CardsController(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }
        [HttpGet] //YME add
        [Route("GetCards")]
        public async Task<IActionResult> GetCard()
        {
            try
            {
                var card = await postRepository.GetCards();
                if (card == null)
                {
                    return NotFound();
                }

                return Ok(card);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost] //YME add
        [Route("CreateCard")]
        public string Register(Cards card)
        {
            string result;
            try
            {
                // save 
                result = postRepository.CreateCards(card).ToString();
            }
            catch (Exception)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);
        }
        [HttpPost] //YME add
        [Route("DeleteCard")]
        public string Delete(long CardID)
        {
            string result = "";
            try
            {
                if ( CardID != 0)
                    // delete 
                    result = postRepository.DeleteCard(CardID).ToString();
            }
            catch (Exception ex)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);
        }
    }
}