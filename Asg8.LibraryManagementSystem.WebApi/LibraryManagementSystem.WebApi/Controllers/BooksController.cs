using LibraryManagementSystem.Application.Helpers;
using LibraryManagementSystem.Application.Mappers;
using LibraryManagementSystem.Core.Models;
using LibraryManagementSystem.Core.Models.Responses;
using LibraryManagementSystem.Domain.Helpers;
using LibraryManagementSystem.Domain.Models.Requests;
using LibraryManagementSystem.Domain.Models.Requests.Books;
using LibraryManagementSystem.Domain.Service;
using LibraryManagementSystem.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagementSystem.WebApi.Controllers
{
    public class BooksController : BaseController
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        // GET: api/<BooksController>
        [Authorize(Roles = "Librarian")]
        [HttpGet]
        public async Task<IEnumerable<object>> Get()
        {
            return await _bookService.GetAllBook();
        }

        // GET api/<BooksController>/5
        [Authorize(Roles = "Librarian")]
        [HttpGet("{bookId}")]
        public async Task<IActionResult> Get([FromRoute] int bookId)
        {
            var book = await _bookService.GetBookById(bookId);
            if(book == null)
            {
                return NotFound();
            }
            return Ok(book.ToBookResponse());
        }

        // POST api/<BooksController>
        [Authorize(Roles = "Librarian")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddBookRequest request)
        {
            var response = await _bookService.AddNewBook(request);
            if(response == null)
            {
                return BadRequest();
            }
            return Ok(response.ToBookResponse());
        }

        // PUT api/<BooksController>/5
        [Authorize(Roles = "Librarian")]
        [HttpPut("{bookId}")]
        public async Task<IActionResult> Put([FromRoute]int bookId, [FromBody]UpdateBookRequest request)
        {
            var response = await _bookService.UpdateBook(bookId, request);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response.ToBookResponse());
        }

        // DELETE api/<BooksController>/5
        [Authorize(Roles = "Librarian")]
        [HttpDelete("{bookId}")]
        public async Task<IActionResult> Delete([FromRoute]int bookId, [FromBody]DeleteBookRequest request)
        {
            var response = await _bookService.DeleteBook(bookId, request);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response.ToBookResponse());
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchBooksPaged([FromQuery] SearchBookQuery query, [FromBody]PageRequest pageRequest)
        {
            var response = await _bookService.GetAllBookSearchPaged(query, pageRequest);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }


        // GET api/Books/report
        //[Authorize(Roles = "Librarian, Library Manager")]
        [HttpGet("overdue-report")]
        public async Task<IActionResult> BookOverdueReport()
        {
            var fileName = "BookReport.pdf";
            var response = await _bookService.GenerateOverdueReportPDF();
            return File(response, "application/pdf", fileName);

        }

        // POST api/Books/signout-report
        //[Authorize(Roles = "Librarian, Library Manager")]
        [HttpPost("signout-report")]
        public async Task<IActionResult> SignOutReport([FromBody] BookSignOutReportRequest request)
        {
            var fileName = "BookSignOutReport.pdf";
            var response = await _bookService.GenerateSignOutBookReportPDF(request.StartDate, request.EndDate);
            return File(response, "application/pdf", fileName);

        }

        // POST api/Books/purchase-report
        //[Authorize(Roles = "Librarian, Library Manager")]
        [HttpPost("purchase-report")]
        public async Task<IActionResult> PurchaseReport([FromBody] BookPurchaseReportRequest request)
        {
            var fileName = "BookPurchaseReport.pdf";
            var response = await _bookService.GenerateBookPurchaseReportPDF(request.StartDate, request.EndDate);
            return File(response, "application/pdf", fileName);

        }

        

        
    }
}
