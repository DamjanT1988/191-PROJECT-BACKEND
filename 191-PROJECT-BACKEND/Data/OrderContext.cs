using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _191_PROJECT_BACKEND.Models;

namespace _191_PROJECT_BACKEND.Data
{
    public class OrderContext : DbContext
    {
        //data context
        public OrderContext (DbContextOptions<OrderContext> options)
            : base(options)
        {
        }

        public DbSet<_191_PROJECT_BACKEND.Models.OrderModel> OrderModel { get; set; } = default!;
    }
}
