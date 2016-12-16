using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Mail;
using hhs_p6_webshop_project.Models.AppointmentModels;

namespace hhs_p6_webshop_project.Controllers {
    public class AppointmentsController : Controller {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index() {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated) {
                return View(await _context.Appointment.ToListAsync());
            } else {
                return NotFound();
            }
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var appointment = await _context.Appointment.SingleOrDefaultAsync(m => m.ID == id);
            if (appointment == null) {
                return NotFound();
            }

            // Check if user is authenticated
            if (User.Identity.IsAuthenticated) {
                return View(appointment);
            } else {
                return NotFound();
            }
        }

        // GET: Appointments/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Confirmation,DateMarried,AppointmentDateTime,Mail,Name,Phone")] Appointment appointment) {
            if (ModelState.IsValid) {
                MailClient.SendAppointmentEmail(appointment.Name, appointment.Mail, appointment.AppointmentDateTime, "het Armani Pak");

                _context.Add(appointment);

                

                await _context.SaveChangesAsync();
                return RedirectToAction("Thanks");
            }
            return View(appointment);
        }

        public IActionResult Thanks() {
            return View();
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var appointment = await _context.Appointment.SingleOrDefaultAsync(m => m.ID == id);
            if (appointment == null) {
                return NotFound();
            }

            // Check if user is authenticated
            if (User.Identity.IsAuthenticated) {
                return View(appointment);
            } else {
                return NotFound();
            }
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Confirmation,DateMarried,AppointmentDateTime,Mail,Name,Phone")] Appointment appointment) {
            if (id != appointment.ID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!AppointmentExists(appointment.ID)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            // Check if user is authenticated
            if (User.Identity.IsAuthenticated) {
                return View(appointment);
            } else {
                return NotFound();
            }
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var appointment = await _context.Appointment.SingleOrDefaultAsync(m => m.ID == id);
            if (appointment == null) {
                return NotFound();
            }

            // Check if user is authenticated
            if (User.Identity.IsAuthenticated) {
                return View(appointment);
            } else {
                return NotFound();
            }
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var appointment = await _context.Appointment.SingleOrDefaultAsync(m => m.ID == id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AppointmentExists(int id) {
            return _context.Appointment.Any(e => e.ID == id);
        }
    }
}
