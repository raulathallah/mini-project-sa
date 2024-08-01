using System.ComponentModel.DataAnnotations;

namespace FoodOrderingSystemAPI.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string Name { get; set; }
        [Range(0.01, 100000)]
        public double Price { get; set; }
        [Required]
        [RegularExpression("Food|Beverage|Dessert")]
        public string Category { get; set; }
        [Range(0, 5)]
        public double Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsAvailable { get; set; }

    }
}
