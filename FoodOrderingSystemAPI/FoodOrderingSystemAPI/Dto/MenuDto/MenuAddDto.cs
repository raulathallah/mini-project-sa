using System.ComponentModel.DataAnnotations;

namespace FoodOrderingSystemAPI.Dto.MenuDto
{
    public class MenuAddDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string Name { get; set; }
        [Range(0.01, 100000)]
        public double Price { get; set; }
        [Required]
        [RegularExpression("Food|Beverage|Dessert")]
        public string Category { get; set; }
    }
}
