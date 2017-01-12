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

namespace hhs_p6_webshop_project.Controllers.ProductsController
{
    public class PropertyValueCouplingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _service;

        public PropertyValueCouplingsController(ApplicationDbContext context, IProductService service)
        {
            _context = context;
            _service = service;
        }

        // GET: PropertyValueCouplings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PropertyValueCouplings.Include(p => p.ProductType).Include(p => p.PropertyType).Include(p => p.PropertyValue);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PropertyValueCouplings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyValueCoupling = await _context.PropertyValueCouplings.SingleOrDefaultAsync(m => m.PropertyValue.PropertyValueId == id);
            if (propertyValueCoupling == null)
            {
                return NotFound();
            }

            return View(propertyValueCoupling);
        }

        // GET: PropertyValueCouplings/Create
        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "ProductTypeId");
            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyType, "PropertyTypeId", "DataType");
            ViewData["PropertyValueId"] = new SelectList(_context.PropertyValue, "PropertyValueId", "Value");
            return View();
        }

        // POST: PropertyValueCouplings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductTypeId,PropertyTypeId,PropertyValueId")] PropertyValueCoupling propertyValueCoupling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propertyValueCoupling);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "ProductTypeId", propertyValueCoupling.ProductTypeId);
            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyType, "PropertyTypeId", "DataType", propertyValueCoupling.PropertyTypeId);
            ViewData["PropertyValueId"] = new SelectList(_context.PropertyValue, "PropertyValueId", "Value", propertyValueCoupling.PropertyValueId);
            return View(propertyValueCoupling);
        }

        // GET: PropertyValueCouplings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pvcs =
                _context.PropertyValueCouplings
                .Include(pvc => pvc.PropertyType)
                .Include(pvc => pvc.PropertyValue)
                .Include(pvc => pvc.ProductType);

            var propertyValueCoupling = await pvcs.SingleOrDefaultAsync(m => m.PropertyValue.PropertyValueId == id);
            if (propertyValueCoupling == null)
            {
                return NotFound();
                
            }
            ViewData["ProductTypeId"] = new SelectList(_context.PropertyValueCouplings.Include(pvc => pvc.ProductType), "ProductType", "NameOverride", _context.PropertyValueCouplings.Include(pvc => pvc.ProductType));
            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyValueCouplings.Include(pvc => pvc.PropertyType), "PropertyType", "Name", propertyValueCoupling.PropertyType);
            ViewData["PropertyValueId"] = new SelectList(_context.PropertyValueCouplings.Include(pvc => pvc.PropertyValue), "PropertyValue", "Value", propertyValueCoupling.PropertyValue);
            return View(propertyValueCoupling);
        }

        // POST: PropertyValueCouplings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductTypeId,PropertyTypeId,PropertyValueId")] PropertyValueCoupling propertyValueCoupling)
        {
            if (id != propertyValueCoupling.ProductTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyValueCoupling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyValueCouplingExists(propertyValueCoupling.ProductTypeId))
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
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "ProductTypeId", propertyValueCoupling.ProductTypeId);
            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyType, "PropertyTypeId", "DataType", propertyValueCoupling.PropertyTypeId);
            ViewData["PropertyValueId"] = new SelectList(_context.PropertyValue, "PropertyValueId", "Value", propertyValueCoupling.PropertyValueId);
            return View(propertyValueCoupling);
        }

        // GET: PropertyValueCouplings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyValueCoupling = await _context.PropertyValueCouplings.SingleOrDefaultAsync(m => m.ProductTypeId == id);
            if (propertyValueCoupling == null)
            {
                return NotFound();
            }

            return View(propertyValueCoupling);
        }

        // POST: PropertyValueCouplings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertyValueCoupling = await _context.PropertyValueCouplings.SingleOrDefaultAsync(m => m.ProductTypeId == id);
            _context.PropertyValueCouplings.Remove(propertyValueCoupling);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PropertyValueCouplingExists(int id)
        {
            return _context.PropertyValueCouplings.Any(e => e.ProductTypeId == id);
        }
    }
}
