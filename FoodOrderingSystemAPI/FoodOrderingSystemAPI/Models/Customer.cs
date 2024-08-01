using System.ComponentModel.DataAnnotations;

namespace FoodOrderingSystemAPI.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
