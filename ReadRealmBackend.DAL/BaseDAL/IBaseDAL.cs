namespace ReadRealmBackend.DAL.BaseDAL
{
    public interface IBaseDAL<TEntity>
    {
        Task<TEntity?> GetOneAsync(int id);
        Task InsertOneAsync(TEntity entity);
        Task InsertMultipleAsync(List<TEntity> entities);
        void UpdateOne(TEntity entity);
        void DeleteOne(TEntity entity);
        void DeleteMultiple(List<TEntity> entities);
        Task<bool> SaveAsync();
    }
}