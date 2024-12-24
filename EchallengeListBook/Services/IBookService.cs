using EchallengeListBook.Models;

namespace EchallengeListBook.Services
{
    public interface IBookService
    {
        List<Book> GetAll();
        Book GetById(int id);
        Book GetByTitleAndId(string title, int id);

        void Add(Book book);
        void Update(Book book);
        void Delete(int id);
    }
}
