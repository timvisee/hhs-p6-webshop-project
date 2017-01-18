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
            return View(getCategoriesVM());
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

            return View(getNewsCategoryVM(nc, new SelectList(_context.NewsArticle, "NewsArticleID", "Name")));
        }

        // GET: NewsCategories/Create
        public IActionResult Create()
        {
            NewsCategory nc = new NewsCategory();
            return View(getNewsCategoryVM(nc, new SelectList(_context.NewsArticle, "NewsArticleID", "Name")));
        }

        // POST: NewsCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewsCategoryVM newNewsCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newNewsCategory.NewsCategory);
                _context.SaveChanges();

                if (newNewsCategory.SelectedNewsArticles != null)
                {
                    foreach (int id in newNewsCategory.SelectedNewsArticles)
                    {
                        _context.NewsArticleCategory.Add(new NewsArticleCategory() { NewsCategoryID = newNewsCategory.NewsCategory.NewsCategoryID, NewsArticleID = id });
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
            if (id == null)
            {
                return NotFound();
            }

            var nc = getNewsCategory(id);

            if (nc == null)
            {
                return NotFound();
            }

            return View(getNewsCategoryVM(nc, new SelectList(_context.NewsArticle, "NewsArticleID", "Name")));
        }

        // POST: NewsCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, NewsCategoryVM updatedNewsCategory)
        {
            if (id != updatedNewsCategory.NewsCategory.NewsCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var teVerwijderenLijst = _context.NewsArticleCategory.Where(sc => sc.NewsCategoryID == id);
                _context.RemoveRange(teVerwijderenLijst);
                _context.SaveChanges();

                updatedNewsCategory.NewsCategory.NewsArticleCategories = new List<NewsArticleCategory>();

                if (updatedNewsCategory.SelectedNewsArticles != null)
                {
                    foreach (int courseID in updatedNewsCategory.SelectedNewsArticles)
                    {
                        _context.NewsArticleCategory.Add(new NewsArticleCategory() { NewsCategoryID = updatedNewsCategory.NewsCategory.NewsCategoryID, NewsArticleID = courseID });
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
            if (id == null)
            {
                return NotFound();
            }

            var nc = getNewsCategory(id);

            if (nc == null)
            {
                return NotFound();
            }

            return View(getNewsCategoryVM(nc, new SelectList(_context.NewsArticle, "NewsArticleID", "Name")));
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

        private List<NewsCategoryVM> getCategoriesVM()
        {
            List<NewsCategoryVM> lijst = new List<NewsCategoryVM>();
            SelectList courseList = new SelectList(_context.NewsArticle, "NewsArticleID", "Name");

            foreach (int id in _context.NewsCategory.Select(x => x.NewsCategoryID))
            {
                lijst.Add(getNewsCategoryVM(getNewsCategory(id), courseList));
            }

            return lijst;
        }

        private NewsCategoryVM getNewsCategoryVM(NewsCategory nc, SelectList courseList)
        {
            NewsCategoryVM ncVM = new NewsCategoryVM()
            {
                NewsCategory = nc,
                NewsArticleList = courseList,
                SelectedNewsArticles = nc.NewsArticleCategories.Select(sc => sc.NewsArticleID)
            };

            return ncVM;
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
