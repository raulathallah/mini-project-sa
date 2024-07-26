using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingSystem
{

    public class Order
    {
        private EStatus status;
        private int number;
        private List<MenuItem> menuItem = new List<MenuItem>();

        public Order(List<MenuItem> menuItem, int number)
        {
            this.menuItem = menuItem;
            this.number = number;
            this.status = EStatus.Process;
        }
        
        public int Number { get => number; set => number = value; }
        public List<MenuItem> MenuItem { get => menuItem; set => menuItem = value; }
        public EStatus Status { get => status; set => status = value; }

        public int CalculateTotal()
        {
            int sum = 0;
            foreach(MenuItem item in menuItem)
            {
                sum += item.CalculatePrice();
                
            }
            return sum;
        }
    }
}
