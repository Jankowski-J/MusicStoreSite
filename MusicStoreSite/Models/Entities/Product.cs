using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicStoreSite.Models.Entities
{
    public class Product
    {
        public int ProductId { set; get; }
        [Required()]
        public string Artist { set; get; }
        [Required()]
        public string Title { get; set; }
        [Required()]
        public string Genre { get; set; }
        [Required()]
        public decimal Price { get; set; }
        [Required()]
        public DateTime AddedAt { get; set; }

        public Product()
        {
            AddedAt = DateTime.Now;
        }
    }
}