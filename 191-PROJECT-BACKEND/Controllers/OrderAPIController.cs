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
    public class OrderAPIController : ControllerBase
    {
        //save db connection
        private readonly OrderContext _context;

        //construct the db connection
        public OrderAPIController(OrderContext context)
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
            //save order in variable
            var orderModel = await _context.OrderModel.FindAsync(id);

            //check if null
            if (orderModel == null)
            {
                return NotFound();
            }
            //return order
            return orderModel;
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderModel(int id, OrderModel orderModel)
        {
            //check if order exist by ID
            if (id != orderModel.Id)
            {
                return BadRequest();
            }

            //modify the order in db
            _context.Entry(orderModel).State = EntityState.Modified;

            //try to save in db
            try
            {
                await _context.SaveChangesAsync();
            }
            //catch errors
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
            //return not found
            return NoContent();
        }

        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrderModel(OrderModel orderModel)
        {
            //add order in db and save changes
            _context.OrderModel.Add(orderModel);
            await _context.SaveChangesAsync();
            //return new order
            return CreatedAtAction("GetOrderModel", new { id = orderModel.Id }, orderModel);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderModel(int id)
        {
            //save order in variable
            var orderModel = await _context.OrderModel.FindAsync(id);
            //check if null
            if (orderModel == null)
            {
                return NotFound();
            }
            //remove order from db and save
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
