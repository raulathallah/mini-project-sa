using LMS.Application.IRepositories;
using LMS.Core.Models;
using LMS.Core.Models.Requests;
using LMS.Core.Services;
using LMS.Domain.Models.Requests;
using LMS.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebApi.Controllers
{
    public class TransactionsController : BaseController
    {
        private readonly IBookManager _bookManager;
        private readonly IBookusertransactionRepository _transactionRepository;

        public TransactionsController(IBookManager bookManager, IBookusertransactionRepository transactionRepository)
        {
            _bookManager = bookManager;
            _transactionRepository = transactionRepository;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookusertransaction>>> GetAllTransactions()
        {
            return await _transactionRepository.GetAllTransactions();
        }


        // GET: api/Transactions/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookusertransaction>> GetTransactionsById(int id)
        {
            var response = await _transactionRepository.GetTransactionById(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // POST: api/Transactions/borrow
        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowBookRequest request)
        {
            var response = await _bookManager.BorrowBook(request);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // POST: api/Transactions/return
        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookRequest request)
        {
            var response = await _bookManager.ReturnBook(request);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
