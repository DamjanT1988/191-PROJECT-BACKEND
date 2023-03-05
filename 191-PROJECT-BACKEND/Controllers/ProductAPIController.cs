using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _191_PROJECT_BACKEND.Data;
using _191_PROJECT_BACKEND.Models;

namespace _191_PROJECT_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductAPIController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductModel()
        {
            return await _context.ProductModel.ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductModel(int id)
        {
            var productModel = await _context.ProductModel.FindAsync(id);

            if (productModel == null)
            {
                return NotFound();
            }

            return productModel;
        }


        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProductModel(ProductModel productModel)
        {
            
                //image or not?
                if (productModel.Image_file != null)
                {
                    //save image to wwwroot/imageupload
                    string fileName = Path.GetFileNameWithoutExtension(productModel.Image_file.FileName);
                    string extension = Path.GetExtension(productModel.Image_file.FileName);

                    //remove space and add time
                    productModel.Image_path = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssfff") + extension;

                    //path
                    string path = Path.Combine("wwwroot/imageupload/", fileName);

                    //store file
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await productModel.Image_file.CopyToAsync(fileStream);
                    }

                    //create miniatures
                    //CreateImageFiles(fileName);

                }
                else
                {
                    productModel.Image_path = null;
                }

                _context.ProductModel.Add(productModel);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetProductModel", new { id = productModel.Id }, productModel);

        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductModel(int id, ProductModel productModel)
        {
            if (id != productModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(productModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelExists(id))
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

        // DELETE: api/Product/5
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductModel(int id)
        {
            var productModel = await _context.ProductModel.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }

            _context.ProductModel.Remove(productModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductModelExists(int id)
        {
            return _context.ProductModel.Any(e => e.Id == id);
        }

        
    }
}
