using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStoreSite.Models.Entities
{
    public class ShoppingCart
    {
        readonly List<Product> _products = new List<Product>();

        public void AddItem(Product product)
        {
            if (product == null)
            {
                return;
            }
            _products.Add(product);
        }

        public void RemoveItem(Product product)
        {
            var productToRemove = _products.Where(x => x.ProductId == product.ProductId).FirstOrDefault();
            if (productToRemove == null)
            {
                return;
            }
            _products.Remove(productToRemove);
        }

        public void ClearCart()
        {
            _products.Clear();
        }

        public IEnumerable<Product> Products
        {
            get { return _products; }
        }
    }
}