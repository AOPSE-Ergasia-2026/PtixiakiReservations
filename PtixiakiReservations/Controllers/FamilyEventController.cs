using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PtixiakiReservations.Data;
using PtixiakiReservations.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PtixiakiReservations.Controllers
{
    public class FamilyEventController : Controller
    {        private readonly ApplicationDbContext _context;

        public FamilyEventController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class FamilyEventRequest
        {
            public string EFName { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventFamily([FromBody] FamilyEventRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request?.EFName))
            {
                var newEvent = new FamilyEvent { Name = request.EFName };
                _context.FamilyEvent.Add(newEvent);
                await _context.SaveChangesAsync(); 
                
                return Json(new { success = true, id = newEvent.Id, name = newEvent.Name }); 
            }
            
            return Json(new { success = false, message = "Invalid name." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventFamilies()
        {
            var families = await _context.FamilyEvent
                .Select(f => new { id = f.Id, name = f.Name })
                .ToListAsync();

            return Json(families);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageEventFamily()
        {
            var ef = await _context.FamilyEvent.ToListAsync();
            return View(ef);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateFamilyEvent()
        {
            return View();
        }


        //POST: creatin event type
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFamilyEvent(string EFName)
        {
            if (!string.IsNullOrWhiteSpace(EFName))
            {
                _context.FamilyEvent.Add(new FamilyEvent {Name = EFName});
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageEventFamily));}
            return View();
        }

        //POST: delete event type
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEventFamily(int id)
        {
            var ef = await _context.FamilyEvent.FindAsync(id);

            if (ef != null)
            {
                var linkedEvents = _context.Event.Where(e => e.FamilyEventId == id);

                _context.Event.RemoveRange(linkedEvents);

                _context.FamilyEvent.Remove(ef);

                await _context.SaveChangesAsync();

            }
            return RedirectToAction(nameof(ManageEventFamily));
        }

        //GET: return selected event type to edit
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditEventFamily(int EFId)
        {
            var ef = await _context.FamilyEvent.FindAsync(EFId);

            if (ef == null)
            {
                return NotFound();

            }

            return View(ef);
        }

        //POST: edit event type
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEventFamily(int EFId, string EFName)
        {

            var ef = await _context.FamilyEvent.FindAsync(EFId);

            if(ef!=null && !string.IsNullOrWhiteSpace(EFName))
            {
                ef.Name = EFName;
                _context.Update(ef);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageEventFamily));
            }
            else
            {
                return NoContent();
            }
        }

    }
    }