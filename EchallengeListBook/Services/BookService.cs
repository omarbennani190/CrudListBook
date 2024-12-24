using EchallengeListBook.Models;

namespace EchallengeListBook.Services
{
    public class BookService : IBookService
    {
        private List<Book> Books;
        public BookService()
        {
            this.Books = new List<Book>();
        }

        public List<Book> GetAll() => this.Books;

        public Book GetById(int id) => this.Books.FirstOrDefault(b => b.Id == id);

        public Book GetByTitleAndId(string title, int id)
        {
            return this.Books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && b.Id == id); //comparaison du titre est insensible à la casse
        }

        public void Add(Book book)
        {
            if (this.Books.Any(b => b.ISBN == book.ISBN))
            {
                throw new Exception("Un livre avec le même ISBN existe déjà.");
            }
            this.Books.Add(book);
        }

        public void Update(Book book)
        {
            var existing = GetById(book.Id);
            if (existing != null)
            {
                existing.Title = book.Title;
                existing.Author = book.Author;
                existing.Year = book.Year;
                existing.ISBN = book.ISBN;
                existing.CopiesAvailable = book.CopiesAvailable;
            }
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null) this.Books.Remove(book);
        }
    }
}
