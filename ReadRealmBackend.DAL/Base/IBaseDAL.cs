namespace ReadRealmBackend.DAL.Base
{
    public interface IBaseDAL<TEntity>
    {
        Task<TEntity?> GetOneAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task InsertOneAsync(TEntity entity);
        Task InsertMultipleAsync(List<TEntity> entities);
        void UpdateOne(TEntity entity);
        void DeleteOne(TEntity entity);
        void DeleteMultiple(List<TEntity> entities);
        Task<bool> SaveAsync();
    }
}