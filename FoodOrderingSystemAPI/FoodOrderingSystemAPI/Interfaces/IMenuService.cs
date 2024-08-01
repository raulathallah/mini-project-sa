using FoodOrderingSystemAPI.Dto.MenuDto;
using FoodOrderingSystemAPI.Models;

namespace FoodOrderingSystemAPI.Interfaces
{
    public interface IMenuService
    {
        MenuListDto GetAllMenu();
        MenuDetailDto AddMenu(MenuAddDto m);
        MenuDetailDto GetMenuById(int id);
        MenuDetailDto UpdateMenu(int id, MenuUpdateDto m);
        MenuDetailDto DeleteMenu(int id);
        MenuDetailDto AddRating(int id, double rating);
    }
}
