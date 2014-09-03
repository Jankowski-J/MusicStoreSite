using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MusicStoreSite.Models.Contexts;
using MusicStoreSite.Models.Entities;

namespace MusicStoreSite.Controllers
{
    public class StoreController : Controller
    {
        //
        // GET: /Store/

        private MusicStoreContext musicStoreContext = new MusicStoreContext();

        public ViewResult Index()
        {
            var newProducts = musicStoreContext.Products.OrderByDescending(x => x.AddedAt).Take(5).AsEnumerable();
            return View(newProducts);
        }

        public ActionResult ProductDetails(int id = 0)
        {
            Product selectedProduct = musicStoreContext.Products.Where(product => product.ProductId == id).FirstOrDefault();

            return View(selectedProduct);
        }

        public ActionResult Browse(string category = "")
        {
            var browseResults = musicStoreContext.Products.Where(x => x.Genre == category).ToList();
            ViewBag.Category = category + " albums";
            
            if (category == "") //Temporary for testing at bigger amount of albums
            {
                browseResults = musicStoreContext.Products.ToList();
                ViewBag.Category = "All albums";
            }

            return View(browseResults);
        }

        public ActionResult AddProductDialog(int poductId)
        {
            var cart = GetCart();
            ViewBag.Message= "ERROR";
            var product = musicStoreContext.Products.Where(x => x.ProductId == poductId).FirstOrDefault();
            if (product != null)
            {
                cart.AddItem(product);
                ViewBag.Message = "Added";
            }

            return PartialView();
        }

        public ActionResult CartMiniInfo()
        {
            return PartialView(GetCart());
        }

        ShoppingCart GetCart()
        {
            var cart = (ShoppingCart)Session["Cart"];
            if (cart == null)
            {
                cart = new ShoppingCart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}
