using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.BlogModels;

namespace hhs_p6_webshop_project.Controllers.BlogController {
    public class BlogArticlesController : Controller {
        private readonly ApplicationDbContext _context;

        public BlogArticlesController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: BlogArticles
        public async Task<IActionResult> Index() {
            var coupling =
                _context.BlogArticleCategory.ToList();

            return View(await _context.BlogArticle.ToListAsync());
        }

        // GET: BlogArticles/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var blogArticle = await _context.BlogArticle.SingleOrDefaultAsync(m => m.BlogArticleId == id);
            if (blogArticle == null) {
                return NotFound();
            }

            return View(blogArticle);
        }

        // GET: BlogArticles/Create
        public IActionResult Create() {
            //ViewBag["CategoryList"] = _context.BlogCategory.ToListAsync();
            var model = new BlogArticleViewModel();
            model.BlogCategories = _context.BlogCategory.ToList();
            model.BlogArticle = new BlogArticle();

            return View(model);
        }

        // POST: BlogArticles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogArticleId,ArticleText,ImagePath,Title")] BlogArticle blogArticle, List<int> CategoryList) {

            Console.WriteLine(ModelState.IsValid);


            if (ModelState.IsValid) {
                _context.Add(blogArticle);

                List<BlogCategory> categories = new List<BlogCategory>();

                foreach (int num in CategoryList) {
                    categories.Add(_context.BlogCategory.First(bc => bc.BlogCategoryId == num));
                }

                foreach (var cat in categories) {
                    var coupling = new BlogArticleCategory();
                    coupling.BlogArticle = blogArticle;
                    coupling.BlogCategory = cat;
                    _context.BlogArticleCategory.Add(coupling);
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogArticle);
        }

        // GET: BlogArticles/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var blogArticle = await _context.BlogArticle.SingleOrDefaultAsync(m => m.BlogArticleId == id);
            if (blogArticle == null) {
                return NotFound();
            }
            return View(blogArticle);
        }

        // POST: BlogArticles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogArticleId,ArticleText,ImagePath,Title")] BlogArticle blogArticle) {
            if (id != blogArticle.BlogArticleId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(blogArticle);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!BlogArticleExists(blogArticle.BlogArticleId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(blogArticle);
        }

        // GET: BlogArticles/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var blogArticle = await _context.BlogArticle.SingleOrDefaultAsync(m => m.BlogArticleId == id);
            if (blogArticle == null) {
                return NotFound();
            }

            return View(blogArticle);
        }

        // POST: BlogArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var blogArticle = await _context.BlogArticle.SingleOrDefaultAsync(m => m.BlogArticleId == id);
            _context.BlogArticle.Remove(blogArticle);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BlogArticleExists(int id) {
            return _context.BlogArticle.Any(e => e.BlogArticleId == id);
        }
    }
}
