using SBMS.Models;
using System.Linq;
using System.Web.Mvc;

namespace SBMS.Controllers
{
    public class ProjectController : Controller
    {
        private readonly SBMSDbContext _db = new SBMSDbContext();
        private readonly DetailsForCustomerViewModel _detailsForCustViewModel = new DetailsForCustomerViewModel();

        // GET: /Project/

        public ActionResult Index()
        {
            return View(_db.Projects.ToList());
        }

        // GET: /Project/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    Project project = _db.Projects.Find(id);
        //    if (project == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(project);
        //}

        // GET: /Project/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: /Project/Create

        //[HttpPost]
        //public ActionResult Create(Project project)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Projects.Add(project);
        //        _db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(project);
        //}

        // GET: /Project/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    Project project = _db.Projects.Find(id);
        //    if (project == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(project);
        //}

        // POST: /Project/Edit/5

        //[HttpPost]
        //public ActionResult Edit(Project project)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.Entry(project).State = EntityState.Modified;
        //        _db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(project);
        //}

        // GET: /Project/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Project project = _db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Project/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = _db.Projects.Find(id);
            _db.Projects.Remove(project);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult DetailsForCustomerView()
        {
            _detailsForCustViewModel.PopulateFromDbContext(_db);
            return View(_detailsForCustViewModel);
        }
    }
}