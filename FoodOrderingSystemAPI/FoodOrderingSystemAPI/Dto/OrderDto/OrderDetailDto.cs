using FoodOrderingSystemAPI.Models;

namespace FoodOrderingSystemAPI.Dto.OrderDto
{
    public class OrderDetailDto : OrderResponseDto
    {
        public Order Data { get; set; }
    }
}
