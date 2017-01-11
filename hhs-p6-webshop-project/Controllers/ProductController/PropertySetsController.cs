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
    public class PropertySetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertySetsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: PropertySets
        public async Task<IActionResult> Index()
        {
            return View(await _context.PropertySet.ToListAsync());
        }

        // GET: PropertySets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertySet = await _context.PropertySet.SingleOrDefaultAsync(m => m.PropertySetId == id);
            if (propertySet == null)
            {
                return NotFound();
            }

            return View(propertySet);
        }

        // GET: PropertySets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PropertySets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropertySetId,Value")] PropertySet propertySet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propertySet);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(propertySet);
        }

        // GET: PropertySets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertySet = await _context.PropertySet.SingleOrDefaultAsync(m => m.PropertySetId == id);
            if (propertySet == null)
            {
                return NotFound();
            }
            return View(propertySet);
        }

        // POST: PropertySets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PropertySetId,Value")] PropertySet propertySet)
        {
            if (id != propertySet.PropertySetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertySet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertySetExists(propertySet.PropertySetId))
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
            return View(propertySet);
        }

        // GET: PropertySets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertySet = await _context.PropertySet.SingleOrDefaultAsync(m => m.PropertySetId == id);
            if (propertySet == null)
            {
                return NotFound();
            }

            return View(propertySet);
        }

        // POST: PropertySets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertySet = await _context.PropertySet.SingleOrDefaultAsync(m => m.PropertySetId == id);
            _context.PropertySet.Remove(propertySet);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PropertySetExists(int id)
        {
            return _context.PropertySet.Any(e => e.PropertySetId == id);
        }
    }
}
