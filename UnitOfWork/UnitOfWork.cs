using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductsWebAPI.Data;

namespace ProductsWebAPI
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly APIDbContext context;
        private IRepository<Product> productRepository;

        public IRepository<Product> Products
        {
            get
            {
                return productRepository ??= new Repository<Product>(context);
            }
        }

        public UnitOfWork(APIDbContext dbContext)
        {
            this.context = dbContext;
        }

        public void Complete()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
