using _191_PROJECT_BACKEND.Data;
using _191_PROJECT_BACKEND.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace _191_PROJECT_BACKEND.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id, Product_title, Image_path, Image_file")] ProductModel productModel)
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

                _context.ProductModel.Add(productModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}