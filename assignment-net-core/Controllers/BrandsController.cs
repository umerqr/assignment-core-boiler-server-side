using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using assignment_net_core.Models;
using ReflectionIT.Mvc.Paging;
using assignment_net_core.DTO;

namespace assignment_net_core.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly ApiContext _context;

        public BrandsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandRow>>> GetBrands(int? page)
        {


            var brands = _context.Brands.Include(b => b.Challenges).Select(x => new BrandRow()
            {
                ChallengeCount = x.Challenges.Count,
                Id = x.Id,
                Name = x.Name
            }).OrderBy(p => p.Id);
            var initialPage = page ?? 1;

            return await PaginatedList<BrandRow>.CreateAsync(brands, initialPage, 10);
        }

        // GET: api/Brands/5
        [HttpGet("{search}")]
        public async Task<ActionResult<Brand>> GetBrand(string search)
        {
            var brand = await _context.Brands.Where(x => x.Name.StartsWith(search)).ToListAsync();

            if (brand == null)
            {
                return NotFound();
            }

            return Ok (brand);
        }

        // PUT: api/Brands/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
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

        // POST: api/Brands
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrand", new { id = brand.Id }, brand);
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Brand>> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return brand;
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}
