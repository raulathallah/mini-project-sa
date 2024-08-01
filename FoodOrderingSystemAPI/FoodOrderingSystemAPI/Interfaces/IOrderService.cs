using FoodOrderingSystemAPI.Dto.OrderDto;

namespace FoodOrderingSystemAPI.Interfaces
{
    public interface IOrderService
    {
        OrderDetailDto PlaceOrder(OrderAddDto orderData);
        OrderDetailDto DisplayOrderDetails(string orderNumber);
        OrderDetailDto CancelOrder(string orderNumber);
        OrderDetailDto UpdateOrderStatus(string orderNumber);
        string GetOrderStatus(string orderNumber);
    }
}
