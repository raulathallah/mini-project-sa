using FoodOrderingSystemAPI.Models;

namespace FoodOrderingSystemAPI.Dto.CustomerDto
{
    public class CustomerListDto : ResponseDto
    {
        public List<Customer> Data { get; set; }
    }
}
