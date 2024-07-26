using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingSystem
{


    /*
     * Class Dessert inherit dari class abstrak MenuItem
     * dan override (polymorphism) fungsi CalculatePrice() dan InfoMenu() untuk mengembalikan nilai price dan menampilkan data
     */
    public class Beverage : MenuItem
    {
        private string size;

        public Beverage(string size, string name, int price, string description) : base(name, price, description)
        {
            this.size = size;
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
            Console.WriteLine("     Size: {0}", this.size);
        }
    }
}
