using EchallengeListBook.Models;
using EchallengeListBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace EchallengeListBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        // GET /api/books
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            ICollection<Book> list = _service.GetAll();
            if (list.Count == 0)
            {
                return NotFound();  //404 NotFound pas de livres
            }
            return Ok(list); //Renvoie 200 OK si des livres existent
        }
        

        // GET /api/books/5
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            Book book = _service.GetById(id);
            if (book == null) return NotFound("Livre introuvable.");
            return Ok(book);
        }

        // GET /api/books/search?title=BookTitle&id=1
        [HttpGet("search")]
        public IActionResult SearchBook([FromQuery] string title, [FromQuery] int id)
        {
            if (string.IsNullOrWhiteSpace(title)) return BadRequest("Le titre est requis.");

            Book book = _service.GetByTitleAndId(title, id);
            if (book == null) return NotFound("Aucun livre trouvé avec les critères donnés.");
            return Ok(book);
        }

        // POST /api/books/Create
        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(); //422 ne peut pas traiter la requete
            _service.Add(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        // PUT  /api/books/Edit/5
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id) return BadRequest("L'ID ne correspond pas.");
            if (!ModelState.IsValid) return UnprocessableEntity();
            _service.Update(book);
            return NoContent();
        }

        // DELETE /api/books/Delete/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            if (_service.GetById(id) == null)
            {
                return NotFound();
            }
            _service.Delete(id);
            return NoContent(); //204 NoContent
        }
    }
}
