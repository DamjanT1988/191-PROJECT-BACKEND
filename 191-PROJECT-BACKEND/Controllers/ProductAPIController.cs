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
    //route to controller and identify
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        //save db connection
        private readonly ProductContext _context;

        //construct the db connection
        public ProductAPIController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductModel()
        {
            //save list im variable
            var productModels = await _context.ProductModel.ToListAsync();

            //check if null
            if (productModels == null)
            {
                return NotFound();
            }

            //loop through each product
            foreach (var productModel in productModels)
            {
                //construct and save image path
                var imagePath = Path.Combine("wwwroot", "imageupload", productModel.Image_path);

                //check if file exist by image path
                if (System.IO.File.Exists(imagePath))
                {
                    //save the file data in stream variable
                    using var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                    //save file stream in byte to product property Image_data
                    productModel.Image_data = new byte[stream.Length];
                    //wait for read
                    await stream.ReadAsync(productModel.Image_data, 0, (int)stream.Length);
                }
            }
            //return
            return productModels;
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductModel(int id)
        {
            //save prodcut in variable
            var productModel = await _context.ProductModel.FindAsync(id);

            //check if null
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
            
                //check if nukk
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

                }
                else
                {
                    productModel.Image_path = null;
                }
                //add to products in db
                _context.ProductModel.Add(productModel);
                //save changes
                await _context.SaveChangesAsync();
                //return new
                return CreatedAtAction("GetProductModel", new { id = productModel.Id }, productModel);

        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductModel(int id, ProductModel productModel)
        {
            //check if exist
            if (id != productModel.Id)
            {
                return BadRequest();
            }

            //modify state
            _context.Entry(productModel).State = EntityState.Modified;

            //try to save changes
            try
            {
                await _context.SaveChangesAsync();
            }
            //catch errors
            catch (DbUpdateConcurrencyException)
            {
                //ID does not exist
                if (!ProductModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    //throw general exception
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Product/5
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductModel(int id)
        {
            //save product in variable from db
            var productModel = await _context.ProductModel.FindAsync(id);
            
            //check if null
            if (productModel == null)
            {
                return NotFound();
            }

            //remove product in db and save changes
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
