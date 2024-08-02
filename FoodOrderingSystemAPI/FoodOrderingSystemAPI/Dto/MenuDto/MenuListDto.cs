using FoodOrderingSystemAPI.Models;

namespace FoodOrderingSystemAPI.Dto.MenuDto
{
    public class MenuListDto : ResponseDto
    {
        public List<Menu> Data { get; set; }

    }
}
