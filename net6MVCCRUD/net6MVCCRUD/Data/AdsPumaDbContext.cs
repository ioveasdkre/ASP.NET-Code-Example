using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreConsoleAppla.Models.Data
{
    public class AdsPumaDbContext:DbContext
    {
        public AdsPumaDbContext(DbContextOptions<AdsPumaDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; } = null!;
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
    }
}
