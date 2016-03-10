using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SBMS.Models;

namespace SBMS.Controllers
{
    public class SupportRequestController : Controller
    {
        private readonly SBMSDbContext _db = new SBMSDbContext();

        // GET: /SupportRequest/

        public ActionResult Index()
        {
            IQueryable<SupportRequest> supportRequests = _db.SupportRequests.Include(s => s.Product);
            return View(supportRequests.ToList());
        }

        // GET: /SupportRequest/Details/5

        public ActionResult Details(int id = 0)
        {
            SupportRequest supportRequest = _db.SupportRequests.Find(id);
            if (supportRequest == null)
            {
                return HttpNotFound();
            }
            supportRequest.Product = _db.Products.Find(supportRequest.ProductId);
            return View(supportRequest);
        }

        // GET: /SupportRequest/Create

        public ActionResult Create()
        {
            if (_db.Products.ToList().Count == 0)
            {
                _db.InitializeProductsOnStartup();
                _db.SaveChanges();
            }

            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name");
            return View();
        }

        // POST: /SupportRequest/Create

        [HttpPost]
        public ActionResult Create(SupportRequest supportRequest)
        {
            if (ModelState.IsValid)
            {
                _db.SupportRequests.Add(supportRequest);
                _db.SaveChanges();
                return RedirectToAction("SupportFormSubmitted");
            }

            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name", supportRequest.ProductId);
            return View(supportRequest);
        }

        // GET: /SupportRequest/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SupportRequest supportRequest = _db.SupportRequests.Find(id);
            if (supportRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name", supportRequest.ProductId);
            return View(supportRequest);
        }

        // POST: /SupportRequest/Edit/5

        [HttpPost]
        public ActionResult Edit(SupportRequest supportRequest)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(supportRequest).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name", supportRequest.ProductId);
            return View(supportRequest);
        }

        // GET: /SupportRequest/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SupportRequest supportRequest = _db.SupportRequests.Find(id);
            if (supportRequest == null)
            {
                return HttpNotFound();
            }
            supportRequest.Product = _db.Products.Find(supportRequest.ProductId);
            return View(supportRequest);
        }

        // POST: /SupportRequest/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SupportRequest supportRequest = _db.SupportRequests.Find(id);
            _db.SupportRequests.Remove(supportRequest);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult SupportFormSubmitted()
        {
            return View();
        }
    }
}