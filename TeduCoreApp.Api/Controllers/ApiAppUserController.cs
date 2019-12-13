using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAppUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApiAppUserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiAppUser
        [HttpGet]
        public IEnumerable<AppUser> GetAppUsers()
        {
            return _context.AppUsers;
        }

        // GET: api/ApiAppUser/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppUser([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = await _context.AppUsers.FindAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            return Ok(appUser);
        }

        // PUT: api/ApiAppUser/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppUser([FromRoute] Guid id, [FromBody] AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(appUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ApiAppUser
        [HttpPost]
        public async Task<IActionResult> PostAppUser([FromBody] AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AppUsers.Add(appUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppUser", new { id = appUser.Id }, appUser);
        }

        // DELETE: api/ApiAppUser/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppUser([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = await _context.AppUsers.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            _context.AppUsers.Remove(appUser);
            await _context.SaveChangesAsync();

            return Ok(appUser);
        }

        private bool AppUserExists(Guid id)
        {
            return _context.AppUsers.Any(e => e.Id == id);
        }
    }
}