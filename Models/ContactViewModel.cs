using System.ComponentModel.DataAnnotations;

namespace AlphaGym.Models
{
    public class ContactViewModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        [RegularExpression(@"\d{10}", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public required string PhoneNumber { get; set; }

        [Required]
        public required string Message { get; set; }
    }
}
