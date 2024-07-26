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
            //generate order number
            int orderNumber = GenerateOrderNumber();
            orderNumbers.Add(orderNumber);
            OnlineFoodOrderingSystem ofos = new OnlineFoodOrderingSystem();
            Order order = new Order(menu, orderNumber);

            foreach (Restaurant r in this.restaurants)
            {
                if (r.Name == restaurantName)
                {
                    r.RecieveOrders(order);
                }
            }
            Console.WriteLine("...Order success! your order number is {0}", orderNumber);
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
                            Console.WriteLine("Status: On Process!");
                        }
                        else
                        {
                            Console.WriteLine("Status: Cancelled!");
                        }
                    }
                }
            }
            if (!isThere)
            {
                Console.WriteLine("...Order number not available!");
            }
        }


        int GenerateOrderNumber()
        {
            int temp = rnd.Next(1000);
            if (orderNumbers.Contains(temp))
            {
                GenerateOrderNumber();
            }
            else
            {
                return temp;
            }
            return temp;
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
            Console.WriteLine("Total Cost: {0}", order.CalculateTotal());
        }
    }
}
