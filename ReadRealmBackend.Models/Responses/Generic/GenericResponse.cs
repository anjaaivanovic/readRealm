namespace ReadRealmBackend.Models.Responses.Generic
{
    public class GenericResponse<TEntity>
    {
        public TEntity Data { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public List<string> Warnings { get; set; }
    }
}
