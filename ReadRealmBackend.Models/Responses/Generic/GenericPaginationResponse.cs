namespace ReadRealmBackend.Models.Responses.Generic
{
    public class GenericPaginationResponse<TEntity>
    {
        public List<TEntity> Items { get; set; }
        public int TotalItemCount { get; set; }
    }
}