using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity Get(object id);  
        IEnumerable<TEntity> GetAll();  
        IQueryable<TEntity> GetAllNoTracking();  
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);  
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);  

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);

        IQueryable<TEntity> IncludeMany(params Expression<Func<TEntity, object>>[] includes);
    }
}
