using FoodOrderingSystemAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderingSystemAPI.Dto.CustomerDto
{
    public class CustomerDetailDto : ResponseDto
    {
        public Customer Data { get; set; }
    }
}
