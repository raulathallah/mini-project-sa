using FoodOrderingSystemAPI.Dto.MenuDto;
using FoodOrderingSystemAPI.Interfaces;
using FoodOrderingSystemAPI.Models;
using System.Xml.Linq;

namespace FoodOrderingSystemAPI.Services
{
    public class MenuService : IMenuService
    {
        private static List<Menu> listMenu = new List<Menu>();
        private Dictionary<int, List<double>> listRating = new Dictionary<int, List<double>>();

        public MenuDetailDto AddMenu(MenuAddDto menuData)
        {
            if (listMenu.Find(lm =>lm.Name == menuData.Name) != null)
            {
                return ResponseDetail(false, null, "Menu exist");
            }

            var menu = new Menu()
            {
                Id = listMenu.Count + 1,
                Name = menuData.Name,
                Price = menuData.Price,
                Category = menuData.Category,
                Rating = 0,
                CreatedDate = DateTime.Now,
                IsAvailable = true
            };


            listMenu.Add(menu);
            return ResponseDetail(true, menu, "Add menu success!");
        }

        public MenuDetailDto AddRating(int id, double rating)
        {
            var menu = SearchMenu(id);
            if (menu == null)
            {
                return ResponseDetail(false, null, "No menu available!");
            }
            if (!listRating.ContainsKey(id))
            {
                List<double> tempNoKey = new List<double>();
                tempNoKey.Add(rating);
                listRating.Add(id, tempNoKey);
                menu.Rating = Math.Round(rating, 1);
                return ResponseDetail(true,menu, "Add rating success!");
            }
            listRating[id].Add(rating);
            double avg = Math.Round(listRating[id].Average(), 1);
            menu.Rating = avg;
            return ResponseDetail(true, menu, "Add rating success!");
        }

        public MenuDetailDto DeleteMenu(int id)
        {
            var menu = SearchMenu(id);
            if(menu == null)
            {
                return ResponseDetail(false, null, "No menu available!");
            }
            listMenu.Remove(menu);

            return ResponseDetail(true,menu, "Delete menu success!");
        }

        public MenuListDto GetAllMenu()
        {
            if(listMenu.Count == 0)
            {
                return ResponseList(false, null, "Menu list empty.");
            }
            return ResponseList(true, listMenu.FindAll(lm=>lm.IsAvailable != false), "Get all menu success!");
        }

        public MenuDetailDto GetMenuById(int id)
        {

            var menu = SearchMenu(id);
            if(menu == null)
            {
                return ResponseDetail(false, null, "No menu available!");
            }
            return ResponseDetail(true,menu, "Get menu by ID success!"); ;
        }

        public MenuDetailDto UpdateMenu(int id, MenuUpdateDto menuData)
        {
            var menu = SearchMenu(id);
            if (menu == null)
            {
                return ResponseDetail(false, null, "No menu available!");
            }

            menu.Name = menuData.Name;
            menu.Price = menuData.Price;
            menu.Category = menuData.Category;
            menu.IsAvailable = menuData.IsAvailable;

            return ResponseDetail(true, menu, "Update menu success!");
        }
        Menu SearchMenu(int id)
        {
            var menu = listMenu.FirstOrDefault(lm => lm.Id == id);
            return menu;
        }

        MenuDetailDto ResponseDetail(bool status, Menu menu, string message)
        {
            return new MenuDetailDto()
            {
                Message = message,
                Status = status,
                Data = menu
            };
        }

        MenuListDto ResponseList(bool status, List<Menu> menu, string message)
        {
            return new MenuListDto()
            {
                Message = message,
                Status = status,
                Data = menu,
            };
        }
    }
}
