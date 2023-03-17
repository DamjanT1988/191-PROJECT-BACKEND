using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _191_PROJECT_BACKEND.Data;
using _191_PROJECT_BACKEND.Models;
using Microsoft.AspNetCore.Authorization;

namespace _191_PROJECT_BACKEND.Controllers
{
    //authorize the whole class
    [Authorize]
    public class ProductAdminController : Controller
    {
        //for db connection
        private readonly ProductContext _context;

        //constructor, iniatiate db connection
        public ProductAdminController(ProductContext context)
        {
            _context = context;
        }

        // GET: ProductAdmin
        public async Task<IActionResult> Index()
        {
            //return view with list
            return View(await _context.ProductModel.ToListAsync());
        }

        // GET: ProductAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //check if null
            if (id == null || _context.ProductModel == null)
            {
                //return
                return NotFound();
            }

            //save specific product in variable by passed ID
            var productModel = await _context.ProductModel
                .FirstOrDefaultAsync(m => m.Id == id);

            //check if such product exist be ID
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // GET: ProductAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Product_title,Ean_number,Product_description,Price,Amount_storage,Expiration_date,Category,IsSwedish,Image_path,Image_file")] ProductModel productModel)
        {
            if (ModelState.IsValid)
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

                }
                else
                {
                    productModel.Image_path = null;
                }

                //add product in db
                _context.Add(productModel);
                //save changes
                await _context.SaveChangesAsync();
                //return to index page
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        // GET: ProductAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //check if null
            if (id == null || _context.ProductModel == null)
            {
                return NotFound();
            }

            //find product in db by ID
            var productModel = await _context.ProductModel.FindAsync(id);
            //if null
            if (productModel == null)
            {
                return NotFound();
            }
            //return
            return View(productModel);
        }

        // POST: ProductAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Product_title,Ean_number,Product_description,Price,Amount_storage,Expiration_date,Category,IsSwedish,Image_path")] ProductModel productModel)
        {
            //check is ID exist
            if (id != productModel.Id)
            {
                return NotFound();
            }

            //check if model is valid
            if (ModelState.IsValid)
            {
                //try to update
                try
                {
                    //update in db and save
                    _context.Update(productModel);
                    await _context.SaveChangesAsync();
                }
                //catch if no update is done
                catch (DbUpdateConcurrencyException)
                {
                    //if ID don't exist
                    if (!ProductModelExists(productModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        //throw general exception
                        throw;
                    }
                }
                //return to index page
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        // GET: ProductAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //check if null
            if (id == null || _context.ProductModel == null)
            {
                return NotFound();
            }

            //save product in variable
            var productModel = await _context.ProductModel
                .FirstOrDefaultAsync(m => m.Id == id);
            //check if exists
            if (productModel == null)
            {
                return NotFound();
            }
            //return it
            return View(productModel);
        }

        // POST: ProductAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //check if null
            if (_context.ProductModel == null)
            {
                return Problem("Entity set 'ProductContext.ProductModel'  is null.");
            }
            //save product in variable
            var productModel = await _context.ProductModel.FindAsync(id);
            //check if null
            if (productModel != null)
            {
                _context.ProductModel.Remove(productModel);
            }
            //save and return to index page
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductModelExists(int id)
        {
            return _context.ProductModel.Any(e => e.Id == id);
        }
    }
}
