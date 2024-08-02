using FoodOrderingSystemAPI.Dto.OrderDto;
using FoodOrderingSystemAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodOrderingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        /// <summary>
        /// Melakukan order untuk memesan menu
        /// </summary>

        /// <remarks>
        /// 
        /// * METHOD : POST
        /// * URL : api/order
        /// 
        /// Sample Request:
        ///
        ///     POST api/order
        ///     {
        ///         "customerId": 1,
        ///         "note": "air dingin",
        ///         "menuList": [
        ///             {
        ///                 "id": 1,
        ///                 "name": "Kopi",
        ///                 "price": 4500,
        ///                 "category": "Beverage",
        ///                 "rating": 0,
        ///                 "createdDate": "2024-08-01T19:20:27.3461497+07:00",
        ///                 "isAvailable": false
        ///             },
        ///             {
        ///                 "id": 2,
        ///                 "name": "Water",
        ///                 "price": 3000,
        ///                 "category": "Beverage",           
        ///                 "rating": 0,
        ///                 "createdDate": "2024-08-01T19:20:43.1717881+07:00",
        ///                 "isAvailable": true
        ///             }
        ///         ]
        ///      }
        ///      
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return status, message, dan data order yang baru saja dibuat </returns>
        [HttpPost]
        public IActionResult PlaceOrder([FromBody]OrderAddDto orderData)
        {
            if (orderData == null)
            {
                return BadRequest();
            }
            var response = _orderService.PlaceOrder(orderData);
            return Ok(response);
        }

        /// <summary>
        /// Menampilkan data order berdasarkan order number
        /// </summary>
        /// 
        /// Masukan parameter query dalam route
        /// 
        /// <remarks>
        /// 
        /// * METHOD : GET
        /// * URL : api/order?orderNumber=OR1
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return status, message, dan data order </returns>
        [HttpGet]
        public IActionResult DisplayOrderDetails([FromQuery]string orderNumber)
        {
            if (orderNumber == null || !orderNumber.Contains("OR"))
            {
                return BadRequest(new OrderResponseDto() { Message = "Invalid order number"});
            }
            var response = _orderService.DisplayOrderDetails(orderNumber);
            if(response == null)
            {
                return NotFound("No order available.");
            }
            return Ok(response);
        }

        /// <summary>
        /// Melakukan cancel order berdasarkan order number
        /// </summary>
        /// 
        /// Masukan parameter query dalam route
        /// 
        /// <remarks>
        /// 
        /// * METHOD : PUT
        /// * URL : api/order/statusCancel?orderNumber=OR1
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return status, message, dan data order </returns>
        [HttpPut("statusCancel")]
        public IActionResult CancelOrder([FromQuery] string orderNumber)
        {
            if (orderNumber == null || !orderNumber.Contains("OR"))
            {
                return BadRequest(new OrderResponseDto() { Message = "Invalid order number" });
            }
            var response = _orderService.CancelOrder(orderNumber);
            if (response == null)
            {
                return NotFound("No order available.");
            }
            return Ok(response);
        }

        /// <summary>
        /// Melakukan update status order berdasarkan order number
        /// </summary>
        /// 
        /// Masukan parameter query dalam route
        /// 
        /// <remarks>
        /// 
        /// * METHOD : PUT
        /// * URL : api/order/statusUpdate?orderNumber=OR1
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return status, message, dan data order </returns>
        [HttpPut("statusUpdate")]
        public IActionResult UpdateOrderStatus([FromQuery] string orderNumber)
        {
            if (orderNumber == null || !orderNumber.Contains("OR"))
            {
                return BadRequest(new OrderResponseDto() { Message = "Invalid order number" });
            }
            var response = _orderService.UpdateOrderStatus(orderNumber);
            if (response == null)
            {
                return NotFound("No order available.");
            }
            return Ok(response);
        }


        /// <summary>
        /// Mendapatkan informasi status order berdasarkan order number
        /// </summary>
        /// 
        /// Masukan parameter query dalam route
        /// 
        /// <remarks>
        /// 
        /// * METHOD : GET
        /// * URL : api/order/status?orderNumber=OR1
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return status order </returns>
        [HttpGet("status")]
        public IActionResult GetOrderStatus([FromQuery] string orderNumber)
        {
            if (orderNumber == null || !orderNumber.Contains("OR"))
            {
                return BadRequest(new OrderResponseDto() { Message = "Invalid order number" });
            }
            var response = _orderService.GetOrderStatus(orderNumber);
            if (response == null)
            {
                return NotFound("No order available.");
            }
            return Ok(response);
        }
    }
}
