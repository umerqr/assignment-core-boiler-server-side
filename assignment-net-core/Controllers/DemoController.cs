using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assignment_net_core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assignment_net_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly ApiContext _context;

        public DemoController(ApiContext context)
        {
            _context = context;
        }

        [Route("brands")]
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Brand>>> Brands()
        {

            for (int i = 0; i < 20; i++)
            {
                Brand obj = new Brand
                {

                    Name = "Redmi" + i
                };
                _context.Brands.Add(obj);
                _context.SaveChanges();

                Brand obj2 = new Brand
                {

                    Name = "Microsoft" + i
                };
                _context.Brands.Add(obj2);
                _context.SaveChanges();

                Brand obj3 = new Brand
                {

                    Name = "Apple" +i
                };
                _context.Brands.Add(obj3);
                _context.SaveChanges();

                Brand obj4 = new Brand
                {

                    Name = "Samsung" +i
                };
                _context.Brands.Add(obj4);
                _context.SaveChanges();

                Brand obj5 = new Brand
                {

                    Name = "Nokia"+i
                };
                _context.Brands.Add(obj5);
                _context.SaveChanges();
            }


            return Ok();
        }

        [Route("Users")]
        [HttpGet]

        public async Task<ActionResult<IEnumerable<User>>> Users()
        {

            for (int i = 0; i < 100; i++)
            {
                User user = new User
                {

                    Name = "User "+i
                };
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            return Ok();
        }
        [Route("Challenges")]
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Challenge>>> Challenges()
        {

            for (int i = 0; i < 100; i++)
            {
                Challenge challenge = new Challenge
                {

                    BrandId = 1,
                    Name = "Tough Challenge " +i



                };
                _context.Challenges.Add(challenge);
                _context.SaveChanges();
            }
           

            

            return Ok();
        }

        [Route("Tricks")]
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Trick>>> Tricks()
        {
            for (int i = 1; i < 101; i++)
            {


            Trick trick = new Trick
            {

                ChallengeId = i,
                Content = "A pretty good trick "+ i,
                UserId = i
            };
            _context.Tricks.Add(trick);
            _context.SaveChanges();

            }
            return Ok();
        }
        


    }
}