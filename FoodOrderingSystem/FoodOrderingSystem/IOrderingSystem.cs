using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingSystem
{

    /*
     * interface berisikan fungsi-fungsi yang harus diimplementasikan oleh OnlineFoodOrderingSystem
     */
    public interface IOrderingSystem
    {
        void AddRestaurant(Restaurant restaurant);
        void PlaceOrder(string restaurantName, List<MenuItem> menu);
        void DisplayOrderDetails(int orderNumber);
        void CancelOrder(int orderNumber);
        void GetOrderStatus(int orderNumber);
    }
}
