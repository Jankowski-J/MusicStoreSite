using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStoreSite.Models.Entities;
using MusicStoreSite.Models.Contexts;
using MusicStoreSite.Models;
using MusicStoreSite.Infrastructure;

namespace MusicStoreSite.Areas.Panel.Controllers
{
    [AdminAuthorizationAttribute(Users="admin")]
    public class AdminController : Controller
    {
        private MusicStoreContext db = new MusicStoreContext();

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            var productsViewModels = new List<ProductViewModel>();
            foreach (var product in db.Products)
            {
                productsViewModels.Add(new ProductViewModel(product.ProductId));
            }
            return View(productsViewModels);
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase albumCover)
        {
            if (ModelState.IsValid)
            {
                if (albumCover != null && albumCover.ContentLength > 0)
                    try
                    {
                        string path = Server.MapPath("~/Content/Images/AlbumCovers") + '\\' + product.Artist + '_' + product.Title + '_' + product.AddedAt.ToString("dd_MM_yyyy") + Path.GetExtension(albumCover.FileName);
                        albumCover.SaveAs(path);
                        product.CoverLocation = product.Artist + '_' + product.Title + '_' + product.AddedAt.ToString("dd_MM_yyyy") + Path.GetExtension(albumCover.FileName);
                        ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "You have not specified a file.";
                } 
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase albumCover)
        {
            if (ModelState.IsValid)
            {
                if (albumCover != null && albumCover.ContentLength > 0)
                    try
                    {
                        string path = Server.MapPath("~/Content/Images/AlbumCovers") + '\\' + product.Artist + '_' + product.Title + '_' + product.AddedAt.ToString("dd_MM_yyyy") + Path.GetExtension(albumCover.FileName);
                        albumCover.SaveAs(path);
                        product.CoverLocation = product.Artist + '_' + product.Title + '_' + product.AddedAt.ToString("dd_MM_yyyy") + Path.GetExtension(albumCover.FileName);
                        ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "You have not specified a file.";
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(new ProductViewModel(product.ProductId));
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetGenres(int? selectedGenre = null)
        {
            if (selectedGenre == null)
            {
                return PartialView("_GenresDropDown", db.Genres);
            }
            else
            {
                ViewBag.SelectedGenre = selectedGenre.Value;
                return PartialView("_GenresDropDownPreselected", db.Genres);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}