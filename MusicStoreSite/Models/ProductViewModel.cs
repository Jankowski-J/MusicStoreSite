using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicStoreSite.Models.Contexts;
using MusicStoreSite.Models.Entities;

namespace MusicStoreSite.Models
{
    public class ProductViewModel
    {
        public int ProductId { set; get; }
        public string Artist { set; get; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public DateTime AddedAt { get; set; }

        public ProductViewModel(int productId)
        {
            var musicStoreContext = new MusicStoreContext();
            Product product = musicStoreContext.Products.FirstOrDefault(x => x.ProductId == productId);
            ProductId = product.ProductId;
            Artist = product.Artist;
            Title = product.Title;
            Genre = musicStoreContext.Genres.Where(x => x.GenreId == product.GenreId).Select(y => y.Name).FirstOrDefault();
            Price = product.Price;
            AddedAt = product.AddedAt;
        }
    }
}