using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDongHo.Models.Entities;

namespace ShopDongHo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                using (var db = new ShopDbContext())
                {
                    ViewBag.DbStatus = "✅ Connected to Database";
                    ViewBag.CategoryCount = db.Categories.Count();
                    ViewBag.ProductCount = db.Products.Count();
                    ViewBag.UserCount = db.Users.Count();
                }
            }
            catch (Exception ex)
            {
                ViewBag.DbStatus = "❌ Error: " + ex.Message;
                ViewBag.CategoryCount = 0;
                ViewBag.ProductCount = 0;
                ViewBag.UserCount = 0;
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}