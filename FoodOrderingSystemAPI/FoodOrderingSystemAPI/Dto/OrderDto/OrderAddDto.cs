using FoodOrderingSystemAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderingSystemAPI.Dto.OrderDto
{
    public class OrderAddDto : ResponseDto
    {
        public int CustomerId { get; set; }
        public string Note { get; set; }
        public List<int> MenuList { get; set; }
    }
}
