using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.ProductModels;

namespace hhs_p6_webshop_project.Controllers.ProductControllers
{
    public class ColorOptionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string[] _coloroptions = { "Wit", "Ivoor", "Roze", "Rood", "Grijs", "Zwart" };

        public ColorOptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ColorOptions
        public async Task<IActionResult> Index()
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                var applicationDbContext = _context.ColorOptions.Include(c => c.Product);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }

        // GET: ColorOptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var colorOption = await _context.ColorOptions.SingleOrDefaultAsync(m => m.ColorOptionId == id);
                if (colorOption == null)
                {
                    return NotFound();
                }

                return View(colorOption);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: ColorOptions/Create
        public IActionResult Create(int? id)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                int? selectedItem = 0;
                if (id != null)
                {
                    selectedItem = id;
                }
                string[] coloroptions = { "Wit", "Ivoor", "Roze", "Rood", "Grijs", "Zwart" };

                ViewData["ColorOption"] = coloroptions.Select(r => new SelectListItem { Text = r, Value = r });
                var dress = _context.Products.Where(c => c.ProductId == id).Select(o => new { Value = o.ProductId, Text = o.Name }).ToList();
                ViewData["Name"] = new SelectList(dress, "Value", "Text", selectedItem);
                return View();
            }
            else
            {
                return NotFound();
            }
        }

        // POST: ColorOptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColorOptionId,Color,ProductId")] ColorOption colorOption, bool again)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(colorOption);
                    await _context.SaveChangesAsync();
                    if (again)
                        return RedirectToAction("Create", "ColorOptions", new { id = colorOption.ProductId });
                    return RedirectToAction("Create", "ProductImages", new { id = colorOption.ProductId });
                }
                ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", colorOption.ProductId);
                return View(colorOption);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: ColorOptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var colorOption = await _context.ColorOptions.SingleOrDefaultAsync(m => m.ColorOptionId == id);
                if (colorOption == null)
                {
                    return NotFound();
                }

                string[] coloroptions = { "Wit", "Ivoor", "Roze", "Rood", "Grijs", "Zwart" };
                ViewData["ColorOption"] = coloroptions.Select(r => new SelectListItem { Text = r, Value = r });
                ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", colorOption.ProductId);
                return View(colorOption);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: ColorOptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ColorOptionId,Color,ProductId")] ColorOption colorOption)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                if (id != colorOption.ColorOptionId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(colorOption);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ColorOptionExists(colorOption.ColorOptionId))
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
                ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", colorOption.ProductId);
                return View(colorOption);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: ColorOptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var colorOption = await _context.ColorOptions.SingleOrDefaultAsync(m => m.ColorOptionId == id);
                if (colorOption == null)
                {
                    return NotFound();
                }

                return View(colorOption);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: ColorOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                var colorOption = await _context.ColorOptions.SingleOrDefaultAsync(m => m.ColorOptionId == id);
                _context.ColorOptions.Remove(colorOption);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        private bool ColorOptionExists(int id)
        {
            return _context.ColorOptions.Any(e => e.ColorOptionId == id);
        }
    }
}
