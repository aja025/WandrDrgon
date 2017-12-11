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
    public class TravelLogController : Controller
    {
        private readonly WanderDragonContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        // Inject UserManager into constructor to get access to IdentityUser Data (the userName of currently logged in user)
        public TravelLogController(
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

        // GET api/TravelLog
        [HttpGet("{id}")]
        public List<string> GetDragon([FromRoute] int id)
        {
            return _context.TravelLogs.Where(m => m.ProfileId == id)
                                      .Select(m => m.Location)
                                      .Distinct()
                                      .ToList();
        }
        
        // POST: api/TravelLog
        [HttpPost]
        public async Task<IActionResult> AddTravelLog([FromBody] TravelLog travelLog)
        {
            var user = await GetUser(this.User.Identity.Name);
            var profile = await GetProfile(user.UserName);
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // travelLog.ProfileId=profile.ProfileId;
            // _context.TravelLogs.Add(travelLog);
            _context.TravelLogs.Add(travelLog).Entity.ProfileId = profile.ProfileId;
            await _context.SaveChangesAsync();

            return Ok(travelLog);
        }
        // DELETE: api/TravelLog/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravelLog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var travelLog = await _context.TravelLogs.SingleOrDefaultAsync(m => m.TravelLogId == id);
            if (travelLog == null)
            {
                return NotFound();
            }

            _context.TravelLogs.Remove(travelLog);
            await _context.SaveChangesAsync();

            return Ok(travelLog);
        }

        private bool TravelLogExists(int id)
        {
            return _context.TravelLogs.Any(p => p.TravelLogId == id);
        }
    }
}