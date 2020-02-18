using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Ajax.Models;

namespace MVC_Ajax.Controllers
{
    public class MainController : Controller
    {
        // GET: Main

        private readonly List<Book> db;
        public MainController()
        {
            db = new List<Book>()
            {
                new Book{Name="book1",Author="Nick Harson"},
                new Book{Name="book2",Author="Shild"},
                new Book{Name="C++",Author="Shild"},
                new Book{Name="book3",Author="Figa"},
                new Book{Name="book4",Author="Troelsen"},
                new Book{Name="book5",Author="Jimmy"}
            };
        }
        public ActionResult Index()
        {
            ViewBag.Authors = db.Select(z => z.Author).Distinct().OrderBy(z => z).ToList();

            //ViewBag.Authors = (from b in db
            //                   select b.Author
            //                   .Distinct()
            //                   .OrderBy(z => z)
            //                   .ToList());

            //ViewBag.Authors = db.Select(z => z.Author).ToList();
            return View();
        }

        public ActionResult BookSearch(string name)
        {
            var allbooks = db.Where(a => a.Author.Contains(name)).ToList();
            if (allbooks.Count <= 0)
            {
                return new HttpUnauthorizedResult();
            }
            return PartialView(allbooks);
        }
        public ActionResult BestBook()
        {
            var bestBook = db.FirstOrDefault();
            if (bestBook== null)
            {
                return new HttpUnauthorizedResult();
            }
            return PartialView(bestBook);
        }

        public JsonResult JsonSearch(string name)
        {
            var jsondata = db.Where(a => a.Author.Contains(name)).ToList<Book>();
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
    }
}