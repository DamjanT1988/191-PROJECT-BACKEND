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
    public class OrderAdminController : Controller
    {
        //for db connection
        private readonly OrderContext _context;

        //constructor, iniatiate db connection
        public OrderAdminController(OrderContext context)
        {
            _context = context;
        }

        // GET: OrderAdmin
        public async Task<IActionResult> Index()
        {
            //return view with list
            return View(await _context.OrderModel.ToListAsync());
        }

        // GET: OrderAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //check if null
            if (id == null || _context.OrderModel == null)
            {
                //return
                return NotFound();
            }

            //save specific order in variable by passed ID
            var orderModel = await _context.OrderModel
                .FirstOrDefaultAsync(m => m.Id == id);

            //check if such order exist be ID
            if (orderModel == null)
            {
                return NotFound();
            }

            //return order if found
            return View(orderModel);
        }

        // GET: OrderAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,order,email,telephone,company_name,company_org,company_adress,contact_name,status,internal_note")] OrderModel orderModel)
        {
            //check if model is valid
            if (ModelState.IsValid)
            {
                //add order
                _context.Add(orderModel);
                //save changes
                await _context.SaveChangesAsync();
                //return to index page
                return RedirectToAction(nameof(Index));
            }
            //return order
            return View(orderModel);
        }

        // GET: OrderAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //check if null
            if (id == null || _context.OrderModel == null)
            {
                return NotFound();
            }

            //find specific order
            var orderModel = await _context.OrderModel.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }

            //return
            return View(orderModel);
        }

        // POST: OrderAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,order,email,telephone,company_name,company_org,company_adress,contact_name,status,internal_note")] OrderModel orderModel)
        {
            //check if exist ID
            if (id != orderModel.Id)
            {
                return NotFound();
            }

            //check if model is valid
            if (ModelState.IsValid)
            {
                //try to update
                try
                {
                    //update db object
                    _context.Update(orderModel);
                    //save changes
                    await _context.SaveChangesAsync();
                }

                //catch a potential exception, if unsuccessful update
                catch (DbUpdateConcurrencyException)
                {
                    //check if exists
                    if (!OrderModelExists(orderModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        //exception thrown
                        throw;
                    }
                }

                //go back to index page
                return RedirectToAction(nameof(Index));
            }

            //return order
            return View(orderModel);
        }

        // GET: OrderAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //check if null
            if (id == null || _context.OrderModel == null)
            {
                return NotFound();
            }

            //save specific order in variable
            var orderModel = await _context.OrderModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            //return
            return View(orderModel);
        }

        // POST: OrderAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //check if null
            if (_context.OrderModel == null)
            {
                //return error message
                return Problem("Entity set 'OrderContext.OrderModel'  is null.");
            }

            //save order in variable
            var orderModel = await _context.OrderModel.FindAsync(id);
            
            //chech if order exist
            if (orderModel != null)
            {
                //delete order
                _context.OrderModel.Remove(orderModel);
            }

            //save changes
            await _context.SaveChangesAsync();
            //return to index
            return RedirectToAction(nameof(Index));
        }

        private bool OrderModelExists(int id)
        {
            return _context.OrderModel.Any(e => e.Id == id);
        }
    }
}
