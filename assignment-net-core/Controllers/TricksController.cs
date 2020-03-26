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
    public class TricksController : ControllerBase
    {
        private readonly ApiContext _context;

        public TricksController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Tricks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrickRow>>> GetTricks(int? page)
        {
            var tricks = _context.Tricks.Select(x => new TrickRow()
            {
                Id = x.Id,
                Content = x.Content,
                ChallengeId = x.ChallengeId,
                UserId = x.UserId
            }).OrderBy(o => o.Id);
            var initialPage = page ?? 1;
            return await PaginatedList<TrickRow>.CreateAsync(tricks, initialPage, 10);
            //return await _context.Tricks.ToListAsync();
        }

        // GET: api/Tricks/5
        [HttpGet("{search}")]
        public async Task<ActionResult<Trick>> GetTrick(string search)
        {
            var trick = await _context.Tricks.Where(x => x.Content.Contains(search)).ToListAsync();


            if (trick == null)
            {
                return NotFound();
            }

            return Ok(trick);
        }

        // PUT: api/Tricks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrick(int id, Trick trick)
        {
            if (id != trick.Id)
            {
                return BadRequest();
            }

            _context.Entry(trick).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrickExists(id))
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

        // POST: api/Tricks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Trick>> PostTrick(Trick trick)
        {
            //_context.Tricks.Add(trick);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTrick", new { id = trick.Id }, trick);

            var tricky = await _context.Tricks.Where(x => x.ChallengeId == trick.ChallengeId && x.UserId == trick.UserId).FirstOrDefaultAsync();
            if (tricky == null)
            {
                _context.Tricks.Add(trick);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTrick", new { id = trick.Id }, trick);
            }
            else

                return BadRequest(400);



        }

        // DELETE: api/Tricks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Trick>> DeleteTrick(int id)
        {
            var trick = await _context.Tricks.FindAsync(id);
            if (trick == null)
            {
                return NotFound();
            }

            _context.Tricks.Remove(trick);
            await _context.SaveChangesAsync();

            return trick;
        }

        private bool TrickExists(int id)
        {
            return _context.Tricks.Any(e => e.Id == id);
        }
    }
}
