using Microsoft.EntityFrameworkCore;
using System;

namespace ProductsWebAPI.Data
{
    public class APIDbContext : DbContext
    {
        private static bool created = false;

        public DbSet<Product> Products { get; set; }

        public APIDbContext(DbContextOptions options)
            : base(options)
        {
            if (!created)
            {
                Database.EnsureCreated();
                created = true;
            }
        }
    }
}
