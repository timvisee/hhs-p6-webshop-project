using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.NewsModels;

namespace hhs_p6_webshop_project.Controllers.NewsControllers
{
    public class NewsCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NewsCategories
        public IActionResult Index()
        {
            return View(getNewsCategoriesView());
        }

        // GET: NewsCategories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nc = getNewsCategory(id);

            if (nc == null)
            {
                return NotFound();
            }

            var nac = _context.NewsArticleCategory.Where(sc => sc.NewsCategoryID == id);
            List<NewsArticle> naObjects = new List<NewsArticle>();

            // Fill a list with article objects which contains the current category id and add them to the view object
            foreach (var naId in nac)
            {
                naObjects.Add(_context.NewsArticle.FirstOrDefault(n => n.NewsArticleID == naId.NewsArticleID));
            }

            // Get a new category view
            var ncvm = getNewsCategoriesView(nc, new SelectList(_context.NewsArticle, "NewsArticleID", "Name"));

            // Fill a list with all the newscategories/articles and add them to the view object
            ncvm.NewsCategories = new List<NewsCategory>(_context.NewsCategory);
            ncvm.NewsArticles = naObjects;

            return View(ncvm);
        }

        // GET: NewsCategories/Create
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            NewsCategory nc = new NewsCategory();
            return View(getNewsCategoriesView(nc, new SelectList(_context.NewsArticle, "NewsArticleID", "Name")));
        }

        // POST: NewsCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewsCategoryView newNewsCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newNewsCategory.NewsCategory);
                _context.SaveChanges();

                // Add articles to the category
                if (newNewsCategory.SelectedNewsArticles != null)
                {
                    foreach (int id in newNewsCategory.SelectedNewsArticles)
                    {
                        _context.NewsArticleCategory.Add(new NewsArticleCategory()
                        {
                            NewsCategoryID = newNewsCategory.NewsCategory.NewsCategoryID,
                            NewsArticleID = id
                        });
                    }
                }

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(newNewsCategory);
        }

        // GET: NewsCategories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            var nc = getNewsCategory(id);

            if (nc == null)
            {
                return NotFound();
            }

            return View(getNewsCategoriesView(nc, new SelectList(_context.NewsArticle, "NewsArticleID", "Name")));
        }

        // POST: NewsCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, NewsCategoryView updatedNewsCategory)
        {
            if (id != updatedNewsCategory.NewsCategory.NewsCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // First remove all the current couplings between this category and articles
                var rml = _context.NewsArticleCategory.Where(sc => sc.NewsCategoryID == id);
                _context.RemoveRange(rml);
                _context.SaveChanges();

                // Add all the selected articles to the current category
                updatedNewsCategory.NewsCategory.NewsArticleCategories = new List<NewsArticleCategory>();

                if (updatedNewsCategory.SelectedNewsArticles != null)
                {
                    foreach (int courseID in updatedNewsCategory.SelectedNewsArticles)
                    {
                        _context.NewsArticleCategory.Add(new NewsArticleCategory()
                        {
                            NewsCategoryID = updatedNewsCategory.NewsCategory.NewsCategoryID,
                            NewsArticleID = courseID
                        });
                    }
                }

                _context.NewsCategory.Update(updatedNewsCategory.NewsCategory);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(updatedNewsCategory.NewsCategory);
        }

        // GET: NewsCategories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            var nc = getNewsCategory(id);

            if (nc == null)
            {
                return NotFound();
            }

            return View(getNewsCategoriesView(nc, new SelectList(_context.NewsArticle, "NewsArticleID", "Name")));
        }

        // POST: NewsCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var nc = _context.NewsCategory.FirstOrDefault(s => s.NewsCategoryID == id);
            _context.NewsCategory.Remove(nc);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool NewsCategoryExists(int id)
        {
            return _context.NewsCategory.Any(e => e.NewsCategoryID == id);
        }

        private List<NewsCategoryView> getNewsCategoriesView()
        {
            List<NewsCategoryView> cvl = new List<NewsCategoryView>();
            SelectList courseList = new SelectList(_context.NewsArticle, "NewsArticleID", "Name");

            foreach (int id in _context.NewsCategory.Select(x => x.NewsCategoryID))
            {
                cvl.Add(getNewsCategoriesView(getNewsCategory(id), courseList));
            }

            return cvl;
        }

        private NewsCategoryView getNewsCategoriesView(NewsCategory nc, SelectList courseList)
        {
            NewsCategoryView ncView = new NewsCategoryView()
            {
                NewsCategory = nc,
                NewsArticleList = courseList,
                SelectedNewsArticles = nc.NewsArticleCategories.Select(sc => sc.NewsArticleID)
            };

            return ncView;
        }

        private NewsCategory getNewsCategory(int? id)
        {
            return _context.NewsCategory
                .Include(x => x.NewsArticleCategories)
                .ThenInclude(y => y.NewsArticle)
                .SingleOrDefault(m => m.NewsCategoryID == id);
        }
    }
}