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
    public class OrderController : ControllerBase
    {
        private readonly OrderContext _context;

        public OrderController(OrderContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrderModel()
        {
            return await _context.OrderModel.ToListAsync();
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> GetOrderModel(int id)
        {
            var orderModel = await _context.OrderModel.FindAsync(id);

            if (orderModel == null)
            {
                return NotFound();
            }

            return orderModel;
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderModel(int id, OrderModel orderModel)
        {
            if (id != orderModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderModelExists(id))
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

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrderModel(OrderModel orderModel)
        {
            _context.OrderModel.Add(orderModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderModel", new { id = orderModel.Id }, orderModel);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderModel(int id)
        {
            var orderModel = await _context.OrderModel.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }

            _context.OrderModel.Remove(orderModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderModelExists(int id)
        {
            return _context.OrderModel.Any(e => e.Id == id);
        }
    }
}
