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

namespace hhs_p6_webshop_project.Controllers.ProductControllers
{
    public class ProductImagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string basepath = "/images/uploads/";

        public ProductImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductImages
        public async Task<IActionResult> Index()
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                var applicationDbContext = _context.ProductImages.Include(p => p.ColorOption);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }

        // GET: ProductImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var productImage = await _context.ProductImages.SingleOrDefaultAsync(m => m.ProductImageId == id);
                if (productImage == null)
                {
                    return NotFound();
                }

                return View(productImage);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: ProductImages/Create
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

                var dressPerColor = _context.ColorOptions.Join(_context.Products, c => c.ProductId, o => o.ProductId, (c, o) => new { c.ColorOptionId, c.Color, o.Name, o.ProductId }).Where(c => c.ProductId == id).ToList();
                IEnumerable<SelectListItem> selectList = from d in dressPerColor select new SelectListItem { Value = d.ColorOptionId.ToString(), Text = d.Name + " - " + d.Color };
                ViewData["ColorOptionId"] = new SelectList(selectList, "Value", "Text", selectedItem);
                return View();
            }
            else
            {
                return NotFound();
            }
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductImageId,ColorOptionId,Path")] ProductImage productImage, IFormFile image, bool again)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid || image != null)
                {

                    string filename = ChangePathName(productImage.Path);
                    FileInfo fi = new FileInfo(image.FileName);
                    string extension = fi.Extension;
                    string path = basepath + filename + extension;
                    productImage.Path = path;
                    using (FileStream fs = System.IO.File.Create("wwwroot" + path))
                    {
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
            else
            {
                return NotFound();
            }
        }

        // GET: ProductImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var productImage = await _context.ProductImages.SingleOrDefaultAsync(m => m.ProductImageId == id);
                if (productImage == null)
                {
                    return NotFound();
                }
                var dressPerColor = _context.ColorOptions.Join(_context.Products, c => c.ProductId, o => o.ProductId, (c, o) => new { c.ColorOptionId, c.Color, o.Name }).ToList();
                IEnumerable<SelectListItem> selectList = from d in dressPerColor select new SelectListItem { Value = d.ColorOptionId.ToString(), Text = d.Name + " - " + d.Color };
                ViewData["ColorOptionId"] = new SelectList(selectList, "Value", "Text");
                return View(productImage);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductImageId,ColorOptionId,Path")] ProductImage productImage, IFormFile image)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                if (id != productImage.ProductImageId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid || image != null)
                {
                    try
                    {

                        string filename = ChangePathName(productImage.Path);
                        FileInfo fi = new FileInfo(image.FileName);
                        string extension = fi.Extension;
                        string path = basepath + filename + extension;
                        productImage.Path = path;
                        using (FileStream fs = System.IO.File.Create("wwwroot" + path))
                        {
                            image.CopyTo(fs);
                            fs.Flush();
                        }

                        _context.Update(productImage);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductImageExists(productImage.ProductImageId))
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
                ViewData["ColorOptionId"] = new SelectList(_context.ColorOptions, "ColorOptionId", "Color", productImage.ColorOptionId);
                return View(productImage);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: ProductImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var productImage = await _context.ProductImages.SingleOrDefaultAsync(m => m.ProductImageId == id);
                if (productImage == null)
                {
                    return NotFound();
                }

                return View(productImage);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                var productImage = await _context.ProductImages.SingleOrDefaultAsync(m => m.ProductImageId == id);
                _context.ProductImages.Remove(productImage);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        private bool ProductImageExists(int id)
        {
            return _context.ProductImages.Any(e => e.ProductImageId == id);
        }

        public string ChangePathName(string input)
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            GuidString = GuidString.Replace("/", "");

            return GuidString;
        }

        // GET: ProductImages/GetImagePaths/5
        public JsonResult GetImagePaths(int id)
        {
            // Get all the images where the color id is the same as requested
            var colorOptions = _context.ProductImages.Where(pi => pi.ColorOptionId == id);

            // Create a list of paths
            List<string> paths = new List<string>();
            foreach(var color in colorOptions)
                paths.Add(color.Path);

            // Create a JSON result object with the paths and return it
            return new JsonResult(new {
                paths = paths
            });
        }
    }
}
