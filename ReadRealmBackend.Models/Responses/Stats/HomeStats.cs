namespace ReadRealmBackend.Models.Responses.Home
{
    public class HomeStats
    {
        public int TotalBooksRead { get; set; }
        public int TotalBooksRated { get; set; }
        public string FavoriteGenre { get; set; }
        public decimal AverageRating { get; set; }
    }
}