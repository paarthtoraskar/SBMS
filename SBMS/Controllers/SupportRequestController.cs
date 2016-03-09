using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SBMS.Models;

namespace SBMS.Controllers
{
    public class SupportRequestController : Controller
    {
        private SBMSDbContext db = new SBMSDbContext();

        // GET: /SupportRequest/

        public ActionResult Index()
        {
            var supportrequests = db.SupportRequests.Include(s => s.Product);
            return View(supportrequests.ToList());
        }

        // GET: /SupportRequest/Details/5

        public ActionResult Details(int id = 0)
        {
            SupportRequest supportrequest = db.SupportRequests.Find(id);
            supportrequest.Product = db.Products.Find(supportrequest.ProductId);
            if (supportrequest == null)
            {
                return HttpNotFound();
            }
            return View(supportrequest);
        }

        // GET: /SupportRequest/Create

        public ActionResult Create()
        {
            if (db.Products.ToList().Count == 0)
            {
                db.InitializeProductsOnStartup();
                db.SaveChanges();
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            return View();
        }

        // POST: /SupportRequest/Create

        [HttpPost]
        public ActionResult Create(SupportRequest supportrequest)
        {
            if (ModelState.IsValid)
            {
                db.SupportRequests.Add(supportrequest);
                db.SaveChanges();
                return RedirectToAction("SupportFormSubmitted");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", supportrequest.ProductId);
            return View(supportrequest);
        }

        // GET: /SupportRequest/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SupportRequest supportrequest = db.SupportRequests.Find(id);
            if (supportrequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", supportrequest.ProductId);
            return View(supportrequest);
        }

        // POST: /SupportRequest/Edit/5

        [HttpPost]
        public ActionResult Edit(SupportRequest supportrequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supportrequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", supportrequest.ProductId);
            return View(supportrequest);
        }

        // GET: /SupportRequest/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SupportRequest supportrequest = db.SupportRequests.Find(id);
            supportrequest.Product = db.Products.Find(supportrequest.ProductId);
            if (supportrequest == null)
            {
                return HttpNotFound();
            }
            return View(supportrequest);
        }

        // POST: /SupportRequest/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SupportRequest supportrequest = db.SupportRequests.Find(id);
            db.SupportRequests.Remove(supportrequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult SupportFormSubmitted()
        {
            return View();
        }
    }
}