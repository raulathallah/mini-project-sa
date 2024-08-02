using FoodOrderingSystemAPI.Models;

namespace FoodOrderingSystemAPI.Dto.MenuDto
{
    public class MenuDetailDto : ResponseDto
    {
        public Menu Data { get; set; }
    }
}
