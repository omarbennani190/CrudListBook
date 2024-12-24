using EchallengeListBook.Controllers;
using EchallengeListBook.Models;
using EchallengeListBook.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.Tests.UnitTests
{
    public class BookControllerTests
    {
        private readonly Mock<IBookService> _serviceMock;
        private readonly BookController _controller;

        public BookControllerTests()
        {
            _serviceMock = new Mock<IBookService>();
            _controller = new BookController(_serviceMock.Object);
        }

        //GetAllBooks
        [Fact]
        public void GetAllBooks_ShouldReturnOk_WhenBooksExist()
        {
            // Arrange
            var books = new List<Book> { new Book { Title = "C# Basics", Author = "John Doe", Year = 2023, ISBN = "1234567890123", CopiesAvailable = 3 } };
            _serviceMock.Setup(s => s.GetAll()).Returns(books);

            // Act
            var result = _controller.GetAllBooks() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(books, result.Value);
        }

        [Fact]
        public void GetAllBooks_ShouldReturnNotFound_WhenNoBooksExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetAll()).Returns(new List<Book>());

            // Act
            var result = _controller.GetAllBooks() as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        //GetBookById
        [Fact]
        public void GetBookById_ShouldReturnOk_WhenBookExists()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Book1" };
            _serviceMock.Setup(s => s.GetById(1)).Returns(book);

            // Act
            var result = _controller.GetBookById(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(book, result.Value);
        }

        [Fact]
        public void GetBookById_ShouldReturnNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetById(1)).Returns((Book)null);

            // Act
            var result = _controller.GetBookById(1) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Livre introuvable.", result.Value); // compare le msg de GetBookById avec la valeur de result
        }

        //SearchBook
        [Fact]
        public void SearchBook_ShouldReturnOk_WhenBookMatchesCriteria()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Book1" };
            _serviceMock.Setup(s => s.GetByTitleAndId("Book1", 1)).Returns(book);

            // Act
            var result = _controller.SearchBook("Book1", 1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(book, result.Value);
        }

        [Fact]
        public void SearchBook_ShouldReturnNotFound_WhenNoBookMatchesCriteria()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetByTitleAndId("Book1", 1)).Returns((Book)null);

            // Act
            var result = _controller.SearchBook("none", 1) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Aucun livre trouvé avec les critères donnés.", result.Value);
        }

        [Fact]
        public void SearchBook_ShouldReturnBadRequest_WhenTitleIsEmpty()
        {
            // Act
            var result = _controller.SearchBook("", 1) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Le titre est requis.", result.Value);
        }

        //AddBook
        [Fact]
        public void AddBook_ShouldReturnCreated_WhenBookIsValid()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "New Book" };

            // Act
            var result = _controller.AddBook(book) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(book, result.Value);
        }

        [Fact]
        public void AddBook_ShouldReturnUnprocessableEntity_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Title", "Required");

            // Act
            var result = _controller.AddBook(new Book()) as UnprocessableEntityResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(422, result.StatusCode);
        }

        //UpdateBook
        [Fact]
        public void UpdateBook_ShouldReturnNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var book = new Book { Id = 1, Title = "Updated Book" };

            // Act
            var result = _controller.UpdateBook(1, book) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public void UpdateBook_ShouldReturnBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            var book = new Book { Id = 2, Title = "Updated Book" };

            // Act
            var result = _controller.UpdateBook(1, book) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("L'ID ne correspond pas.", result.Value);
        }

        [Fact]
        public void UpdateBook_ShouldReturnUnprocessableEntity_WhenModelStateIsInvalid()
        {
            // Vérifiez que le contrôleur est initialisé
            Assert.NotNull(_controller);

            // Le modèle (Book) est invalide, si le titre est null ou invalide dans la requête
            _controller.ModelState.AddModelError("Title", "Required");
            var book = new Book { Id = 2, Title = "0" };
            // UpdateBook avec un objet Book vide et id=1
            var result = _controller.UpdateBook(2, book) as UnprocessableEntityResult;

            // Verifier que la réponse n'est pas nulle et code http est 422
            Assert.NotNull(result);
            Assert.Equal(422, result.StatusCode);
        }

        //DeleteBook
        [Fact]
        public void DeleteBook_ShouldReturnNoContent_WhenBookIsDeleted()
        {
            // Configurer le comportement simulé (mock) de _serviceMock.
            // GetById(1) doit retourner un objet Book avec un Id de 1.
            _serviceMock.Setup(s => s.GetById(1)).Returns(new Book { Id = 1 });

            // Reponse attendu avec le code de statut HTTP 204
            var result = _controller.DeleteBook(1) as NoContentResult;

            // le résultat n'est pas null et DeleteBook va retourné une réponse.
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public void DeleteBook_ShouldReturnNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetById(1)).Returns((Book)null);

            // Act
            var result = _controller.DeleteBook(1) as NotFoundResult;

            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

    }
}
