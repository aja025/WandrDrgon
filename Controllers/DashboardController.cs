using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WanderDragon.Data;
using WanderDragon.Models;

namespace WanderDragon.Controllers
{
    [Authorize(Policy = "RegisteredUser")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly WanderDragonContext _context;
        public DashboardController(
            WanderDragonContext context)
        {
            _context = context;
        }

        // GET api/Dashboard/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDashboard([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = await _context.Profiles.Where(n => n.ProfileId == id).FirstOrDefaultAsync();
            var dragon = await _context.Dragons.Where(m => m.ProfileId == id).FirstOrDefaultAsync();
            var dragonSprite = await _context.DragonSprites.Where(o => o.DragonSpriteId == dragon.DragonSpriteId).FirstOrDefaultAsync();
            var image = "";
            if (dragon.XP < 10000)
            {
                image = dragonSprite.Image1;
            }
            else if (dragon.XP >= 10000 && dragon.XP < 50000)
            {
                image = dragonSprite.Image2;
            }
            else
            {
                image = dragonSprite.Image3;
            }
            var dashboard = new DashboardModel {
                Profile = profile,
                Dragon = dragon,
                Image = image
            };

            if (profile == null)
            {
                // return NotFound();
                return Redirect("http://localhost:5000/login");
            }

            return Ok(dashboard);
        }
    }
}