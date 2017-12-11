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
    [Route("api/ItemSprites")]
    public class ItemSpritesController : Controller
    {
        private readonly WanderDragonContext _context;

        public ItemSpritesController(WanderDragonContext context)
        {
            _context = context;
        }

        // GET: api/ItemSprites
        [HttpGet]
        public IEnumerable<ItemSprite> GetItemSprite()
        {
            // HttpContext.Current.Response.Flush();
            return _context.ItemSprites;
        }

        // GET: api/DragonSprites/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemSprite([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemSprite = await _context.ItemSprites.SingleOrDefaultAsync(m => m.ItemSpriteId == id);

            if (itemSprite == null)
            {
                return NotFound();
            }
            // HttpContext.Current.Response.Flush();
            return Ok(itemSprite);
        }
    }
}