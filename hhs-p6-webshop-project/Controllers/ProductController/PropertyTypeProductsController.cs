using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.ProductModels;

namespace hhs_p6_webshop_project.Controllers.ProductController
{
    public class PropertyTypeProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyTypeProductsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: PropertyTypeProducts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PropertyTypeProducts.Include(p => p.PropertyValue);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PropertyTypeProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyTypeProduct = await _context.PropertyTypeProducts.SingleOrDefaultAsync(m => m.ProductId == id);
            if (propertyTypeProduct == null)
            {
                return NotFound();
            }

            return View(propertyTypeProduct);
        }

        // GET: PropertyTypeProducts/Create
        public IActionResult Create()
        {
            ViewData["PropertyValueId"] = new SelectList(_context.PropertyValue, "PropertyValueId", "Value");
            return View();
        }

        // POST: PropertyTypeProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,PropertyTypeId,PropertyValueId")] PropertyTypeProduct propertyTypeProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propertyTypeProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PropertyValueId"] = new SelectList(_context.PropertyValue, "PropertyValueId", "Value", propertyTypeProduct.PropertyValueId);
            return View(propertyTypeProduct);
        }

        // GET: PropertyTypeProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyTypeProduct = await _context.PropertyTypeProducts.SingleOrDefaultAsync(m => m.ProductId == id);
            if (propertyTypeProduct == null)
            {
                return NotFound();
            }
            ViewData["PropertyValueId"] = new SelectList(_context.PropertyValue, "PropertyValueId", "Value", propertyTypeProduct.PropertyValueId);
            return View(propertyTypeProduct);
        }

        // POST: PropertyTypeProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,PropertyTypeId,PropertyValueId")] PropertyTypeProduct propertyTypeProduct)
        {
            if (id != propertyTypeProduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyTypeProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyTypeProductExists(propertyTypeProduct.ProductId))
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
            ViewData["PropertyValueId"] = new SelectList(_context.PropertyValue, "PropertyValueId", "Value", propertyTypeProduct.PropertyValueId);
            return View(propertyTypeProduct);
        }

        // GET: PropertyTypeProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyTypeProduct = await _context.PropertyTypeProducts.SingleOrDefaultAsync(m => m.ProductId == id);
            if (propertyTypeProduct == null)
            {
                return NotFound();
            }

            return View(propertyTypeProduct);
        }

        // POST: PropertyTypeProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertyTypeProduct = await _context.PropertyTypeProducts.SingleOrDefaultAsync(m => m.ProductId == id);
            _context.PropertyTypeProducts.Remove(propertyTypeProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PropertyTypeProductExists(int id)
        {
            return _context.PropertyTypeProducts.Any(e => e.ProductId == id);
        }
    }
}
