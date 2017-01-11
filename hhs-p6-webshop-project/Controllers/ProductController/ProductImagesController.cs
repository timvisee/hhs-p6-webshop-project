using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.ProductModels;

namespace hhs_p6_webshop_project.Controllers.ProductController
{
    [Produces("application/json")]
    [Route("api/ProductImages")]
    public class ProductImagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductImages
        [HttpGet]
        public IEnumerable<ProductImage> GetProductImage()
        {
            return _context.ProductImage;
        }

        // GET: api/ProductImages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductImage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProductImage productImage = await _context.ProductImage.SingleOrDefaultAsync(m => m.ProductImageId == id);

            if (productImage == null)
            {
                return NotFound();
            }

            return Ok(productImage);
        }

        // PUT: api/ProductImages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductImage([FromRoute] int id, [FromBody] ProductImage productImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productImage.ProductImageId)
            {
                return BadRequest();
            }

            _context.Entry(productImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductImageExists(id))
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

        // POST: api/ProductImages
        [HttpPost]
        public async Task<IActionResult> PostProductImage([FromBody] ProductImage productImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProductImage.Add(productImage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductImageExists(productImage.ProductImageId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductImage", new { id = productImage.ProductImageId }, productImage);
        }

        // DELETE: api/ProductImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProductImage productImage = await _context.ProductImage.SingleOrDefaultAsync(m => m.ProductImageId == id);
            if (productImage == null)
            {
                return NotFound();
            }

            _context.ProductImage.Remove(productImage);
            await _context.SaveChangesAsync();

            return Ok(productImage);
        }

        private bool ProductImageExists(int id)
        {
            return _context.ProductImage.Any(e => e.ProductImageId == id);
        }
    }
}