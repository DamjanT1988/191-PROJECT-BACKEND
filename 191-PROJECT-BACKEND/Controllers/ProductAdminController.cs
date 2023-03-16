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
    [Authorize]
    public class ProductAdminController : Controller
    {
        private readonly ProductContext _context;
        
        public ProductAdminController(ProductContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductModel.ToListAsync());
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductModel == null)
            {
                return NotFound();
            }

            var productModel = await _context.ProductModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

                    //create miniatures
                    //CreateImageFiles(fileName);

                }
                else
                {
                    productModel.Image_path = null;
                }

                _context.Add(productModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductModel == null)
            {
                return NotFound();
            }

            var productModel = await _context.ProductModel.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }
            return View(productModel);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Product_title,Ean_number,Product_description,Price,Amount_storage,Expiration_date,Category,IsSwedish,Image_path")] ProductModel productModel)
        {
            if (id != productModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductModelExists(productModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductModel == null)
            {
                return NotFound();
            }

            var productModel = await _context.ProductModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductModel == null)
            {
                return Problem("Entity set 'ProductContext.ProductModel'  is null.");
            }
            var productModel = await _context.ProductModel.FindAsync(id);
            if (productModel != null)
            {
                _context.ProductModel.Remove(productModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductModelExists(int id)
        {
            return _context.ProductModel.Any(e => e.Id == id);
        }
    }
}
