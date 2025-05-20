using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HalyomorphaHalys.DensityMap.Models;

namespace HalyomorphaHalys.DensityMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugDensityNotificationsController : ControllerBase
    {
        private readonly HazelnutBugDbContext _context;

        public BugDensityNotificationsController(HazelnutBugDbContext context)
        {
            _context = context;
        }

        // GET: api/BugDensityNotifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BugDensityNotification>>> GetBugDensityNotifications()
        {
            return await _context.BugDensityNotifications.ToListAsync();
        }

        // GET: api/BugDensityNotifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BugDensityNotification>> GetBugDensityNotification(int id)
        {
            var bugDensityNotification = await _context.BugDensityNotifications.FindAsync(id);

            if (bugDensityNotification == null)
            {
                return NotFound();
            }

            return bugDensityNotification;
        }

        // PUT: api/BugDensityNotifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBugDensityNotification(int id, BugDensityNotification bugDensityNotification)
        {
            if (id != bugDensityNotification.NotificationId)
            {
                return BadRequest();
            }

            _context.Entry(bugDensityNotification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BugDensityNotificationExists(id))
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

        // POST: api/BugDensityNotifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BugDensityNotification>> PostBugDensityNotification(BugDensityNotification bugDensityNotification)
        {
            _context.BugDensityNotifications.Add(bugDensityNotification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBugDensityNotification", new { id = bugDensityNotification.NotificationId }, bugDensityNotification);
        }

        // DELETE: api/BugDensityNotifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBugDensityNotification(int id)
        {
            var bugDensityNotification = await _context.BugDensityNotifications.FindAsync(id);
            if (bugDensityNotification == null)
            {
                return NotFound();
            }

            _context.BugDensityNotifications.Remove(bugDensityNotification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BugDensityNotificationExists(int id)
        {
            return _context.BugDensityNotifications.Any(e => e.NotificationId == id);
        }
    }
}
