using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.BlogModels;

namespace hhs_p6_webshop_project.Controllers.BlogController
{
    [Produces("application/json")]
    [Route("api/BlogArticleCategories")]
    public class BlogArticleCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogArticleCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BlogArticleCategories
        [HttpGet]
        public IEnumerable<BlogArticleCategory> GetBlogArticleCategory()
        {
            return _context.BlogArticleCategory;
        }

        // GET: api/BlogArticleCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogArticleCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BlogArticleCategory blogArticleCategory = await _context.BlogArticleCategory.SingleOrDefaultAsync(m => m.BlogArticleId == id);

            if (blogArticleCategory == null)
            {
                return NotFound();
            }

            return Ok(blogArticleCategory);
        }

        // PUT: api/BlogArticleCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogArticleCategory([FromRoute] int id, [FromBody] BlogArticleCategory blogArticleCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != blogArticleCategory.BlogArticleId)
            {
                return BadRequest();
            }

            _context.Entry(blogArticleCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogArticleCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BlogArticleCategories
        [HttpPost]
        public async Task<IActionResult> PostBlogArticleCategory([FromBody] BlogArticleCategory blogArticleCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BlogArticleCategory.Add(blogArticleCategory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BlogArticleCategoryExists(blogArticleCategory.BlogArticleId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBlogArticleCategory", new { id = blogArticleCategory.BlogArticleId }, blogArticleCategory);
        }

        // DELETE: api/BlogArticleCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogArticleCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BlogArticleCategory blogArticleCategory = await _context.BlogArticleCategory.SingleOrDefaultAsync(m => m.BlogArticleId == id);
            if (blogArticleCategory == null)
            {
                return NotFound();
            }

            _context.BlogArticleCategory.Remove(blogArticleCategory);
            await _context.SaveChangesAsync();

            return Ok(blogArticleCategory);
        }

        private bool BlogArticleCategoryExists(int id)
        {
            return _context.BlogArticleCategory.Any(e => e.BlogArticleId == id);
        }
    }
}