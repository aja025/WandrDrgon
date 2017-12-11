using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WanderDragon.Data;
using WanderDragon.Models;

namespace WanderDragon.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DragonController : Controller
    {
        private readonly WanderDragonContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        // Inject UserManager into constructor to get access to IdentityUser Data (the userName of currently logged in user)
        public DragonController(
            WanderDragonContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Declare GetUser() method so we can access UserModel.UserName
        private async Task<UserModel> GetUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var claims = await _userManager.GetClaimsAsync(user);
            var vm = new UserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Claims = claims.ToDictionary(c => c.Type, c => c.Value)
            };
            return vm;
        }

        // Get profiile of currently logged in user
        private async Task<Profile> GetProfile(string userName)
        {
            var profile = await _context.Profiles.SingleOrDefaultAsync(m => m.UserName == userName);
            var pid = new Profile
            {
                ProfileId = profile.ProfileId,
            };
            return pid;
        }

        // GET api/Dragon
        [HttpGet]
        public IEnumerable<Dragon> GetDragon()
        {
            return _context.Dragons;
        }

        // GET api/Dragon/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDragon([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             var dragon = await _context.Dragons.Where(m => m.ProfileId == id).FirstOrDefaultAsync();

            if (dragon == null)
            {
                return NotFound();
            }

            return Ok(dragon);
        }

        // POST: api/Dragon
        [HttpPost]
        public async Task<IActionResult> AddDragon([FromBody] Dragon dragon)
        {
            var user = await GetUser(this.User.Identity.Name);
            var profile = await GetProfile(user.UserName);
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var itemLog = new ItemLog {
                ProfileId = profile.ProfileId
            };
            _context.ItemLogs.Add(itemLog);
            _context.Dragons.Add(dragon).Entity.ProfileId = profile.ProfileId;
            await _context.SaveChangesAsync();

            return Ok(dragon);
        }

        // PUT: api/Dragon/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDragon([FromRoute] int id, [FromBody] DragonUpdate  Updates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dragon = _context.Dragons.Where(m => m.ProfileId == id).FirstOrDefault();
            if (dragon == null)
            {
                return NotFound();
            }
            else
            {
                _context.Dragons.Update(dragon).Entity.KmRadius += Updates.KmRadius;
                _context.Dragons.Update(dragon).Entity.ChallengesWon += Updates.ChallengesWon;
                _context.Dragons.Update(dragon).Entity.KmTravelled += Updates.KmTravelled;
                _context.Dragons.Update(dragon).Entity.XP += Updates.Xp;            
                _context.Dragons.Update(dragon).Entity.TripsTaken += Updates.TripsTaken;
                _context.Entry(dragon).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            return Ok(dragon);
        }

        // DELETE: api/Dragon/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDragon([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dragon = await _context.Dragons.SingleOrDefaultAsync(m => m.DragonId == id);
            if (dragon == null)
            {
                return NotFound();
            }

            _context.Dragons.Remove(dragon);
            await _context.SaveChangesAsync();

            return Ok(dragon);
        }

        private bool DragonExists(int id)
        {
            return _context.Dragons.Any(p => p.DragonId == id);
        }
    }
}