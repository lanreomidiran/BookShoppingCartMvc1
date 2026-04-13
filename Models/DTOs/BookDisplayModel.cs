namespace BookShoppingCartMvc1.Models.DTOs
{
    public class BookDisplayModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string STerm { get; set; } = "";
        public string GenreId { get; set; } = 0;

        public static implicit operator BookDisplayModel(BookDisplayModel)
        {
            throw new NotImplementedException();
        }
    }
}
