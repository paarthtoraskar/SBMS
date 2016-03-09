using SBMS.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBMS.Controllers
{
    public class CartController : Controller
    {
        private SBMSDbContext db = new SBMSDbContext();

        // GET: /Cart/

        public ActionResult Index()
        {
            var carts = db.Carts.Include(c => c.Product);
            return View(carts.ToList());
        }

        // GET: /Cart/Details/5

        public ActionResult Details(int id = 0)
        {
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            cart.Product = db.Products.Find(cart.ProductId);
            return View(cart);
        }

        // GET: /Cart/Create

        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            return View();
        }

        // POST: /Cart/Create

        [HttpPost]
        public ActionResult Create(Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", cart.ProductId);
            return View(cart);
        }

        // GET: /Cart/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", cart.ProductId);
            return View(cart);
        }

        // POST: /Cart/Edit/5

        [HttpPost]
        public ActionResult Edit(Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", cart.ProductId);
            return View(cart);
        }

        // GET: /Cart/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: /Cart/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult AddToCart(int id = 0)
        {
            Product productFound = db.Products.Find(id);
            if (productFound == null)
            {
                return HttpNotFound();
            }
            List<Product> products = ((List<Product>)(HttpContext.Session["ProductsInCart"]));
            if (products.Find(productToBeAdded => productToBeAdded.ProductId == productFound.ProductId) == null)
                products.Add(productFound);
            else
                return View("~/Views/Product/Catalog.cshtml", db.Products.ToList());

            decimal totalCartPayment = (decimal)Session["TotalCartPayment"];
            totalCartPayment += productFound.Price;
            Session["TotalCartPayment"] = totalCartPayment;

            return View("~/Views/Product/Catalog.cshtml", db.Products.ToList());
        }

        public ActionResult ViewCart()
        {
            List<Product> products = ((List<Product>)(HttpContext.Session["ProductsInCart"]));
            return View("~/Views/Cart/CartView.cshtml", products);
        }

        public ActionResult RemoveFromCart(int id = 0)
        {
            Product productFound = db.Products.Find(id);
            if (productFound == null)
            {
                return HttpNotFound();
            }
            List<Product> products = ((List<Product>)(HttpContext.Session["ProductsInCart"]));
            products.RemoveAll(productToBeRemoved => productToBeRemoved.ProductId == productFound.ProductId);

            decimal totalCartPayment = (decimal)Session["TotalCartPayment"];
            totalCartPayment -= productFound.Price;
            Session["TotalCartPayment"] = totalCartPayment;

            return View("~/Views/Cart/CartView.cshtml", products);
        }
    }
}