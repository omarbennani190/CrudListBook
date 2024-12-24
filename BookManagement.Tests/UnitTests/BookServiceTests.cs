using EchallengeListBook.Models;
using EchallengeListBook.Services;

namespace BookManagement.Tests.UnitTests
{
    public class BookServiceTests
    {
        private readonly IBookService _service;

        public BookServiceTests()
        {
            _service = new BookService();
        }

        //GetAll
        [Fact]
        public void GetAll_ShouldReturnEmptyList_WhenNoBooksAreAdded()
        {
            var result = _service.GetAll();
            Assert.Empty(result);
        }

        [Fact]
        public void GetAll_ShouldReturnBooks_WhenBooksAreAdded()
        {
            // Création de plusieurs livres
            Book book1 = new Book { Title = "C# Basics", Author = "John Doe", Year = 2023, ISBN = "1234567890123", CopiesAvailable = 3 };
            Book book2 = new Book { Title = "Advanced C#", Author = "Jane Smith", Year = 2024, ISBN = "9876543210987", CopiesAvailable = 5 };

            // Ajout des livres au service
            _service.Add(book1);
            _service.Add(book2);

            // Récupération de tous les livres
            var result = _service.GetAll();

            // Vérification du nombre de livres ajoutés
            Assert.Equal(2, result.Count);

            // Vérification des titres des livres
            Assert.Equal("C# Basics", result[0].Title);
            Assert.Equal("Advanced C#", result[1].Title);
        }

        //GetById
        [Fact]
        public void GetById_ShouldReturnBook_WhenBookExists()
        {
            Book book = new Book { Title = "C# Basics", Author = "John Doe", Year = 2023, ISBN = "1234567890123", CopiesAvailable = 3 };
            _service.Add(book);

            var result = _service.GetById(0);

            Assert.NotNull(result);
            Assert.Equal("C# Basics", result.Title); // verifier titre
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            var result = _service.GetById(10);

            Assert.Null(result);
        }

        // GetByTitleAndId
        [Fact]
        public void GetByTitleAndId_ShouldReturnBook_WhenBookExistsWithMatchingTitleAndId()
        {
            Book book = new Book{ Title = "C# Basics", Author = "John Doe", Year = 2023, ISBN = "1234567890123", CopiesAvailable = 3};

            // appliquer ajout et la recherche
            _service.Add(book);
            var result = _service.GetByTitleAndId("C# Basics", book.Id);

            // tester
            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Title, result.Title);
        }

        [Fact]
        public void GetByTitleAndId_ShouldReturnNull_WhenBookDoesNotExistWithMatchingTitleAndId()
        {
            Book book = new Book { Title = "C# Basics", Author = "John Doe", Year = 2023, ISBN = "1234567890123", CopiesAvailable = 3 };
            _service.Add(book);

            var result = _service.GetByTitleAndId("Nonexistent Title", 1);

            Assert.Null(result);
        }

        [Fact]
        public void GetByTitleAndId_ShouldReturnNull_WhenBookExistsWithMismatchingId()
        {
            Book book = new Book { Title = "C# Basics", Author = "John Doe", Year = 2023, ISBN = "1234567890123", CopiesAvailable = 3 };
            _service.Add(book);

            var result = _service.GetByTitleAndId("Book 1", 2);  // Mismatching ID

            Assert.Null(result);
        }

        //Add

        [Fact]
        public void AddBook_ShouldIncreaseCount()
        {
            var initialCount = _service.GetAll().Count;
            _service.Add(new Book { Title = "New Book", Author = "Author", ISBN = "1234567890123", Year = 2023, CopiesAvailable = 10 });
            Assert.Equal(initialCount + 1, _service.GetAll().Count);
        }

        [Fact]
        public void Add_ShouldAddBook_WhenValidBookIsProvided()
        {
            Book book = new Book { Title = "C# Basics", Author = "John Doe", Year = 2023, ISBN = "1234567890123", CopiesAvailable = 3 };

            _service.Add(book);

            var result = _service.GetAll();
            Assert.Single(result);
            Assert.Equal("C# Basics", result[0].Title);
        }

        [Fact]
        public void Add_ShouldThrowException_WhenISBNAlreadyExists()
        {
            Book book1 = new Book { Title = "C# Basics", Author = "John Doe", Year = 2023, ISBN = "1234567890123", CopiesAvailable = 3 };
            Book book2 = new Book { Title = "C# Basics edition2", Author = "John Doe", Year = 2023, ISBN = "1234567890123", CopiesAvailable = 4 };

            _service.Add(book1);

            var exception = Assert.Throws<Exception>(() => _service.Add(book2));
            Assert.Equal("Un livre avec le même ISBN existe déjà.", exception.Message);
        }

        //Update
        [Fact]
        public void Update_ShouldUpdateBook_WhenBookExists()
        {
            Book book = new Book { Title = "C# Basics", Author = "John Doe", Year = 2023, ISBN = "1234567890123", CopiesAvailable = 3 };
            _service.Add(book);

            Book updatedBook = new Book{ Title = "C# Basics edition2", Author = "John Doe", Year = 2024, ISBN = "1234567890123", CopiesAvailable = 3};
            _service.Update(updatedBook);

            var result = _service.GetById(0); // Vérifiez si l'ID utilisé est le bon
            Assert.NotNull(result); // Assurez-vous que le résultat n'est pas null
            Assert.Equal("C# Basics edition2", result.Title); // Vérifiez le titre mis à jour
            Assert.Equal("John Doe", result.Author); // Vérifiez l'auteur
            Assert.Equal(2024, result.Year); // Vérifiez l'année
        }

        [Fact]
        public void Update_ShouldNotUpdate_WhenBookDoesNotExist()
        {
            Book updatedBook = new Book { Title = "C# Basics", Author = "John Doe", Year = 2023, ISBN = "1234567890123", CopiesAvailable = 3 };

            _service.Update(updatedBook);  // Book n existe pas, et rien ne se passe

            var result = _service.GetById(1);
            Assert.Null(result);
        }

        //Delete
        [Fact]
        public void Delete_ShouldRemoveBook_WhenBookExists()
        {
            Book book = new Book { Title = "C# Basics", Author = "John Doe", Year = 2023, ISBN = "1234567890123", CopiesAvailable = 3 };
            _service.Add(book);

            _service.Delete(1);

            var result = _service.GetById(1);
            Assert.Null(result);  // Book doit etre supprimer
        }

        [Fact]
        public void Delete_ShouldDoNothing_WhenBookDoesNotExist()
        {
            _service.Delete(1);  // Book n existe pas

            var result = _service.GetById(1);
            Assert.Null(result);  // rien n est supprimer
        }

    }
}
