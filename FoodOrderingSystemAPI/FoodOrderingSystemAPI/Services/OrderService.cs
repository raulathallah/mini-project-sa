using FoodOrderingSystemAPI.Dto.CustomerDto;
using FoodOrderingSystemAPI.Dto.OrderDto;
using FoodOrderingSystemAPI.Interfaces;
using FoodOrderingSystemAPI.Models;

namespace FoodOrderingSystemAPI.Services
{
    public class OrderService : IOrderService
    {
        private static List<Order> listOrder = new List<Order>();
        //private static Dictionary<string, Order> listOrderNumber = new Dictionary<string, Order>();
        private readonly ICustomerService _customerService;
        private readonly IMenuService _menuService; 

        public OrderService(ICustomerService customerService, IMenuService menuService)
        {
            _customerService = customerService;
            _menuService = menuService;
        }

        public OrderDetailDto CancelOrder(string orderNumber)
        {
            var response = listOrder.Find(lo => lo.OrderNumber == orderNumber);
            if (response == null)
            {
                return ResponseDetail(false, null, $"No order with number {orderNumber}");
            }

            response.OrderStatus = "Cancel";
            return ResponseDetail(true, response, $"Cancel order with ID {orderNumber} success!");
        }

        public OrderDetailDto DisplayOrderDetails(string orderNumber)
        {
            var response = listOrder.Find(lo=>lo.OrderNumber == orderNumber);
            if (response == null)
            {
                return ResponseDetail(false, null, $"No order with number {orderNumber}");
            }
            return ResponseDetail(true, response, $"Display order with ID {orderNumber} success!");
        }

        public string GetOrderStatus(string orderNumber)
        {
            var response = listOrder.Find(lo => lo.OrderNumber == orderNumber);
            if (response == null)
            {
                return response.OrderStatus;
            }
            return response.OrderStatus;
        }

        public OrderDetailDto PlaceOrder(OrderAddDto orderData)
        {
            var customer = _customerService.GetCustomerById(orderData.CustomerId);
            if(customer.Data == null)
            {
                return ResponseDetail(false, null, "Order Failed! Customer is not available.");
            }

            bool isThere = false;
            string menuName = "";
            foreach (var item in orderData.MenuList)
            {
                var menu = _menuService.GetMenuById(item.Id);
                if (menu.Data == null || menu.Data.IsAvailable == false)
                {

                    menuName = item.Name;
                    isThere = true;
                    break;
                }
            }
            if (isThere)
            {
                return ResponseDetail(false, null, $"Order Failed! {menuName} is not available.");
            }

            var order = new Order()
            {
                Id = listOrder.Count + 1,
                OrderNumber = GenerateOrderNumber(),
                CustomerId = orderData.CustomerId,
                OrderDate = DateTime.Now,
                OrderStatus = "Processed",
                Note = orderData.Note,
                MenuList = orderData.MenuList,
            };
            order.CalculatedTotalOrder();
            listOrder.Add(order);
            return ResponseDetail(true, order, "Place order success!");
        }

        public OrderDetailDto UpdateOrderStatus(string orderNumber)
        {
            var response = listOrder.Find(lo => lo.OrderNumber == orderNumber);
            if (response == null)
            {
                return ResponseDetail(false, null, $"No order with number {orderNumber}");
            }

            if(response.OrderStatus == "Cancel")
            {
                return ResponseDetail(false, null, "Order is cancelled");
            }

            response.OrderStatus = "Delivered";
            return ResponseDetail(true, response, $"Order with ID {orderNumber} delivered!");
        }

        Order SearchOrder(int id)
        {
            var order = listOrder.FirstOrDefault(lo => lo.Id == id);
            return order;
        }

        string GenerateOrderNumber()
        {
            int number = listOrder.Count + 1;
            return $"OR{number}";
        }

        OrderDetailDto ResponseDetail(bool status, Order order, string message)
        {
            return new OrderDetailDto()
            {
                Message = message,
                Status = status,
                Data = order
            };
        }
    }
}
