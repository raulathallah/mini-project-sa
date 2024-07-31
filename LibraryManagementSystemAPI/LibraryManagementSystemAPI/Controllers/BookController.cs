using LibraryManagementSystemAPI.Dto;
using LibraryManagementSystemAPI.Interface;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }


        /// <summary>
        /// Mendapatkan semua data buku dalam list
        /// </summary>
        /// <remarks>
        /// No parameters.
        ///  
        /// Sample request:
        /// 
        ///     GET /book
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> Mengembalikan status request, message request, dan data buku dalam list </returns>
        [HttpGet]
        [ProducesResponseType(typeof(BookListDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BookListDto), StatusCodes.Status404NotFound)]
        public IActionResult GetAllBook()
        {
            BookListDto res = _bookService.GetAllBook();

            if(!res.Status)
            {
                return NotFound(res);
            }
            return Ok(res);
        }

        /// <summary>
        /// Mendapatkan data buku seusai ID yang diinginkan
        /// </summary>
        /// <remarks>
        /// Memiliki 1 parameter dalam route
        ///     
        /// Method : GET
        /// 
        /// URL : /book/{id}
        ///  
        /// Sample request:
        /// 
        ///     GET /book/1
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> Mengembalikan status request, message request dan data buku sesuai ID parameter</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status400BadRequest)]
        public IActionResult GetBookById(int id)
        {
            BookDetailDto res = _bookService.GetBookById(id);
            if(!res.Status)
            {
                return NotFound(res);
            }

            return Ok(res);
        }


        /// <summary>
        /// Melakukan tambah data buku kedalam list 
        /// </summary>
        /// <remarks>
        /// Memiliki body request
        ///     
        /// Method : POST
        /// 
        /// URL : /book
        ///  
        /// Sample request:
        /// 
        ///     POST /book
        ///     {
        ///         "title": "Buku Volume 1",
        ///         "author": "Arif Burhan",
        ///         "publicationYear": 2020,
        ///         "isbn": "9781234567897"
        ///     }
        ///     
        ///     POST /book
        ///     {
        ///         "title": "Buku Volume 2: The Adventure",
        ///         "author": "Arif Burhan",
        ///         "publicationYear": 2021,
        ///         "isbn": "9781234567898"
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> Mengembalikan status request, message request, dan data buku yang baru saja di tambahkan</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status400BadRequest)]
        public IActionResult AddNewBook([FromBody] BookAddDto value)
        {
            BookResponseDto res = _bookService.AddNewBook(value);
            if (!res.Status)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        /// <summary>
        /// Melakukan update data Book yang sudah ada dalam list
        /// </summary>
        /// <remarks>
        /// Memiliki body request dan parameter ID pada route
        ///     
        /// Method : PUT
        /// 
        /// URL : /book/{id}
        ///  
        /// Sample request:
        /// 
        ///     PUT /book/1
        ///     {
        ///         "title": "Buku Volume 1: Prelude",
        ///         "author": "Arif Burhan",
        ///         "publicationYear": 2020,
        ///         "isbn": "9781234567897"
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> Mengembalikan status request, message request, dan data buku yang baru saja diperbarui </returns>
        [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookAddDto value)
        {
            BookResponseDto res = _bookService.UpdateBook(id, value);
            if(!res.Status)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        /// <summary>
        /// Melakukan delete data Book yang sudah ada dalam list
        /// </summary>

        /// <remarks>
        /// Memiliki parameter ID dalam route
        ///     
        /// Method : DELETE
        /// 
        /// URL : /book/{id}
        ///  
        /// Sample request:
        /// 
        ///     DELETE /book/1
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> Mengembalikan status request, message request, dan data buku yang baru saja di delete </returns>
        [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            BookResponseDto res = _bookService.DeleteBook(id);
            if (!res.Status)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }
    }
}
