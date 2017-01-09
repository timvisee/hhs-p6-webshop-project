using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.ProductModels;

namespace hhs_p6_webshop_project.Controllers.ProductController {
    public class ProductImagesController : Controller {
        private readonly ApplicationDbContext _context;

        public ProductImagesController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: ProductImages
        public async Task<IActionResult> Index() {
            return View(await _context.ProductImage.ToListAsync());
        }

        // GET: ProductImages/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var productImage = await _context.ProductImage.SingleOrDefaultAsync(m => m.ID == id);
            if (productImage == null) {
                return NotFound();
            }

            return View(productImage);
        }

        // GET: ProductImages/Create
        public IActionResult Create() {
            return View();
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Path")] ProductImage productImage) {
            if (ModelState.IsValid) {
                _context.Add(productImage);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productImage);
        }

        // GET: ProductImages/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var productImage = await _context.ProductImage.SingleOrDefaultAsync(m => m.ID == id);
            if (productImage == null) {
                return NotFound();
            }
            return View(productImage);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Path")] ProductImage productImage) {
            if (id != productImage.ID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(productImage);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!ProductImageExists(productImage.ID)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(productImage);
        }

        // GET: ProductImages/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var productImage = await _context.ProductImage.SingleOrDefaultAsync(m => m.ID == id);
            if (productImage == null) {
                return NotFound();
            }

            return View(productImage);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var productImage = await _context.ProductImage.SingleOrDefaultAsync(m => m.ID == id);
            _context.ProductImage.Remove(productImage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductImageExists(int id) {
            return _context.ProductImage.Any(e => e.ID == id);
        }
    }
}
