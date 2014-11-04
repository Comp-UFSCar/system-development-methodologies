using Gamedalf.Core.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Gamedalf.Services.Infrastructure
{
    public abstract class Service<TEntity>
        where TEntity : class
    {
        #region Protected Fields
        protected readonly ApplicationDbContext Db;
        #endregion Protected Fields

        #region Constructor
        protected Service(ApplicationDbContext db)
        {
            Db = db;
        }
        #endregion Constructor

        public virtual async Task<ICollection<TEntity>> All()
        {
            return await Db.Set<TEntity>().ToListAsync();
        }
        public virtual async Task<TEntity> Find(params object[] keyValues)
        {
            return await Db.Set<TEntity>().FindAsync(keyValues);
        }
        public virtual async Task<TEntity> Add(TEntity entity)
        {
            Db.Set<TEntity>().Add(entity);
            await Db.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<int> AddRange(IEnumerable<TEntity> entities)
        {
            Db.Set<TEntity>().AddRange(entities);
            return await Db.SaveChangesAsync();
        }
        public virtual async Task<int> Update(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            return await Db.SaveChangesAsync();
        }
        public virtual async Task<int> Delete(params object[] keyValues)
        {
            var entity = await Find(keyValues);
            return await Delete(entity);
        }
        public virtual async Task<int> Delete(TEntity entity)
        {
            Db.Set<TEntity>().Remove(entity);
            return await Db.SaveChangesAsync();
        }
        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
