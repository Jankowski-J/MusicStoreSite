﻿using System;
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
            var browseResults = musicStoreContext.Products.Where(x => x.Genre == category).ToList();
            ViewBag.Category = category + " albums";
            
            if (category == 0) //Temporary for testing at bigger amount of albums
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

        [HttpPost]
        public ActionResult GetCartMiniInfo()
        {
            var stringView = RenderRazorViewToString("CartMiniInfo", GetCart());
            return Json(new
            {
                view = stringView 
            });
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
