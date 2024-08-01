using FoodOrderingSystemAPI.Models;

namespace FoodOrderingSystemAPI.Dto.MenuDto
{
    public class MenuListDto : MenuResponseDto
    {
        public List<Menu> Data { get; set; }

    }
}
