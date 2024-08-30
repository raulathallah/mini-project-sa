using LibraryManagementSystem.Domain.Models.Requests.CheckOuts;
using LibraryManagementSystem.Domain.Models.Requests.Stocks;
using LibraryManagementSystem.Domain.Service;
using LibraryManagementSystem.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Librarian")]
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

        // POST api/<StocksController>/cout
        [Authorize(Roles = "Librarian")]
        [HttpPost("cout")]
        public async Task<IActionResult> CheckOut([FromBody] BookCheckOutRequest request)
        {
            var response = await _stockService.BookCheckOut(request);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);

        }

        // POST api/<StocksController>/cout-book
        [Authorize(Roles = "Librarian")]
        [HttpPost("cout-book")]
        public async Task<IActionResult> CheckOutBookInformation([FromBody] BookCheckOutBookRequest request)
        {
            var response = await _stockService.BookCheckOutBookInformation(request);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);

        }

        // POST api/<StocksController>/cout-book
        [Authorize(Roles = "Librarian")]
        [HttpPost("cout-user")]
        public async Task<IActionResult> CheckOutUserInformation([FromBody] BookCheckOutUserRequest request)
        {
            var response = await _stockService.BookCheckOutUserInformation(request);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);

        }
    }
}
