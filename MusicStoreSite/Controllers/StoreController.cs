using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStoreSite.Models.Entities;

namespace MusicStoreSite.Controllers
{
    public class StoreController : Controller
    {
        //
        // GET: /Store/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductDetails(int id = 0)
        {
            Product product = new Product() { Price = 10.0M, AddedAt = DateTime.Now, Artist = "Ozzy Osbourne", ProductId = 1001, Genre = "Rock", Title = "Paranoid" };

            return View(product);
        }
    }
}
