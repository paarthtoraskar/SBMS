using SBMS.Models;
using System.Linq;
using System.Web.Mvc;

namespace SBMS.Controllers
{
    public class CustomerController : Controller
    {
        private readonly SBMSDbContext _db = new SBMSDbContext();

        // GET: /Customer/

        public ActionResult Index()
        {
            return View(_db.Customers.ToList());
        }

        // GET: /Customer/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    Customer customer = _db.Customers.Find(id);
        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(customer);
        //}

        // GET: /Customer/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: /Customer/Create

        //[HttpPost]
        //public ActionResult Create(Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Customers.Add(customer);
        //        _db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(customer);
        //}

        // GET: /Customer/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    Customer customer = _db.Customers.Find(id);
        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(customer);
        //}

        // POST: /Customer/Edit/5

        //[HttpPost]
        //public ActionResult Edit(Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Entry(customer).State = EntityState.Modified;
        //        _db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(customer);
        //}

        // GET: /Customer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Customer customer = _db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: /Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = _db.Customers.Find(id);
            _db.Customers.Remove(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}