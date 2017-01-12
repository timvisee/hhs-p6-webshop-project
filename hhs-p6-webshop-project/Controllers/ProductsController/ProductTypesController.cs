using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.ProductModels;

namespace hhs_p6_webshop_project.Controllers.ProductsController
{
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductTypesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ProductTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductType.Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType.SingleOrDefaultAsync(m => m.ProductTypeId == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: ProductTypes/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name");
            return View();
        }

        // POST: ProductTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductTypeId,DescriptionOverride,NameOverride,ProductId")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productType.ProductId);
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType.SingleOrDefaultAsync(m => m.ProductTypeId == id);
            if (productType == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productType.ProductId);
            return View(productType);
        }

        // POST: ProductTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductTypeId,DescriptionOverride,NameOverride,ProductId")] ProductType productType)
        {
            if (id != productType.ProductTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(productType.ProductTypeId))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Name", productType.ProductId);
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType.SingleOrDefaultAsync(m => m.ProductTypeId == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productType = await _context.ProductType.SingleOrDefaultAsync(m => m.ProductTypeId == id);
            _context.ProductType.Remove(productType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductTypeExists(int id)
        {
            return _context.ProductType.Any(e => e.ProductTypeId == id);
        }
    }
}
