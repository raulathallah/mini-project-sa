using FoodOrderingSystemAPI.Dto.CustomerDto;
using FoodOrderingSystemAPI.Interfaces;
using FoodOrderingSystemAPI.Models;
using FoodOrderingSystemAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodOrderingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Mendapatkan semua data customer
        /// </summary>

        /// <remarks>
        /// 
        /// * METHOD : GET
        /// * URL : api/customer
        ///  
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return data menu sesuai dengan parameter ID </returns>
        [HttpGet]
        public IActionResult GetAllCustomer()
        {
            var response = _customerService.GetAllCustomer();
            if (response == null)
            {
                return NotFound("No customer available.");
            }
            return Ok(response);
        }

        /// <summary>
        /// Mendapatkan data customer berdasarkan ID
        /// </summary>

        /// <remarks>
        /// 
        /// * METHOD : GET
        /// * URL : api/customer/{ id }
        ///  
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return status request, message request, dan data customer sesuai dengan parameter ID </returns>
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            if(id == null || id < 0)
            {
                return BadRequest();
            }
            var response = _customerService.GetCustomerById(id);
            if (response == null)
            {
                return NotFound("No customer available.");
            }
            return Ok(response);
        }

        /// <summary>
        /// Menambahkan data customer
        /// </summary>

        /// <remarks>
        /// 
        /// * METHOD : POST
        /// * URL : api/customer
        ///  
        /// Sample Request:
        ///
        ///     POST api/customer
        ///     {
        ///         "name": "Arif Burhan",
        ///         "email": "arif@gmail.com",
        ///         "phoneNumber": "081295949392",
        ///         "address": "Jl. Gatot Soebroto No. 62, Mamasa, SG 39292"
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return status request, message request, dan data customer yang baru saja dibuat </returns>
        [HttpPost]
        public IActionResult AddCustomer([FromBody]CustomerAddDto customer)
        {
            var response = _customerService.AddCustomer(customer);
            if (response == null)
            {
                return BadRequest("Bad request.");
            }
            return Ok(response);
        }

        /// <summary>
        /// Update data berdasarkan ID
        /// </summary>

        /// <remarks>
        /// 
        /// * METHOD : PUT
        /// * URL : api/customer/{ id }
        ///  
        /// Sample Request:
        ///
        ///     PUT api/customer/1
        ///     {
        ///         "name": "Arif Burhan",
        ///         "email": "arif@gmail.com",
        ///         "phoneNumber": "081295949392",
        ///         "address": "Jl. Gatot Soebroto No. 62, Mamasa, SG 39292"
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return status request, message request, dan data customer yang baru saja di-update </returns>
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody]CustomerAddDto customer)
        {
            var response = _customerService.UpdateCustomer(id, customer);
            if (response == null)
            {
                return BadRequest("No customer available.");
            }
            return Ok(response);
        }

        /// <summary>
        /// Delete data customer berdasarkan ID
        /// </summary>

        /// <remarks>
        /// 
        /// * METHOD : DELETE
        /// * URL : api/customer/{ id }
        ///  
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return status request, message request, dan data customer yang baru saja di-delete </returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var response = _customerService.DeleteCustomer(id);
            if (response == null)
            {
                return BadRequest("Bad request.");
            }
            return Ok(response);
        }
    }
}
