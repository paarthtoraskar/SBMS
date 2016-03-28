using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SBMS.Models;

namespace SBMS.Controllers
{
    public class CartController : Controller
    {
        private readonly SBMSDbContext _db = new SBMSDbContext();

        // GET: /Cart/

        public ActionResult Index()
        {
            IQueryable<Cart> carts = _db.Carts.Include(c => c.Product);
            return View(carts.ToList());
        }

        // GET: /Cart/Details/5

        public ActionResult Details(int id = 0)
        {
            Cart cart = _db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            cart.Product = _db.Products.Find(cart.ProductId);
            return View(cart);
        }

        // GET: /Cart/Create

        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name");
            return View();
        }

        // POST: /Cart/Create

        [HttpPost]
        public ActionResult Create(Cart cart)
        {
            if (ModelState.IsValid)
            {
                _db.Carts.Add(cart);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name");
            return View(cart);
        }

        // GET: /Cart/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Cart cart = _db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name");
            return View(cart);
        }

        // POST: /Cart/Edit/5

        [HttpPost]
        public ActionResult Edit(Cart cart)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(cart).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name");
            return View(cart);
        }

        // GET: /Cart/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Cart cart = _db.Carts.Find(id);
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
            Cart cart = _db.Carts.Find(id);
            _db.Carts.Remove(cart);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult AddToCart(int id = 0)
        {
            Product productFound = _db.Products.Find(id);
            if (productFound == null)
            {
                return HttpNotFound();
            }
            var products = ((List<Product>) (HttpContext.Session["ProductsInCart"]));
            if (products.Find(productToBeAdded => productToBeAdded.ProductId == productFound.ProductId) == null)
                products.Add(productFound);
            else
                return View("~/Views/Product/Catalog.cshtml", _db.Products.ToList());

            var totalCartPayment = (decimal) Session["TotalCartPayment"];
            totalCartPayment += productFound.Price;
            Session["TotalCartPayment"] = totalCartPayment;

            return View("~/Views/Product/Catalog.cshtml", _db.Products.ToList());
        }

        public ActionResult ViewCart()
        {
            var products = ((List<Product>) (HttpContext.Session["ProductsInCart"]));
            return View("~/Views/Cart/CartView.cshtml", products);
        }

        public ActionResult RemoveFromCart(int id = 0)
        {
            Product productFound = _db.Products.Find(id);
            if (productFound == null)
            {
                return HttpNotFound();
            }
            var products = ((List<Product>) (HttpContext.Session["ProductsInCart"]));
            products.RemoveAll(productToBeRemoved => productToBeRemoved.ProductId == productFound.ProductId);

            var totalCartPayment = (decimal) Session["TotalCartPayment"];
            totalCartPayment -= productFound.Price;
            Session["TotalCartPayment"] = totalCartPayment;

            return View("~/Views/Cart/CartView.cshtml", products);
        }
    }
}