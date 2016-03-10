using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using SBMS.Models;
using WebMatrix.WebData;

namespace SBMS.Controllers
{
    public class OrderController : Controller
    {
        private readonly SBMSDbContext _db = new SBMSDbContext();

        // GET: /Order/

        public ActionResult Index()
        {
            return View(_db.Orders.ToList());
        }

        // GET: /Order/Details/5

        public ActionResult Details(int id = 0)
        {
            Order order = _db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: /Order/Create

        public ActionResult Create()
        {
            if (!WebSecurity.IsAuthenticated)
            {
                //Response.Redirect("~/Account/Login/");
                //RedirectToAction("Login", "Account", "~/Order/Create");
                Response.Redirect("~/Account/Login/?ReturnUrl=/Order/Create");
            }

            ViewBag.CardTypeName = new SelectList(CardTypeEnumList(), "Value", "Text");
            return View();
        }

        // returns a SelectList for populating the Card Type combobox
        private IEnumerable<SelectListItem> CardTypeEnumList()
        {
            string[] names = Enum.GetNames(typeof(Order.CardType));
            Array values = Enum.GetValues(typeof(Order.CardType));
            IList<SelectListItem> items = new List<SelectListItem>();
            for (int i = 0; i < names.Length; i++)
            {
                var val = (int)values.GetValue(i);
                items.Add(new SelectListItem
                {
                    Text = names[i],
                    Value = val.ToString(CultureInfo.InvariantCulture)
                });
            }
            return items;
        }

        // POST: /Order/Create

        [HttpPost]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                SaveCartOnOrderPlaced();

                order.CartId = Session["CartId"] as string;
                order.UserId = WebSecurity.CurrentUserId;
                order.Username = WebSecurity.CurrentUserName;
                order.TotalOrderPayment = (decimal)Session["TotalCartPayment"];

                _db.Orders.Add(order);
                _db.SaveChanges();
                return RedirectToAction("OrderPlaced");
            }

            return View(order);
        }

        public void SaveCartOnOrderPlaced()
        {
            string cartId = Session["CartId"].ToString();

            var productsInCart = Session["ProductsInCart"] as List<Product>;

            if (productsInCart != null)
                foreach (Product product in productsInCart)
                {
                    var newCart = new Cart { CartId = cartId, ProductId = product.ProductId };
                    _db.Carts.Add(newCart);
                    _db.SaveChanges();
                }
        }

        // GET: /Order/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Order order = _db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /Order/Edit/5

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(order).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: /Order/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Order order = _db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /Order/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = _db.Orders.Find(id);
            _db.Orders.Remove(order);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult OrderPlaced()
        {
            return View("OrderPlacedView");
        }
    }
}