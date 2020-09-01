﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ThanksCardServer.Model;
using Newtonsoft.Json;
using System;

namespace ThanksCardServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public UsersController(ApplicationContext context)
        {
            _context = context;
        }
        //start
        //// GET: api/Users
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        //{
        //    return await _context.Users.ToListAsync();
        //}

        //// GET: api/Users/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Users>> GetUsers(long? id)
        //{
        //    var users = await _context.Users.FindAsync(id);

        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    return users;
        //}

        //// PUT: api/Users/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUsers(long? id, Users users)
        //{
        //    if (id != users.User_ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(users).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UsersExists(id))
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

        //// POST: api/Users
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Users>> PostUsers(Users users)
        //{
        //    _context.Users.Add(users);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUsers", new { id = users.User_ID }, users);
        //}

        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Users>> DeleteUsers(long? id)
        //{
        //    var users = await _context.Users.FindAsync(id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(users);
        //    await _context.SaveChangesAsync();

        //    return users;
        //}

        //private bool UsersExists(long? id)
        //{
        //    return _context.Users.Any(e => e.User_ID == id);
        //}
        //end

        //yamin start
        // GET: api/Users
        //[HttpGet]
        //[Route("Login")]
        //public async Task<Users> Login(string username, string password)
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(x => x.User_Name == username); //Get user from database.
        //    if (user == null)
        //        return null; // User does not exist.

        //    if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
        //        return null;

        //    return user;
        //}

        //private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // Create hash using password salt.
        //        for (int i = 0; i < computedHash.Length; i++)
        //        { // Loop through the byte array
        //            if (computedHash[i] != passwordHash[i]) return false; // if mismatch
        //        }
        //    }
        //    return true; //if no mismatches.
        //}

        //// POST: api/Users
        //[HttpPost]
        //[Route("Register")]
        //public async Task<Users> Register(Users user, string password)
        //{
        //    byte[] passwordHash, passwordSalt;
        //    CreatePasswordHash(password, out passwordHash, out passwordSalt);

        //    user.PasswordHash = passwordHash;
        //    user.PasswordSalt = passwordSalt;

        //    await _context.Users.AddAsync(user); // Adding the user to context of users.
        //    await _context.SaveChangesAsync(); // Save changes to database.

        //    return user;
        //}

        //private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using (var hmac = new System.Security.Cryptography.HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}

        //// GET: api/Users
        //[HttpGet]
        //[Route("CheckUserExists")]
        //public async Task<bool> UserExists(string username)
        //{
        //    if (await _context.Users.AnyAsync(x => x.User_Name == username))
        //        return true;
        //    return false;
        //}

        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Users>> DeleteUsers(long? id)
        //{
        //    var users = await _context.Users.FindAsync(id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(users);
        //    await _context.SaveChangesAsync();

        //    return users;
        //}

        //yamin start1
        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("Login")]
        public string CheckUser(string user_name, string password)
        {
            // var user = await _context.Users.FindAsync(id);
            var user = _context.Users.Where(e => e.User_Name == user_name && e.Password == password);
            IDictionary<string, string> result = new Dictionary<string, string>();
            if (user != null)
            {
                result["status"] = "200";
                result["message"] = "Login Success";
            }
            else
            {
                result["status"] = "404";
                result["message"] = "Login Denied";
            }
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        [Route("Register")]
        public string CreateUser(Users user)
        {            
            try 
            {
                // save 
                _context.SaveChangesAsync();
                return "Successfully Register";
            } 
            catch(Exception ex)
            {
                // return error message if there was an exception
                return "Try Again";
            }
        }
    }
}