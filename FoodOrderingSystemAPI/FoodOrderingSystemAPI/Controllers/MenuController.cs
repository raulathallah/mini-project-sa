using FoodOrderingSystemAPI.Dto.MenuDto;
using FoodOrderingSystemAPI.Interfaces;
using FoodOrderingSystemAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodOrderingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuServices;

        public MenuController(IMenuService menuServices)
        {
            _menuServices = menuServices;
        }

        /// <summary>
        /// Mendapatkan semua data menu
        /// </summary>

        /// <remarks>
        /// 
        /// * METHOD : GET
        /// * URL : api/menu
        ///  
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return semua data menu </returns>
        [HttpGet]
        [ProducesResponseType(typeof(MenuListDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MenuListDto), StatusCodes.Status404NotFound)]
        public IActionResult GetAllMenu()
        {
            var response = _menuServices.GetAllMenu();
            if(response == null)
            {
                return NotFound("No menu available.");
            }
            return Ok(response);
        }

        /// <summary>
        /// Mendapatkan semua data menu berdasarkan ID
        /// </summary>

        /// <remarks>
        /// 
        /// * METHOD : GET
        /// * URL : api/menu/{ id }
        ///  
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return data menu sesuai dengan parameter ID </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MenuDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MenuResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MenuDetailDto), StatusCodes.Status404NotFound)]
        public IActionResult GetMenuById(int id)
        {
            if (id < 1)
            {
                return BadRequest(new MenuResponseDto() { Message = "Invalid menu id" });
            }
            var response = _menuServices.GetMenuById(id);
            if(response == null)
            {
                return NotFound("No menu available.");
            }
            return Ok(response);
        }


        /// <summary>
        /// Membuat data menu
        /// </summary>

        /// <remarks>
        /// 
        /// * METHOD : POST
        /// * URL : api/menu
        /// 
        /// Sample Request:
        /// 
        ///     POST api/menu
        ///     {
        ///         "name": "Water",
        ///         "price": 3000,
        ///         "category": "Beverage"
        ///     }
        ///     
        ///     POST api/menu
        ///     {
        ///         "name": "Indomie",
        ///         "price": 5500,
        ///         "category": "Food"
        ///     }
        ///  
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return status request, message request, data menu yang baru saja dibuat </returns>
        [HttpPost]
        [ProducesResponseType(typeof(MenuDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateMenu([FromBody] MenuAddDto menuDto)
        {
            var response = _menuServices.AddMenu(menuDto);
            return Ok(response);
        }

        /// <summary>
        /// Update data menu berdasarkan ID
        /// </summary>

        /// <remarks>
        /// 
        /// * METHOD : PUT
        /// * URL : api/menu/{ id }
        /// 
        /// Sample Request:
        /// 
        ///     PUT api/menu/1
        ///     {
        ///         "name": "Kopi Torabika",
        ///         "price": 3500,
        ///         "category": "Beverage"
        ///     }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return status request, message request, data menu yang baru saja di-update </returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(MenuDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MenuResponseDto), StatusCodes.Status404NotFound)]
        public IActionResult UpdateMenu(int id, [FromBody] MenuUpdateDto menu)
        {
            if (id < 1)
            {
                return BadRequest(new MenuResponseDto() { Message = "Invalid menu id" });
            }
            var response = _menuServices.UpdateMenu(id, menu);
            if (response == null)
            {
                return NotFound("No menu available.");
            }
            return Ok(response);
        }

        /// <summary>
        /// Delete data menu berdasarkan ID
        /// </summary>

        /// <remarks>
        /// 
        /// * METHOD : DELETE
        /// * URL : api/menu/{ id }
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return status request, message request, data menu yang baru saja di-delete </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(MenuDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MenuResponseDto), StatusCodes.Status404NotFound)]
        public IActionResult DeleteMenu(int id)
        {
            if (id < 1)
            {
                return BadRequest(new MenuResponseDto() { Message = "Invalid menu id" });
            }
            var response = _menuServices.DeleteMenu(id);
            if (response == null)
            {
                return NotFound("No menu available.");
            }
            return Ok(response);
        }

        /// <summary>
        /// Menambahkan rating terhadap menu berdasarkan ID
        /// </summary>

        /// <remarks>
        /// 
        /// Masukan parameter id dan rating ke dalam query route
        /// 
        /// * METHOD : PUT
        /// * URL : api/menu/rating?id={ id }rating={ rating }
        /// 
        /// Sample Request:
        /// 
        ///     POST api/menu/rating?id=1rating=4
        ///    
        /// </remarks>
        /// <param name="request"></param>
        /// <returns> return status request, message request, data menu yang baru saja diberikan rating </returns>
        [ProducesResponseType(typeof(MenuDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MenuResponseDto), StatusCodes.Status404NotFound)]
        [HttpPost("rating")]
        public IActionResult AddRating([FromQuery]int id, double rating)
        {
            if (id < 1)
            {
                return BadRequest(new MenuResponseDto() { Message = "Invalid menu id" });
            }
            if (rating < 1 || rating > 5)
            {
                return BadRequest(new MenuResponseDto() { Message = "Invalid range input! range 1 - 5" });
            }
            var response = _menuServices.AddRating(id, rating);
            if (response == null)
            {
                return NotFound("No menu available.");
            }
            return Ok(response);
        }
    }
}
