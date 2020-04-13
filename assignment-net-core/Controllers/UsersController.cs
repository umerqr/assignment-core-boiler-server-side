using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using assignment_net_core.Models;
using assignment_net_core.DTO;

namespace assignment_net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiContext _context;

        public UsersController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRow>>> GetUsers(int? page)
        {
            var users = _context.Users.Include(c => c.Tricks).Select(x => new UserRow()
            {
                Id = x.Id,
                Name = x.Name,
                TrickCount = x.Tricks.Count
            }).OrderBy(o => o.Id);
            var initialPage = page ?? 1;
            //var totalpage = PaginatedList<UserRow>.CreateAsync(users, initialPage, 10 );
            return await PaginatedList<UserRow>.CreateAsync(users, initialPage, 10);
        }

        // GET: api/Users/search
        [HttpGet("{search}")]
        public async Task<ActionResult<User>> GetUser(string search)
        {
            var user = _context.Users.Where(x => x.Name.StartsWith(search)).Include(c => c.Tricks).Select(x => new UserRow()
            {
                Id = x.Id,
                Name = x.Name,
                TrickCount = x.Tricks.Count
            }).OrderBy(o => o.Id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
