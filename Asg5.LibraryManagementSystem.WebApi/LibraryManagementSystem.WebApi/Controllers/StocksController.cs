using LibraryManagementSystem.Domain.Models.Requests.Borrows;
using LibraryManagementSystem.Domain.Models.Requests.Stocks;
using LibraryManagementSystem.Domain.Service;
using LibraryManagementSystem.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagementSystem.WebApi.Controllers
{
    public class StocksController : BaseController
    {
        private readonly IStockService _stockService;

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

        // POST api/<StocksController>
        [HttpPost("borrow")]
        public async Task<IActionResult> Borrow([FromBody] BookBorrowRequest request)
        {
            var response = await _stockService.BookCheckOut(request);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);

        }
    }
}
