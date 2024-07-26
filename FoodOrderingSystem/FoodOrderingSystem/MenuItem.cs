using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderingSystem
{

    /*
     * 
     * Class abstract yang akan digunakan oleh 3 Class yaitu: Food, Beverage, Dessert
     * field bersifat protected yg hanya bisa digunakan langsung oleh anak dari MenuItem (encapsulation)
     * 
     */
    public abstract class MenuItem
    {
        protected string name;
        protected int price;
        protected string description;

        public string Name { get => name; set => name = value; }
        public int Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }

        /*
         * membuat constructor agar anak dari MenuItem dapat menggunakan value dari MenuItem dengan fungsi base()
         */
        public MenuItem(string name, int price, string description)
        {
            this.name = name;
            this.price = price;
            this.description = description;
        }

        /*
         * membuat fungsi CalculatePrice() sebagai fungsi yg harus ada didalam anak dari class MenuItem, yaitu: Food, Beverage, Dessert (abstraction)
         */
        public abstract int CalculatePrice();
        public abstract void InfoMenu();
    }
}
