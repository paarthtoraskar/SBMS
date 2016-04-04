using SBMS.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SBMS.Controllers
{
    public class MilestoneController : Controller
    {
        private readonly SBMSDbContext _db = new SBMSDbContext();

        // GET: /Milestone/

        public ActionResult Index()
        {
            IQueryable<Milestone> milestones = _db.Milestones.Include(m => m.Project);
            return View(milestones.ToList());
        }

        // GET: /Milestone/Details/5

        public ActionResult Details(int id = 0)
        {
            Milestone milestone = _db.Milestones.Find(id);
            if (milestone == null)
            {
                return HttpNotFound();
            }
            milestone.Project = _db.Projects.Find(milestone.ProjectId);
            return View(milestone);
        }

        // GET: /Milestone/Create

        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(_db.Projects, "ProjectId", "ProjectTitle");
            return View();
        }

        // POST: /Milestone/Create

        [HttpPost]
        public ActionResult Create(Milestone milestone)
        {
            if (ModelState.IsValid)
            {
                _db.Milestones.Add(milestone);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(_db.Projects, "ProjectId", "ProjectTitle");
            return View(milestone);
        }

        // GET: /Milestone/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Milestone milestone = _db.Milestones.Find(id);
            if (milestone == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(_db.Projects, "ProjectId", "ProjectTitle");
            return View(milestone);
        }

        // POST: /Milestone/Edit/5

        [HttpPost]
        public ActionResult Edit(Milestone milestone)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(milestone).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(_db.Projects, "ProjectId", "ProjectTitle");
            return View(milestone);
        }

        // GET: /Milestone/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Milestone milestone = _db.Milestones.Find(id);
            if (milestone == null)
            {
                return HttpNotFound();
            }
            milestone.Project = _db.Projects.Find(milestone.ProjectId);
            return View(milestone);
        }

        // POST: /Milestone/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Milestone milestone = _db.Milestones.Find(id);
            _db.Milestones.Remove(milestone);
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