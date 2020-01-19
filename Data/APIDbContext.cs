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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //optionsBuilder.UseSqlServer("server =.; database = myDb; trusted_connection = true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //var iEntityType = typeof(IEntity);

            //var modelAssembly = iEntityType.GetTypeInfo().Assembly;

            //var types = modelAssembly.GetTypes()
            //    .Where(x => iEntityType.IsAssignableFrom(x) && x.GetTypeInfo().IsClass);

            //var method = typeof(ModelBuilder).GetMethods().First(m => m.Name == "Entity"
            //    && m.IsGenericMethodDefinition
            //    && m.GetParameters().Length == 0);

            //foreach (var type in types)
            //{
            //    method = method.MakeGenericMethod(type);
            //    method.Invoke(modelBuilder, null);
            //}

            //base.OnModelCreating(modelBuilder);
        }

    }
}
