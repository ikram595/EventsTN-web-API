using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventsTN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EventsTN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EventsController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Events
        [HttpGet("get-all-events")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsTN()
        {
          if (_context.EventsTN == null)
          {
              return NotFound();
          }
            return await _context.EventsTN.ToListAsync();
        }

        // GET: api/Events/5
        [Authorize]
        [HttpGet("get-event-by-id/{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
          if (_context.EventsTN == null)
          {
              return NotFound();
          }
            var @event = await _context.EventsTN.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin,Organisateur")]
        [HttpPut("edit-event/{id}")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
            if (id != @event.EventId)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin,Organisateur")]
        [HttpPost("create-event")]
        public async Task<ActionResult<Event>> PostEvent([FromBody]  Event @event)
        {
          if (_context.EventsTN == null)
          {
              return Problem("Entity set 'AppDbContext.EventsTN'  is null.");
          }
            // Example: Retrieve the currently authenticated user
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser == null)
            {
                // User not authenticated, return unauthorized status
                return Unauthorized();
            }

            // Associate the event with the current user
            @event.UserId = currentUser.Id;
            _context.EventsTN.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.EventId }, @event);
        }

        // DELETE: api/Events/5
        [Authorize(Roles = "Admin,Organisateur")]

        [HttpDelete("delete-event/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            if (_context.EventsTN == null)
            {
                return NotFound();
            }
            var @event = await _context.EventsTN.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.EventsTN.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(int id)
        {
            return (_context.EventsTN?.Any(e => e.EventId == id)).GetValueOrDefault();
        }
    }
}
