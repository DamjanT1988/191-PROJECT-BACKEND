﻿using System;
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
    public class OrderAdminController : Controller
    {
        private readonly OrderContext _context;

        public OrderAdminController(OrderContext context)
        {
            _context = context;
        }

        // GET: OrderAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderModel.ToListAsync());
        }

        // GET: OrderAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderModel == null)
            {
                return NotFound();
            }

            var orderModel = await _context.OrderModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        // GET: OrderAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,order,email,telephone,company_name,company_org,company_adress,contact_name,status,internal_note")] OrderModel orderModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderModel);
        }

        // GET: OrderAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderModel == null)
            {
                return NotFound();
            }

            var orderModel = await _context.OrderModel.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }
            return View(orderModel);
        }

        // POST: OrderAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,order,email,telephone,company_name,company_org,company_adress,contact_name,status,internal_note")] OrderModel orderModel)
        {
            if (id != orderModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderModelExists(orderModel.Id))
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
            return View(orderModel);
        }

        // GET: OrderAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderModel == null)
            {
                return NotFound();
            }

            var orderModel = await _context.OrderModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        // POST: OrderAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderModel == null)
            {
                return Problem("Entity set 'OrderContext.OrderModel'  is null.");
            }
            var orderModel = await _context.OrderModel.FindAsync(id);
            if (orderModel != null)
            {
                _context.OrderModel.Remove(orderModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderModelExists(int id)
        {
            return _context.OrderModel.Any(e => e.Id == id);
        }
    }
}
