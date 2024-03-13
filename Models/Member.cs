using System.ComponentModel.DataAnnotations;
using System;
namespace AlphaGym.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 digits")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Monthly Subscription")]
        public int MonthlySubscription { get; set; }

        [Required]
        [Display(Name = "Subscription Start Date")]
        [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
         public DateTime SubscriptionStartDate { get; set; }

        [Required]
        [Display(Name = "Subscription Expiry Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime SubscriptionExpiryDate { get; set; }

    }
}
