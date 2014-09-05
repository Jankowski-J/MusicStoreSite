using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStoreSite.Models.Entities
{
    public class ShoppingCart
    {
        readonly List<CartItem> _products = new List<CartItem>();

        public void AddItem(Product product)
        {
            if (product == null)
            {
                return;
            }

            var foundItem = _products.Find(p => p.Product.ProductId == product.ProductId);
            if (foundItem != null)
            {
                foundItem.Quantity++;
            }
            else
            {
                _products.Add(new CartItem() { Product = product, Quantity = 1 });
            }
        }

        public void RemoveItem(int productId)
        {
            var productToRemove = _products.Where(x => x.Product.ProductId == productId).FirstOrDefault();
            if (productToRemove == null)
            {
                return;
            }

            if (productToRemove.Quantity > 1)
            {
                productToRemove.Quantity--;
            }
            else
            {
                _products.Remove(productToRemove);
            }
        }

        public void ClearCart()
        {
            _products.Clear();
        }

        public IEnumerable<CartItem> Products
        {
            get { return _products; }
        }
    }
}