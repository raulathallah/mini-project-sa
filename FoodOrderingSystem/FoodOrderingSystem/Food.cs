using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingSystem
{

    /*
     * Class Food inherit dari class abstrak MenuItem
     * dan override (polymorphism) fungsi CalculatePrice() untuk mengembalikan nilai price class Food
     */
    public class Food : MenuItem
    {
        private string spiciness;

        public Food(string spiciness, string name, int price, string description) : base(name, price, description)
        {
            this.spiciness = spiciness;
            this.name = name;
            this.price = price;
            this.description = description;
        }
        public override int CalculatePrice()
        {
            return this.price;
        }
        public override void InfoMenu()
        {
            Console.WriteLine("     Name: {0}", this.name);
            Console.WriteLine("     Price: {0}", this.price);
            Console.WriteLine("     Description: {0}", this.description);
            Console.WriteLine("     Spiciness: {0}", this.spiciness);
        }
    }
}
