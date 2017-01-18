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
            var applicationDbContext = _context.ColorOptions.Include(c => c.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ColorOptions/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: ColorOptions/Create
        public IActionResult Create(int? id)
        {
            int? selectedItem = 0;
            if (id != null)
            {
                selectedItem = id;
            }

            ViewData["ColorOption"] = _coloroptions.Select(r => new SelectListItem { Text = r, Value = r });
            ViewData["Name"] = new SelectList(_context.Products, "ProductId", "Name", selectedItem);
            return View();
        }

        // POST: ColorOptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColorOptionId,Color,ProductId")] ColorOption colorOption, bool again)
        {
            if (ModelState.IsValid)
            {
                _context.Add(colorOption);
                await _context.SaveChangesAsync();
                if (again)
                    return RedirectToAction("Create", "ColorOptions", new { id = colorOption.ProductId });
                return RedirectToAction("Create", "ProductImages", new { id = colorOption.ColorOptionId });
            }

            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", colorOption.ProductId);
            return View(colorOption);
        }

        // GET: ColorOptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            ViewData["ColorOption"] = _coloroptions.Select(r => new SelectListItem { Text = r, Value = r });
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", colorOption.ProductId);
            return View(colorOption);
        }

        // POST: ColorOptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ColorOptionId,Color,ProductId")] ColorOption colorOption)
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

        // GET: ColorOptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: ColorOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var colorOption = await _context.ColorOptions.SingleOrDefaultAsync(m => m.ColorOptionId == id);
            _context.ColorOptions.Remove(colorOption);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ColorOptionExists(int id)
        {
            return _context.ColorOptions.Any(e => e.ColorOptionId == id);
        }
    }
}
