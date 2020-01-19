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
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        public void Complete();
    }
}
