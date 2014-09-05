using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MusicStoreSite.Models.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"^[0-9A-Za-z\-]{5,9}$", ErrorMessage = "Invalid Postal Code.")]
        public string PostalCode { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "Please provide a 9-digit phone number.")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a valid email in format: login@provider")]
        public string Email { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public virtual ICollection<OrderItem> OrderItemsList { get; set; }
    }
}