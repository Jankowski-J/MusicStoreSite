using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MusicStoreSite.Models;
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
            ProductViewModel selectedProduct = new ProductViewModel(id);

            return View(selectedProduct);
        }

        public ActionResult Browse(int category = 0)
        {
            var browseResults = new List<Product>();

            if (category != 0)
            {
                browseResults = musicStoreContext.Products.Where(x => x.GenreId == category).ToList();
                var genre = musicStoreContext.Genres.Where(x => x.GenreId == category).FirstOrDefault();
                ViewBag.Category = genre.Name + " albums:";
            }
            else //Temporary for testing at bigger amount of albums
            {
                browseResults = musicStoreContext.Products.ToList();
                ViewBag.Category = "All albums";
            }

            return View(browseResults);
        }

        public ActionResult AddProductDialog(int? poductId)
        {
            if (poductId == null)
            {
                ViewBag.ErrorCode = 403;
                ViewBag.ErrorMessage = "No productId was specified in request";
                return View("Error");
            }
            var cart = GetCart();
            ViewBag.Message = "ERROR";
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

        [HttpPost]
        public ActionResult GetCartMiniInfo()
        {
            var stringView = RenderRazorViewToString("CartMiniInfo", GetCart());
            return Json(new
            {
                view = stringView
            });
        }

        public ActionResult ShoppingCart()
        {
            var cart = GetCart();
            return View(cart);
        }

        public ActionResult RemoveFromCart(int productId)
        {
            var cart = GetCart();
            cart.RemoveItem(productId);
            return View("ShoppingCart", cart);
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public ActionResult CheckoutScreen()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CheckoutScreen(Order order)
        {
            if (ModelState.IsValid)
            {
                int? orderIndex = null;
                order.TotalPrice = 0.0M;

                musicStoreContext.Orders.Add(order);

                musicStoreContext.SaveChanges();

                orderIndex = order.OrderId;

                List<OrderItem> items = new List<OrderItem>();

                foreach (var item in GetCart().Products)
                {
                    var orderItem = new OrderItem() { OrderId = (int)orderIndex, ProductId = item.ProductId, Quantity = 1 };

                    var foundItem = items.Find(i => i.OrderId == orderIndex && i.ProductId == item.ProductId);
                    if (foundItem != null)
                    {
                        foundItem.Quantity++;
                    }
                    else
                    {
                        items.Insert(0, orderItem);
                    }
                    order.TotalPrice += item.Price;
                }

                foreach (var item in items)
                {
                    musicStoreContext.OrderItems.Add(item);
                }

                musicStoreContext.Orders.Where(o => o.OrderId == order.OrderId).FirstOrDefault().TotalPrice = order.TotalPrice;
                musicStoreContext.SaveChanges();

                ViewBag.OrderIndex = orderIndex;

                Session["Cart"] = new ShoppingCart();

                return View("OrderInfo");
            }

            return View(order);
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

        string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
