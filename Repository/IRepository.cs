using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProductsWebAPI
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(Guid? id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        
        void Remove(TEntity entity);
        void RemoveById(Guid Id);
        
        public void Update(TEntity entity);

    }
}
