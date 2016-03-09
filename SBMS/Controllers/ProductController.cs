using SBMS.Models;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SBMS.Controllers
{
    public class ProductController : Controller
    {
        private SBMSDbContext db = new SBMSDbContext();

        // GET: /Product/

        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: /Product/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Product/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Product/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        // Customer viewable section

        public ActionResult Catalog()
        {
            if (db.Products.ToList().Count == 0)
            {
                db.InitializeProductsOnStartup();
                db.SaveChanges();
            }

            return View(db.Products.ToList());
        }

        public ActionResult JustPushPlay(Product jpp)
        {
            return View(jpp);
        }

        public ActionResult PicturePerfect(Product pp)
        {
            return View(pp);
        }

        public ActionResult MovieMan(Product mm)
        {
            return View(mm);
        }

        public ActionResult CompressIt(Product ci)
        {
            return View(ci);
        }

        public ActionResult CleanAll(Product ca)
        {
            return View(ca);
        }

        public ActionResult StartUtility()
        {
            string pathInAPIDomain = Server.MapPath("~");
            var appRoot = Directory.GetParent(Directory.GetParent(pathInAPIDomain).ToString()).ToString();
            var utilityFileName = appRoot + "\\Client\\bin\\Debug\\" + "WpfApp.exe";

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = utilityFileName;

            if (!System.IO.File.Exists(utilityFileName))
                //return RedirectToAction("About", "Home", null);
                RedirectToAction("Catalog", "Product", null);

            Process utility = new Process();
            utility.StartInfo = psi;
            utility.Start();

            return RedirectToAction("Catalog", "Product", null);
        }
    }

    //public static class HtmlExtensions
    //{
    //  public static void StartUtility()
    //  {
    //  }
    //}
}