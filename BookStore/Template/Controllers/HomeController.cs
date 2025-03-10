using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Template.Models;
namespace Template.Controllers
{
    public class HomeController : Controller
    {
        dbStore dbLABEntities = new dbStore();
        // GET: Home
        public ActionResult Index(string language= "Hepsi")
        {
            if(language.Equals("Hepsi"))
            {
                ViewBag.total = dbLABEntities.Books.Select(a => a.Price).Sum();
                return View(dbLABEntities.Books.OrderBy(a => a.Price).ToList());
            }
            else
            {
                ViewBag.total = dbLABEntities.Books.Where(a => a.Language.Equals(language)).Select(a => a.Price).Sum();
                return View(dbLABEntities.Books.Where(a => a.Language.Equals(language)).OrderBy(a => a.Price).ToList());
            }

        }
        public ActionResult Login()
        {
            ViewBag.error = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            User loginUser = dbLABEntities.Users.Where(a => a.Username.Equals(username) && a.Password.Equals(password)).FirstOrDefault();
            if (loginUser != null)
                return RedirectToAction("Index");
            ViewBag.error = "Hatalı giriş";
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Book newBook)
        {
            newBook.PubDate = DateTime.Now;
            dbLABEntities.Books.Add(newBook);
            dbLABEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Remove(int BookId)
        {
            Book deleted = dbLABEntities.Books.Find(BookId);
            dbLABEntities.Books.Remove(deleted);
            dbLABEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Update(int BookId)
        {
            Book updated = dbLABEntities.Books.Find(BookId);
            return View(updated);
        }
        [HttpPost]
        public ActionResult Update(int BookId, string BookName, string Author, string Language, double Price)
        {
            Book updated = dbLABEntities.Books.Find(BookId);
            updated.BookName = BookName;
            updated.Author = Author;
            updated.Language = Language;
            updated.Price = Price;
            dbLABEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}