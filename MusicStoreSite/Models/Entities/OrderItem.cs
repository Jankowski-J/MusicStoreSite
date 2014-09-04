using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicStoreSite.Models.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}