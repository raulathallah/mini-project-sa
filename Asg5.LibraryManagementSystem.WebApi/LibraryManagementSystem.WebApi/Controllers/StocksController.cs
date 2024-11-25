using LibraryManagementSystem.Domain.Models.Requests.Borrows;
using LibraryManagementSystem.Domain.Models.Requests.Stocks;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Domain.Service;
using LibraryManagementSystem.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagementSystem.WebApi.Controllers
{
    public class StocksController : BaseController
    {
        private readonly IStockService _stockService;
        private readonly IBookUserTransactionRepository _bookUserTransactionRepository;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        // POST api/<StocksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddStockRequest request)
        {
            var response = await _stockService.AddStocks(request);
            if(response == null)
            {
                return BadRequest();
            }
            return Ok(response);

        }

        // POST api/<StocksController>/transactions/borrow
        [HttpPost("transactions/borrow")]
        public async Task<IActionResult> Borrow([FromBody] BookBorrowRequest request)
        {
            var response = await _stockService.BookCheckOut(request);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);

        }

        // POST api/<StocksController>/return
        [HttpPost("transactions/return")]
        public async Task<IActionResult> ReturnBook([FromBody] BookReturnRequest request)
        {
            var response = await _stockService.BookCheckIn(request);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // POST api/<StocksController>/transactions/all
        [HttpGet("transactions/all")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var response = await _stockService.GetAllTransactions();
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

    }
}
