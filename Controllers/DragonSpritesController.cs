using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WanderDragon.Data;
using WanderDragon.Models;

namespace WanderDragon.Controllers
{
    [Produces("application/json")]
    [Route("api/DragonSprites")]
    public class DragonSpritesController : Controller
    {
        private readonly WanderDragonContext _context;

        public DragonSpritesController(WanderDragonContext context)
        {
            _context = context;
        }

        // GET: api/DragonSprites
        [HttpGet]
        public IEnumerable<DragonSprite> GetDragonSprite()
        {
            // HttpContext.Current.Response.Flush();
            return _context.DragonSprites;
        }

        // GET: api/DragonSprites/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDragonSprite([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dragonSprite = await _context.DragonSprites.SingleOrDefaultAsync(m => m.DragonSpriteId == id);

            if (dragonSprite == null)
            {
                return NotFound();
            }
            // HttpContext.Current.Response.Flush();
            return Ok(dragonSprite);
        }
    }
}