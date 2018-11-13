using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DAL.Context;
using EntityLayer.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly EfContext _context;
        private Microsoft.EntityFrameworkCore.DbSet<TEntity> _entities;

        public Repository(EfContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = Entities;  
  
            if (filter != null)  
            {  
                query = query.Where(filter);  
            }  
  
            foreach (var includeProperty in includeProperties.Split  
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))  
            {  
                query = query.Include(includeProperty);  
            }  
  
            if (orderBy != null)  
            {  
                return orderBy(query).ToList();  
            }  
            else  
            {  
                return query.ToList();  
            }  
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        IQueryable<TEntity> IRepository<TEntity>.GetAllNoTracking()
        {
            return Entities.AsNoTracking();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Entities.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Entities.RemoveRange(entities);
        }

        public TEntity Get(object id)
        {
            return Entities.Find(id);
        }
     
        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Attach(entity);
            _context.Entry(entity).State = (EntityState)System.Data.Entity.EntityState.Modified;
        }

        // değişiklikten sonra save etmek yeterli
        public void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
        }

        public IQueryable<TEntity> IncludeMany(params Expression<Func<TEntity, object>>[] includes)
        {
            // Extensions metot
             return _entities.IncludeMultiple(includes);
        }

        protected virtual Microsoft.EntityFrameworkCore.DbSet<TEntity> Entities => _entities ?? (_entities = _context.Set<TEntity>());

    }
}
