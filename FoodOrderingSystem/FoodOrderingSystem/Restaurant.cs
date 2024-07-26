using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingSystem
{
    public class Restaurant
    {
        private string name;
        private List<MenuItem> menus = new List<MenuItem>();
        private List<Order> orders = new List<Order>();

        public string Name { get => name; set => name = value; }
        public List<MenuItem> Menus { get => menus; set => menus = value; }
        public List<Order> Orders { get => orders; set => orders = value; }

        public Restaurant(string name) 
        { 
            this.name = name;
        }

        public void AddItemToMenu(MenuItem item)
        {

            //check jika item sudah ada tau belum dalam menu
            bool isThere = false;
            foreach(MenuItem menu in menus)
            {
               if(menu.Name == item.Name)
                {
                    isThere = true;
                }
                else
                {
                    isThere = false;
                }
            }

            if (isThere)
            {
                Console.WriteLine("...{0} already added!", item.Name);
            }
            else
            {
                this.menus.Add(item);
            }
        }

        public void RecieveOrders(Order order)
        {
            orders.Add(order);
        }

        public int CalculateRevenue()
        {
            int sum = 0;
            foreach(Order order in orders)
            {
                foreach(MenuItem item in order.MenuItem)
                {
                    sum += item.CalculatePrice();
                }
            }
            return sum;
        }
    }
}
