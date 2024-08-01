using FoodOrderingSystemAPI.Models;

namespace FoodOrderingSystemAPI.Dto.MenuDto
{
    public class MenuDetailDto : MenuResponseDto
    {
        public Menu Data { get; set; }
    }
}
