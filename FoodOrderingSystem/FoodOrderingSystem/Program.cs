﻿
using FoodOrderingSystem;

Console.Clear();
OnlineFoodOrderingSystem ofos = new OnlineFoodOrderingSystem();

//membuat restoran
Restaurant resto1 = new Restaurant("Resto Lama" );
ofos.AddRestaurant(resto1);

//membuat menu untuk restoran 1
Food food1 = new Food("Spicy", "Foodz", 15000, "Foodz descriptions");
Beverage bev1 = new Beverage("Small", "Beveragez", 4000, "Beveragez descriptions");
Dessert des1 = new Dessert("Less", "Dessertz", 6000, "Dessertz descriptions");
Dessert des1d = new Dessert("Less", "Dessertz", 6000, "Dessertz descriptions");
resto1.AddItemToMenu(food1);
resto1.AddItemToMenu(bev1);
resto1.AddItemToMenu(des1);
resto1.AddItemToMenu(des1d);

//order menu pertama dan ketiga di restoran 1
List<MenuItem> list1 = new List<MenuItem>();
list1.Add(resto1.Menus[0]);
list1.Add(resto1.Menus[2]);

//membuat order ke restoran 1
ofos.PlaceOrder(resto1.Name, list1);

//membuat restoran
Restaurant resto2 = new Restaurant("Resto Baru");
ofos.AddRestaurant(resto2);

//membuat menu untuk restoran 2
Food food2 = new Food("Not Spicy", "Foodiee", 14500, "Foodiee descriptions");
Beverage bev2 = new Beverage("Large", "Beveriee", 4500, "Beveriee descriptions");
Dessert des2 = new Dessert("High", "Desseriee", 6500, "Desseriee descriptions");
resto2.AddItemToMenu(food2);
resto2.AddItemToMenu(bev2);
resto2.AddItemToMenu(des2);

//order menu pertama dan kedua di restoran 2
List<MenuItem> list2 = new List<MenuItem>();
list2.Add(resto2.Menus[0]);
list2.Add(resto2.Menus[1]);

//membuat order ke restoran 2
ofos.PlaceOrder(resto2.Name, list2);

//menampilkan revenue dari tiap restaurant
foreach(Restaurant r in ofos.Restaurants)
{
    Console.WriteLine("-- {0} revenue is {1}", r.Name, r.CalculateRevenue());

}

string temp;
int orderNumber = 0;
while (true) 
{
    Console.WriteLine("===================================");
    //meminta order number 
    Console.Write("Input order number: ");
    temp = Console.ReadLine();
    orderNumber = int.Parse(temp);
    
    if (orderNumber != null)
    {
        //menampilkan order
        ofos.DisplayOrderDetails(orderNumber);
        //melakukan cancel order
        ofos.CancelOrder(orderNumber);
        //mengecek status order
        ofos.GetOrderStatus(orderNumber);
    }
}







