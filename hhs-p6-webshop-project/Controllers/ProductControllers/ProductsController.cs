using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.ProductModels;

using hhs_p6_webshop_project.Services;

namespace hhs_p6_webshop_project.Controllers.ProductControllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _service;

        public ProductsController(ApplicationDbContext context, IProductService service)
        {
            _context = context;
            _service = service;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            ProductView pv = new ProductView();

            pv.Products = _service.GetAllProducts();

            pv.Filters = _service.GetAllFilters();

            return View(pv);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _service.GetAllProducts().FirstOrDefault(pr => pr.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else {
                return NotFound();
            }
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Description,Name,Price")] Product product, bool again)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    if (again)
                        return RedirectToAction("Create");
                    return RedirectToAction("Create", "ColorOptions", new { id = product.ProductId });
                }
                return View(product);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Products.SingleOrDefaultAsync(m => m.ProductId == id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Description,Name,Price")] Product product)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                if (id != product.ProductId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(product);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(product.ProductId))
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
                return View(product);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Products.SingleOrDefaultAsync(m => m.ProductId == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check if user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                var product = await _context.Products.SingleOrDefaultAsync(m => m.ProductId == id);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
