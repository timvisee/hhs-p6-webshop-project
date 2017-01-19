using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.NewsModels;
using Microsoft.AspNetCore.Http;

namespace hhs_p6_webshop_project.Controllers.NewsControllers
{
    public class NewsArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NewsArticles
        public async Task<IActionResult> Index()
        {
            return View(getNewsArticlesVM());
        }

        // GET: NewsArticles/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = getNewsArticle(id);

            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(getNewsArticlesVM(newsArticle, new SelectList(_context.NewsCategory, "NewsCategoryID", "Name")));
        }

        // GET: NewsArticles/Create
        public IActionResult Create()
        {
            NewsArticle na = new NewsArticle();
            return View(getNewsArticlesVM(na, new SelectList(_context.NewsCategory, "NewsCategoryID", "Name")));
        }


        // POST: NewsArticles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewsArticleVM newNewsArticle, IFormFile image)
        {
            if (ModelState.IsValid || image != null)
            {
                string filename = ChangePathName(newNewsArticle.NewsArticle.ImagePath);
                FileInfo fi = new FileInfo(image.FileName);
                string extension = fi.Extension;
                string path = "/images/uploads/" + filename + extension;
                newNewsArticle.NewsArticle.ImagePath = path;
                using (FileStream fs = System.IO.File.Create("wwwroot/" + path))
                {
                    image.CopyTo(fs);
                    fs.Flush();
                }

                _context.Add(newNewsArticle.NewsArticle);
                _context.SaveChanges();

                if (newNewsArticle.SelectedNewsCategories != null)
                {
                    foreach (int id in newNewsArticle.SelectedNewsCategories)
                    {
                        _context.NewsArticleCategory.Add(new NewsArticleCategory() { NewsArticleID = newNewsArticle.NewsArticle.NewsArticleID, NewsCategoryID = id });
                    }
                }

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(newNewsArticle);
        }

        // GET: NewsArticles/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = getNewsArticle(id);

            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(getNewsArticlesVM(newsArticle, new SelectList(_context.NewsCategory, "NewsCategoryID", "Name")));
        }

        // POST: NewsArticles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, NewsArticleVM updatedNewsArticle, IFormFile image)
        {
            if (id != updatedNewsArticle.NewsArticle.NewsArticleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var rml = _context.NewsArticleCategory.Where(sc => sc.NewsArticleID == id);
                _context.RemoveRange(rml);
                _context.SaveChanges();

                updatedNewsArticle.NewsArticle.NewsArticleCategories = new List<NewsArticleCategory>();

                if (updatedNewsArticle.SelectedNewsCategories != null)
                {
                    foreach (int newsCategoryID in updatedNewsArticle.SelectedNewsCategories)
                    {
                        _context.NewsArticleCategory.Add(new NewsArticleCategory() { NewsArticleID = updatedNewsArticle.NewsArticle.NewsArticleID, NewsCategoryID = newsCategoryID });
                    }
                }

                if (image != null)
                {
                    string filename = ChangePathName(updatedNewsArticle.NewsArticle.ImagePath);
                    FileInfo fi = new FileInfo(image.FileName);
                    string extension = fi.Extension;
                    string path = "/images/uploads/" + filename + extension;
                    updatedNewsArticle.NewsArticle.ImagePath = path;
                    using (FileStream fs = System.IO.File.Create("wwwroot/" + path))
                    {
                        image.CopyTo(fs);
                        fs.Flush();
                    }
                }

                _context.NewsArticle.Update(updatedNewsArticle.NewsArticle);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(updatedNewsArticle.NewsArticle);
        }

        // GET: NewsArticles/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var na = getNewsArticle(id);

            if (na == null)
            {
                return NotFound();
            }

            return View(getNewsArticlesVM(na, new SelectList(_context.NewsCategory, "NewsCategoryID", "Name")));
        }

        // POST: NewsArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var na = _context.NewsArticle.FirstOrDefault(s => s.NewsArticleID == id);
            _context.NewsArticle.Remove(na);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool NewsArticleExists(int id)
        {
            return _context.NewsArticle.Any(e => e.NewsArticleID == id);
        }


        private List<NewsArticleVM> getNewsArticlesVM()
        {
            List<NewsArticleVM> lijst = new List<NewsArticleVM>();
            SelectList categoryList = new SelectList(_context.NewsCategory, "NewsCategoryID", "Name");

            foreach (int id in _context.NewsArticle.Select(x => x.NewsArticleID))
            {
                lijst.Add(getNewsArticlesVM(getNewsArticle(id), categoryList));
            }

            return lijst;
        }

        private NewsArticleVM getNewsArticlesVM(NewsArticle newsArticle, SelectList newsCategoriesList)
        {
            NewsArticleVM NewsArticleVM = new NewsArticleVM()
            {
                NewsArticle = newsArticle,
                NewsCategoriesList = newsCategoriesList,
                SelectedNewsCategories = newsArticle.NewsArticleCategories.Select(sc => sc.NewsCategoryID)
            };

            return NewsArticleVM;
        }

        private NewsArticle getNewsArticle(int? id)
        {
            return _context.NewsArticle
                .Include(x => x.NewsArticleCategories)
                .ThenInclude(y => y.NewsCategory)
                .SingleOrDefault(m => m.NewsArticleID == id);
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
    }

}
