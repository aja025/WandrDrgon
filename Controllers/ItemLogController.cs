using System;
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
    public class ItemLogController : Controller
    {
        private readonly WanderDragonContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        // Inject UserManager into constructor to get access to IdentityUser Data (the userName of currently logged in user)
        public ItemLogController(
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

        // GET api/ItemLog
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemLog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemLog = await _context.ItemLogs.SingleOrDefaultAsync(m => m.ProfileId == id);

            if (itemLog == null)
            {
                return NotFound();
            }

            return Ok(itemLog);
        }

        // PUT: api/ItemLog/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemLog([FromRoute] int id, [FromBody] ItemLogUpdate  Updates)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemLog = _context.ItemLogs.Where(m => m.ProfileId == id).FirstOrDefault();
            if (itemLog == null)
            {
                return NotFound();
            }
            else
            {
                _context.ItemLogs.Update(itemLog).Entity.Alexandrite += Updates.Alexandrite;
                _context.ItemLogs.Update(itemLog).Entity.Amethyst += Updates.Amethyst;
                _context.ItemLogs.Update(itemLog).Entity.AnimalMask += Updates.AnimalMask;
                _context.ItemLogs.Update(itemLog).Entity.Aquamarine += Updates.Aquamarine;
                _context.ItemLogs.Update(itemLog).Entity.BeadedNecklace += Updates.BeadedNecklace;
                _context.ItemLogs.Update(itemLog).Entity.Bloodstone += Updates.Bloodstone;
                _context.ItemLogs.Update(itemLog).Entity.Boomarang += Updates.Boomarang;
                _context.ItemLogs.Update(itemLog).Entity.Cherryblossom += Updates.Cherryblossom;
                _context.ItemLogs.Update(itemLog).Entity.Cholla += Updates.Cholla;
                _context.ItemLogs.Update(itemLog).Entity.Croissant += Updates.Croissant;
                _context.ItemLogs.Update(itemLog).Entity.Garnet += Updates.Garnet;
                _context.ItemLogs.Update(itemLog).Entity.Hamburger += Updates.Hamburger;
                _context.ItemLogs.Update(itemLog).Entity.HockeyPuck += Updates.HockeyPuck;
                _context.ItemLogs.Update(itemLog).Entity.Jade += Updates.Jade;
                _context.ItemLogs.Update(itemLog).Entity.KoalaDoll += Updates.KoalaDoll;
                _context.ItemLogs.Update(itemLog).Entity.MapleLeaf += Updates.MapleLeaf;
                _context.ItemLogs.Update(itemLog).Entity.MatryoshkaDoll += Updates.MatryoshkaDoll;
                _context.ItemLogs.Update(itemLog).Entity.Moonstone += Updates.Moonstone;
                _context.ItemLogs.Update(itemLog).Entity.Noodlebowl += Updates.Noodlebowl;
                _context.ItemLogs.Update(itemLog).Entity.Opal += Updates.Opal;
                _context.ItemLogs.Update(itemLog).Entity.ParrotFeather += Updates.ParrotFeather;
                _context.ItemLogs.Update(itemLog).Entity.Pearl += Updates.Pearl;
                _context.ItemLogs.Update(itemLog).Entity.Pierogi += Updates.Pierogi;
                _context.ItemLogs.Update(itemLog).Entity.PinaColada += Updates.PinaColada;
                _context.ItemLogs.Update(itemLog).Entity.Pineapple += Updates.Pineapple;
                _context.ItemLogs.Update(itemLog).Entity.Plumeria += Updates.Plumeria;
                _context.ItemLogs.Update(itemLog).Entity.Sapphire += Updates.Sapphire;
                _context.ItemLogs.Update(itemLog).Entity.TeaBag += Updates.TeaBag;
                _context.ItemLogs.Update(itemLog).Entity.TigersEye += Updates.TigersEye;
                _context.ItemLogs.Update(itemLog).Entity.Topaz += Updates.Topaz;
                _context.ItemLogs.Update(itemLog).Entity.Turquoise += Updates.Turquoise;

                _context.Entry(itemLog).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            return Ok(itemLog);
        }
    }
}