﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThanksCardServer.DataAccess;
using ThanksCardServer.Model;

namespace ThanksCardServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogSendsController_mssm : ControllerBase
    {
        //private readonly ApplicationContext _context;

        //public LogSendsController_mssm(ApplicationContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/LogSendsController_mssm
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<LogSends>>> GetLogSends()
        //{
        //    return await _context.LogSends.ToListAsync();
        //}

        //// GET: api/LogSendsController_mssm/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<LogSends>> GetLogSends(long? id)
        //{
        //    var logSends = await _context.LogSends.FindAsync(id);

        //    if (logSends == null)
        //    {
        //        return NotFound();
        //    }

        //    return logSends;
        //}

        // PUT: api/LogSendsController_mssm/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutLogSends(long? id, LogSends logSends)
        //{
        //    if (id != logSends.SendLog_ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(logSends).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LogSendsExists(id))
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

        // POST: api/LogSendsController_mssm
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<LogSends>> PostLogSends(LogSends logSends)
        //{
        //    _context.LogSends.Add(logSends);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetLogSends", new { id = logSends.SendLog_ID }, logSends);
        //}

        // DELETE: api/LogSendsController_mssm/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<LogSends>> DeleteLogSends(long? id)
        //{
        //    var logSends = await _context.LogSends.FindAsync(id);
        //    if (logSends == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.LogSends.Remove(logSends);
        //    await _context.SaveChangesAsync();

        //    return logSends;
        //}

        //private bool LogSendsExists(long? id)
        //{
        //    return _context.LogSends.Any(e => e.SendLog_ID == id);
        //}
        //MSSM add
        private readonly IPostRepository postRepository;
        public LogSendsController_mssm(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }
        [HttpGet] //MSSM add
        [Route("GetInboxData")]
        public async Task<IActionResult> GetInboxData(LogSends ls)
        {
            try
            {
                var inboxdata = await postRepository.GetInboxData(ls.Receiver_ID);
                if (inboxdata == null)
                {
                    return NotFound();
                }

                return Ok(inboxdata);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}