using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStoreSite.Models.Entities
{
    public class Product
    {
        public int ProductId { set; get; }
        public string Artist { set; get; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public DateTime AddedAt { get; set; }
    }
}