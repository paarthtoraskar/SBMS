using SBMS.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SBMS.Controllers
{
    public class CustomerIssueController : Controller
    {
        private readonly SBMSDbContext _db = new SBMSDbContext();

        // GET: /CustomerIssue/

        public ActionResult Index()
        {
            IQueryable<CustomerIssue> customerIssues = _db.CustomerIssues.Include(c => c.Customer);
            return View(customerIssues.ToList());
        }

        // GET: /CustomerIssue/Details/5

        public ActionResult Details(int id = 0)
        {
            CustomerIssue customerIssue = _db.CustomerIssues.Find(id);
            if (customerIssue == null)
            {
                return HttpNotFound();
            }
            customerIssue.Customer = _db.Customers.Find(customerIssue.CustomerId);
            return View(customerIssue);
        }

        // GET: /CustomerIssue/Create

        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(_db.Customers, "CustomerId", "Username");
            return View();
        }

        // POST: /CustomerIssue/Create

        [HttpPost]
        public ActionResult Create(CustomerIssue customerIssue)
        {
            if (ModelState.IsValid)
            {
                _db.CustomerIssues.Add(customerIssue);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(_db.Customers, "CustomerId", "Username");
            return View(customerIssue);
        }

        // GET: /CustomerIssue/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CustomerIssue customerIssue = _db.CustomerIssues.Find(id);
            if (customerIssue == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(_db.Customers, "CustomerId", "Username");
            return View(customerIssue);
        }

        // POST: /CustomerIssue/Edit/5

        [HttpPost]
        public ActionResult Edit(CustomerIssue customerIssue)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(customerIssue).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(_db.Customers, "CustomerId", "Username");
            return View(customerIssue);
        }

        // GET: /CustomerIssue/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CustomerIssue customerIssue = _db.CustomerIssues.Find(id);
            if (customerIssue == null)
            {
                return HttpNotFound();
            }
            customerIssue.Customer = _db.Customers.Find(customerIssue.CustomerId);
            return View(customerIssue);
        }

        // POST: /CustomerIssue/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerIssue customerIssue = _db.CustomerIssues.Find(id);
            _db.CustomerIssues.Remove(customerIssue);
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