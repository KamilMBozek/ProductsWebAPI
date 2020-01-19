using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductsWebAPI.Data;

namespace ProductsWebAPI
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext dataContext;

        public Repository(DbContext context)
        {
            this.dataContext = context;
        }

        public TEntity Get(Guid? id)
        {
            return dataContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dataContext.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return dataContext.Set<TEntity>().Where(predicate);
        }

        public void Add(TEntity entity)
        {
            dataContext.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            dataContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveById(Guid Id)
        {
            dataContext.Set<TEntity>().Remove(Get(Id));
        }

        public void Update(TEntity entity)
        {
            dataContext.Set<TEntity>().Update(entity);
        }
    }
}
