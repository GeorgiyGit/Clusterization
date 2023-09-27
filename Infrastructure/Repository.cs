using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal ClusterizationDbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(ClusterizationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

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
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual async Task<TEntity> FindAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async Task Remove(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            Remove(entityToDelete);
        }

        public virtual void Remove(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            context.Entry(entityToUpdate).State = EntityState.Detached;
            dbSet.Attach(entityToUpdate);
            //context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
