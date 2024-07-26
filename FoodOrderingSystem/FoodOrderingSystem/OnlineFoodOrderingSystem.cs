using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingSystem
{
    public class OnlineFoodOrderingSystem : IOrderingSystem
    {
        Random rnd = new Random();
        private List<Restaurant> restaurants = new List<Restaurant>();
        private List<int> orderNumbers = new List<int>();

        public List<Restaurant> Restaurants { get => restaurants; set => restaurants = value; }

        public void AddRestaurant(Restaurant restaurant)
        {
            restaurants.Add(restaurant);
        }

        public void PlaceOrder(string restaurantName, List<MenuItem> menu)
        {


            int idx_restaurant = 0;
            foreach (Restaurant r in this.restaurants)
            {
                if (r.Name == restaurantName)
                {
                    //generate order number
                    int orderNumber = GenerateOrderNumber(idx_restaurant, r.Orders);
                    if (orderNumber != 0)
                    {
                        Order order = new Order(menu, orderNumber);
                        r.RecieveOrders(order);
                        Console.WriteLine("...Order to {0} success! your order number is {1}", r.Name,orderNumber);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("...Order full! try again later.");
                    }
                }
                idx_restaurant++;
            }
           
        }

        public void DisplayOrderDetails(int orderNumber)
        {
            bool isThere = false;
            foreach (Restaurant r in restaurants)
            {
                foreach (Order order in r.Orders)
                {
                    if (order.Number == orderNumber)
                    {
                        isThere = true;
                        Display(r.Name, orderNumber, order.MenuItem, order);
                    }
                }
            }
           
            if (!isThere)
            {
                Console.WriteLine("...Order number not available!");
            }
        }
        public void CancelOrder(int orderNumber)
        {
            bool isThere = false;
            foreach (Restaurant r in restaurants)
            {
                foreach (Order order in r.Orders)
                {
                    if (order.Number == orderNumber)
                    {
                        isThere = true;
                        if (order.Status)
                        {
                            order.Status = false;
                            Console.WriteLine("Order {0} successfully cancelled.", orderNumber);
                        }
                        else
                        {
                            Console.WriteLine("Order {0} is cancelled!", orderNumber);
                        }
                    }
                }
            }
            if (!isThere)
            {
                Console.WriteLine("...Order number not available!");
            }
        }
        public void GetOrderStatus(int orderNumber)
        {
            bool isThere = false;
            foreach (Restaurant r in restaurants)
            {
                foreach (Order order in r.Orders)
                {
                    if (order.Number == orderNumber)
                    {
                        isThere = true;
                        if (order.Status)
                        {
                            Console.WriteLine("Status: On Process");
                        }
                        else
                        {
                            Console.WriteLine("Status: Cancelled");
                        }
                    }
                }
            }
            if (!isThere)
            {
                Console.WriteLine("...Order number not available!");
            }
        }


        int GenerateOrderNumber(int index, List<Order> orders)
        {
            //bentuk order number -> <index resto> + 00 + <increment number>
            //contoh: 1001 -> order pertama pada restoran pertama
            //contoh: 2003 -> order ketiga pada restoran kedua

            int idx = index + 1;
            int count = orders.Count + 1;
;           string orderNumber = idx.ToString()+"00"+ count.ToString();
            return int.Parse(orderNumber);
        }

        void Display(string restaurantName, int orderNumber, List<MenuItem> list, Order order)
        {
            int count = 1;
            Console.WriteLine("!!! Order Details !!!");
            Console.WriteLine("Restaurant: {0}", restaurantName);
            Console.WriteLine("Order Number: {0}", orderNumber);
            Console.WriteLine("Order List: ");
            foreach (MenuItem item in list)
            {
                Console.WriteLine(" {0}--", count);
                item.InfoMenu();
                count++;
            }

            if (order.Status)
            {
                Console.WriteLine("Status: On Process");
            }
            else
            {
                Console.WriteLine("Status: Cancelled");
            }
            
            Console.WriteLine("Total Cost: {0}", order.CalculateTotal());
        }
    }
}
