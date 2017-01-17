using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.ProductModels;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace hhs_p6_webshop_project.Controllers.ProductControllers {
    public class ProductImagesController : Controller {
        private readonly ApplicationDbContext _context;

        public ProductImagesController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: ProductImages
        public async Task<IActionResult> Index() {
            var applicationDbContext = _context.ProductImages.Include(p => p.ColorOption);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductImages/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var productImage = await _context.ProductImages.SingleOrDefaultAsync(m => m.ProductImageId == id);
            if (productImage == null) {
                return NotFound();
            }

            return View(productImage);
        }

        // GET: ProductImages/Create
        public IActionResult Create(int? id)
        {
            int? selectedItem = 0;
            if(id != null)
            {
                selectedItem = id;
            }

            var dressPerColor = _context.ColorOptions.Join(_context.Products, c => c.ProductId, o => o.ProductId, (c, o) => new { c.ColorOptionId, c.Color, o.Name }).ToList();
            IEnumerable<SelectListItem> selectList = from d in dressPerColor select new SelectListItem { Value = d.ColorOptionId.ToString(), Text = d.Name + " - " + d.Color };
            ViewData["ColorOptionId"] = new SelectList(selectList, "Value", "Text", selectedItem);
            return View();
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductImageId,ColorOptionId,Path")] ProductImage productImage, IFormFile image, bool again) {
            if (ModelState.IsValid || image != null) {

                string filename = ChangePathName(productImage.Path);
                FileInfo fi = new FileInfo(image.FileName);
                string extension = fi.Extension;
                string path = "images/dress/" + filename + extension;
                productImage.Path = path;
                using (FileStream fs = System.IO.File.Create("wwwroot/" + path)) {
                    image.CopyTo(fs);
                    fs.Flush();
                }

                _context.Add(productImage);
                await _context.SaveChangesAsync();
                if (again)
                    return RedirectToAction("Create", "ProductImages", productImage.ColorOptionId);

                return RedirectToAction("Index", "Products");
            }
            ViewData["ColorOptionId"] = new SelectList(_context.ColorOptions, "ColorOptionId", "Color", productImage.ColorOptionId);
            return View(productImage);
        }

        // GET: ProductImages/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var productImage = await _context.ProductImages.SingleOrDefaultAsync(m => m.ProductImageId == id);
            if (productImage == null) {
                return NotFound();
            }
            var dressPerColor = _context.ColorOptions.Join(_context.Products, c => c.ProductId, o => o.ProductId, (c, o) => new { c.ColorOptionId, c.Color, o.Name }).ToList();
            IEnumerable<SelectListItem> selectList = from d in dressPerColor select new SelectListItem { Value = d.ColorOptionId.ToString(), Text = d.Name + " - " + d.Color };
            ViewData["ColorOptionId"] = new SelectList(selectList, "Value", "Text");
            return View(productImage);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductImageId,ColorOptionId,Path")] ProductImage productImage, IFormFile image) {
            if (id != productImage.ProductImageId) {
                return NotFound();
            }

            if (ModelState.IsValid || image != null) {
                try {

                    string filename = ChangePathName(productImage.Path);
                    FileInfo fi = new FileInfo(image.FileName);
                    string extension = fi.Extension;
                    string path = "images/dress/" + filename + extension;
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
            ViewData["ColorOptionId"] = new SelectList(_context.ColorOptions, "ColorOptionId", "Color", productImage.ColorOptionId);
            return View(productImage);
        }

        // GET: ProductImages/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var productImage = await _context.ProductImages.SingleOrDefaultAsync(m => m.ProductImageId == id);
            if (productImage == null) {
                return NotFound();
            }

            return View(productImage);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var productImage = await _context.ProductImages.SingleOrDefaultAsync(m => m.ProductImageId == id);
            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductImageExists(int id) {
            return _context.ProductImages.Any(e => e.ProductImageId == id);
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
