using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WanderDragon.Data;
using WanderDragon.Models;

namespace WanderDragon.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private readonly WanderDragonContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        // Inject UserManager into constructor to get access to IdentityUser Data
        public ProfileController(
            WanderDragonContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Declare GetUser() method so we can access UserModel.Id == GUID :)
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

        // GET api/Profile
        [HttpGet]
        public IEnumerable<Profile> GetProfiles()
        {
            return _context.Profiles;
        }

       // GET api/Profile/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             var profile = await _context.Profiles.SingleOrDefaultAsync(m => m.ProfileId == id);

            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }

        // // POST: api/Profile
        // [HttpPost]
        // public async Task<IActionResult> AddProfile([FromBody] Profile profile)
        // {
        //     var user = await GetUser(this.User.Identity.Name);
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     _context.Profiles.Add(profile).Entity.UserId=user.Id;
        //     _context.Profiles.Add(profile).Entity.UserName=user.UserName;
        //     await _context.SaveChangesAsync();

        //     return Ok(profile);
        // }

        // PUT: api/Profile/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile([FromRoute] int id, [FromBody] Profile profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ProfileExists(id))
            {
                return NotFound();
            }

            var profileUpdate = await _context.Profiles.FirstOrDefaultAsync(m => m.ProfileId == id);
            profileUpdate.DisplayName = profile.DisplayName;
            profileUpdate.AboutMe = profile.AboutMe;
            profileUpdate.Image = profile.Image;
            // _context.Profiles.Update(profileUpdate).Entity.DisplayName = profile.DisplayName;
            _context.Entry(profileUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               return NotFound();
            }

            // return NoContent();  <-- WHICH RETURN TO USE??
            return Ok(profile);
        }


        // DELETE: api/Profile/id
        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = await _context.Profiles.SingleOrDefaultAsync(m => m.ProfileId == id);
            if (profile == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();

            return Ok(profile);
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(p => p.ProfileId == id);
        }
    }
}