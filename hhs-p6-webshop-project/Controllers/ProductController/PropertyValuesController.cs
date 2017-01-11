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
    public class PropertyValuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyValuesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: PropertyValues
        public async Task<IActionResult> Index()
        {
            return View(await _context.PropertyValue.ToListAsync());
        }

        // GET: PropertyValues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyValue = await _context.PropertyValue.SingleOrDefaultAsync(m => m.PropertyValueId == id);
            if (propertyValue == null)
            {
                return NotFound();
            }

            return View(propertyValue);
        }

        // GET: PropertyValues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PropertyValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropertyValueId,PropertyTypeProductId,Value")] PropertyValue propertyValue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propertyValue);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(propertyValue);
        }

        // GET: PropertyValues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyValue = await _context.PropertyValue.SingleOrDefaultAsync(m => m.PropertyValueId == id);
            if (propertyValue == null)
            {
                return NotFound();
            }
            return View(propertyValue);
        }

        // POST: PropertyValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PropertyValueId,PropertyTypeProductId,Value")] PropertyValue propertyValue)
        {
            if (id != propertyValue.PropertyValueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyValue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyValueExists(propertyValue.PropertyValueId))
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
            return View(propertyValue);
        }

        // GET: PropertyValues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyValue = await _context.PropertyValue.SingleOrDefaultAsync(m => m.PropertyValueId == id);
            if (propertyValue == null)
            {
                return NotFound();
            }

            return View(propertyValue);
        }

        // POST: PropertyValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertyValue = await _context.PropertyValue.SingleOrDefaultAsync(m => m.PropertyValueId == id);
            _context.PropertyValue.Remove(propertyValue);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PropertyValueExists(int id)
        {
            return _context.PropertyValue.Any(e => e.PropertyValueId == id);
        }
    }
}
