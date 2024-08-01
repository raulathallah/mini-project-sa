using FoodOrderingSystemAPI.Models;

namespace FoodOrderingSystemAPI.Dto.CustomerDto
{
    public class CustomerListDto : CustomerResponseDto
    {
        public List<Customer> Data { get; set; }
    }
}
