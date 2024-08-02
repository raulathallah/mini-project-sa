using FoodOrderingSystemAPI.Models;

namespace FoodOrderingSystemAPI.Dto.OrderDto
{
    public class OrderDetailDto : ResponseDto
    {
        public Order Data { get; set; }
    }
}
