
using Microsoft.EntityFrameworkCore;
using ReadRealmBackend.Models.Context;

namespace ReadRealmBackend.DAL.Base
{
    public class BaseDAL<TEntity> : IBaseDAL<TEntity> where TEntity : class
    {
        private protected readonly ReadRealmContext _context;
        private protected readonly DbSet<TEntity> _set;

        public BaseDAL(ReadRealmContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        #region SaveChanges

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Get

        public async Task<TEntity?> GetOneAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _set.CountAsync();
        }

        #endregion

        #region Insert

        public async Task InsertOneAsync(TEntity entity)
        {
            await _set.AddAsync(entity);
        }

        public async Task InsertMultipleAsync(List<TEntity> entities)
        {
            await _set.AddRangeAsync(entities);
        }

        #endregion

        #region Update

        public void UpdateOne(TEntity entity)
        {
            _set.Update(entity);
        }

        #endregion

        #region Delete

        public void DeleteOne(TEntity entity)
        {
            _set.Remove(entity);
        }

        public void DeleteMultiple(List<TEntity> entities)
        {
            _set.RemoveRange(entities);
        }

        #endregion
    }
}