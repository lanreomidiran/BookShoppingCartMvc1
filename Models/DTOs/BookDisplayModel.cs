namespace BookShoppingCartMvc1.Models.DTOs
{
    public class BookDisplayModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string STerm { get; set; } = "";
        public int GenreId { get; set; } = 0;

        public static implicit operator BookDisplayModel(Book book )
        {
            return new BookDisplayModel
            {
                GenreId = book.GenreId,
                // STerm = book.
            };
        }
    }
}
