using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.AppointmentModels;

namespace hhs_p6_webshop_project.Controllers
{
    public class AppointmentTimesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentTimesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: AppointmentTimes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AppointmentTime.ToListAsync());
        }

        // GET: AppointmentTimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentTime = await _context.AppointmentTime.SingleOrDefaultAsync(m => m.ID == id);
            if (appointmentTime == null)
            {
                return NotFound();
            }

            return View(appointmentTime);
        }

        // GET: AppointmentTimes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppointmentTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DateTime")] AppointmentTime appointmentTime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointmentTime);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(appointmentTime);
        }

        // GET: AppointmentTimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentTime = await _context.AppointmentTime.SingleOrDefaultAsync(m => m.ID == id);
            if (appointmentTime == null)
            {
                return NotFound();
            }
            return View(appointmentTime);
        }

        // POST: AppointmentTimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DateTime")] AppointmentTime appointmentTime)
        {
            if (id != appointmentTime.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointmentTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentTimeExists(appointmentTime.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(appointmentTime);
        }

        // GET: AppointmentTimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentTime = await _context.AppointmentTime.SingleOrDefaultAsync(m => m.ID == id);
            if (appointmentTime == null)
            {
                return NotFound();
            }

            return View(appointmentTime);
        }

        // POST: AppointmentTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointmentTime = await _context.AppointmentTime.SingleOrDefaultAsync(m => m.ID == id);
            _context.AppointmentTime.Remove(appointmentTime);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AppointmentTimeExists(int id)
        {
            return _context.AppointmentTime.Any(e => e.ID == id);
        }
    }
}
