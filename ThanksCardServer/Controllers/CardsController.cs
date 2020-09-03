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
    public class CardsController : ControllerBase
    {
        //private readonly ApplicationContext _context;

        //public CardsController(ApplicationContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/Cards
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Cards>>> GetCards()
        //{
        //    return await _context.Cards.ToListAsync();
        //}

        //// GET: api/Cards/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Cards>> GetCards(long? id)
        //{
        //    var cards = await _context.Cards.FindAsync(id);

        //    if (cards == null)
        //    {
        //        return NotFound();
        //    }

        //    return cards;
        //}

        //// PUT: api/Cards/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCards(long? id, Cards cards)
        //{
        //    if (id != cards.Card_ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(cards).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CardsExists(id))
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

        //// POST: api/Cards
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Cards>> PostCards(Cards cards)
        //{
        //    _context.Cards.Add(cards);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCards", new { id = cards.Card_ID }, cards);
        //}

        //// DELETE: api/Cards/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Cards>> DeleteCards(long? id)
        //{
        //    var cards = await _context.Cards.FindAsync(id);
        //    if (cards == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Cards.Remove(cards);
        //    await _context.SaveChangesAsync();

        //    return cards;
        //}

        //private bool CardsExists(long? id)
        //{
        //    return _context.Cards.Any(e => e.Card_ID == id);
        //}

        //yamin test
        private readonly IPostRepository postRepository;
        public CardsController(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }

        [HttpGet]
        [Route("GetCards")]
        public async Task<IActionResult> GetCard()
        {
            try
            {
                var card = await postRepository.GetCard();
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
        //yamin test

        [HttpPost]
        [Route("CreateCard")]
        public string Register(Cards card)
        {
            //IDictionary<string, string> response = new Dictionary<string, string>();
            string result;
            try
            {
                // save 
                result = postRepository.CreateCard(card).ToString();
            }
            catch (Exception)
            {
                result = "Error";
            }
            return JsonConvert.SerializeObject(result);

        }
    }
}
