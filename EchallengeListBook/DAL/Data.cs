using EchallengeListBook.Models;

namespace EchallengeListBook.DAL
{
    public class Data
    {
        public static List<Book> Books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Le Petit Prince",
                Author = "Antoine de Saint-Exupéry",
                Year = 1943,
                ISBN = "9781234567890",
                CopiesAvailable = 5
            },

            new Book
            {
                Id = 2,
                Title = "1984",
                Author = "George Orwell",
                Year = 1949,
                ISBN = "9780451524935",
                CopiesAvailable = 3
            },

            new Book
            {
                Id = 3,
                Title = "Les Misérables",
                Author = "Victor Hugo",
                Year = 1862,
                ISBN = "9782070414534",
                CopiesAvailable = 2
            },

            new Book
            {
                Id = 4,
                Title = "Le Rouge et le Noir",
                Author = "Stendhal",
                Year = 1830,
                ISBN = "9782070411786",
                CopiesAvailable = 0
            },

            new Book
            {
                Id = 5,
                Title = "La Peste",
                Author = "Albert Camus",
                Year = 1947,
                ISBN = "9782070408496",
                CopiesAvailable = 10
            },

            new Book
            {
                Id = 6,
                Title = "L'Étranger",
                Author = "Albert Camus",
                Year = 1942,
                ISBN = "9782253034851",
                CopiesAvailable = 7
            },

            new Book
            {
                Id = 7,
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                Year = 1960,
                ISBN = "9780061120084",
                CopiesAvailable = 15
            },

            new Book
            {
                Id = 8,
                Title = "Brave New World",
                Author = "Aldous Huxley",
                Year = 1932,
                ISBN = "9780060850524",
                CopiesAvailable = 8
            },

            new Book
            {
                Id = 9,
                Title = "Fahrenheit 451",
                Author = "Ray Bradbury",
                Year = 1953,
                ISBN = "9781451673319",
                CopiesAvailable = 4
            },

            new Book
            {
                Id = 10,
                Title = "Dune",
                Author = "Frank Herbert",
                Year = 1965,
                ISBN = "9780441013593",
                CopiesAvailable = 12
            }

        };
    }
}
