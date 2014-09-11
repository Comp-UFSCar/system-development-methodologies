using Gamedalf.Core.Data;
using Gamedalf.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Gamedalf.Services
{
    public abstract class Service<TEntity>
        where TEntity : class
    {
        #region Protected Fields
        protected readonly ApplicationDbContext db;
        #endregion Protected Fields

        #region Constructor
        protected Service(ApplicationDbContext _db)
        {
            db = _db;
        }
        #endregion Constructor

        public virtual async Task<ICollection<TEntity>> All()
        {
            return await db.Set<TEntity>().ToListAsync();
        }
        public virtual async Task<TEntity> Find(params object[] keyValues)
        {
            return await db.Set<TEntity>().FindAsync(keyValues);
        }
        public virtual async Task<TEntity> Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<int> AddRange(IEnumerable<TEntity> entities)
        {
            db.Set<TEntity>().AddRange(entities);
            return await db.SaveChangesAsync();
        }
        public virtual async Task<int> Update(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return await db.SaveChangesAsync();
        }
        public virtual async Task<int> Delete(params object[] keyValues)
        {
            var entity = await Find(keyValues);
            return await Delete(entity);
        }
        public virtual async Task<int> Delete(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
            return await db.SaveChangesAsync();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
