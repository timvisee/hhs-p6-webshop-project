using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.App.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.AppointmentModels;
using Microsoft.Extensions.Options;
using hhs_p6_webshop_project.Services.Abstracts;
using hhs_p6_webshop_project.Services.Containers;

namespace hhs_p6_webshop_project.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IOptions<SecureAppConfig> _secretConfig;
        private readonly ITransactionalEmailService _emailService;

        public AppointmentsController(ApplicationDbContext context, IOptions<SecureAppConfig> secretConfig, ITransactionalEmailService emailService)
        {
            _context = context;
            _secretConfig = secretConfig;
            _emailService = emailService;
        }

        // GET: Appointments
        public ActionResult Index()
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                return View(_context.Appointment.ToList());
            }

            return NotFound();
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = _context.Appointment.SingleOrDefault(m => m.ID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                return View(appointment);
            }

            return Forbid();
        }

        // GET: Appointments/Create
        public ActionResult Create(int? productId, [FromQuery] string color)
        {
            if (productId != null)
            {
                ViewBag.selectedDress = _context.Products.Where(p => p.ProductId == productId)
                    .Select(p => p.Name)
                    .ToList()
                    .First();
            }

            // Set the color parameter if any is given
            if (color != null)
                ViewBag.dressColor = color;

            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind("ID,Confirmation,DateMarried,AppointmentDateTime,Mail,Name,Phone")] Appointment appointment,
            string dressName, string dressColor)
        {
            if (appointment == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                //Prepare email data
                AppointmentMessageContainer container = new AppointmentMessageContainer
                {
                    Name = appointment.Name,
                    Recipient = appointment.Mail,
                    Date = appointment.AppointmentDateTime,
                    Garment = $"{dressName} ({dressColor})"
                };

                //Send the email
                _emailService.SendAppointmentEmail(container);
                
                _context.Appointment.Add(appointment);
                
               _context.SaveChanges();
                return RedirectToAction("Thanks");
            }
            return View(appointment);
        }

        public IActionResult Thanks()
        {
            return View();
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.SingleOrDefaultAsync(m => m.ID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                return View(appointment);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("ID,Confirmation,DateMarried,AppointmentDateTime,Mail,Name,Phone")] Appointment appointment)
        {
            if (id != appointment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.ID))
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

            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                return View(appointment);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.SingleOrDefaultAsync(m => m.ID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                return View(appointment);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.SingleOrDefaultAsync(m => m.ID == id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.ID == id);
        }
    }
}