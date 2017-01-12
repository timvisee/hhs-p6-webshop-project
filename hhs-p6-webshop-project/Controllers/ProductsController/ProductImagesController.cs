using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.ProductModels;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace hhs_p6_webshop_project.Controllers.ProductsController {
    public class ProductImagesController : Controller {
        private readonly ApplicationDbContext _context;

        public ProductImagesController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: ProductImages
        public async Task<IActionResult> Index() {
            var applicationDbContext = _context.ProductImage.Include(p => p.ProductType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductImages/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var productImage = await _context.ProductImage.SingleOrDefaultAsync(m => m.ProductImageId == id);
            if (productImage == null) {
                return NotFound();
            }

            return View(productImage);
        }

        // GET: ProductImages/Create
        public IActionResult Create() {
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeId");
            return View();
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductImageId,Path,ProductTypeId")] ProductImage productImage, IFormFile image) {
            if (ModelState.IsValid || image != null) {


                string filename = ChangePathName(productImage.Path);
                FileInfo fi = new FileInfo(image.FileName);
                string extension = fi.Extension;
                string path = "images/uploads/" + filename + extension;
                productImage.Path = path;
                using (FileStream fs = System.IO.File.Create("wwwroot/" + path)) {
                    image.CopyTo(fs);
                    fs.Flush();
                }

                _context.Add(productImage);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeId", productImage.ProductTypeId);
            return View(productImage);
        }

        // GET: ProductImages/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var productImage = await _context.ProductImage.SingleOrDefaultAsync(m => m.ProductImageId == id);
            if (productImage == null) {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeId", productImage.ProductTypeId);
            return View(productImage);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductImageId,Path,ProductTypeId")] ProductImage productImage, IFormFile image) {
            if (id != productImage.ProductImageId) {
                return NotFound();
            }

            if (ModelState.IsValid || image != null) {
                try {

                    string filename = ChangePathName(productImage.Path);
                    FileInfo fi = new FileInfo(image.FileName);
                    string extension = fi.Extension;
                    string path = "images/uploads/" + filename + extension;
                    productImage.Path = path;
                    using (FileStream fs = System.IO.File.Create("wwwroot/" + path)) {
                        image.CopyTo(fs);
                        fs.Flush();
                    }

                    _context.Update(productImage);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!ProductImageExists(productImage.ProductImageId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ProductTypeId"] = new SelectList(_context.Set<ProductType>(), "ProductTypeId", "ProductTypeId", productImage.ProductTypeId);
            return View(productImage);
        }

        // GET: ProductImages/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var productImage = await _context.ProductImage.SingleOrDefaultAsync(m => m.ProductImageId == id);
            if (productImage == null) {
                return NotFound();
            }

            return View(productImage);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var productImage = await _context.ProductImage.SingleOrDefaultAsync(m => m.ProductImageId == id);
            _context.ProductImage.Remove(productImage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductImageExists(int id) {
            return _context.ProductImage.Any(e => e.ProductImageId == id);
        }

        public string ChangePathName(string input) {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            GuidString = GuidString.Replace("/", "");

            return GuidString;
        }
    }
}
