using LMS.Application.IRepositories;
using LMS.Core.IRepositories;
using LMS.Core.Models;
using LMS.Core.Models.Requests;
using LMS.Core.Services;
using LMS.Domain.Models.Requests;
using LMS.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebApi.Controllers
{
    public class BooksController : BaseController
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _bookRepository.GetAllBook();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook([FromRoute] int id, [FromBody] UpdateBookRequest book)
        {
            if (id < 1)
            {
                return BadRequest("Invalid book ID");
            }
            var response = await _bookRepository.UpdateBook(id, book);
            if (response == null)
            {
                return NotFound("No book available");
            }

            return Ok(response);
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody] AddBookRequest book)
        {
            var response = await _bookRepository.AddNewBook(book);
            if (response == null)
            {
                return BadRequest("No book available");
            }
            return Ok(response);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var response = await _bookRepository.DeleteBook(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);


        }
    }
}
