using System.ComponentModel.DataAnnotations;

namespace FoodOrderingSystemAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
        [RegularExpression("Processed|Delivered|Canceled")]
        public string OrderStatus { get; set; }
        public string Note { get; set; }
        public List<Menu> MenuList { get; set; }



        public void CalculatedTotalOrder()
        {
            double sum = 0;
            foreach (var item in MenuList)
            {
                sum += item.Price;
            }
            this.TotalPrice = sum;
        }
    }
}
