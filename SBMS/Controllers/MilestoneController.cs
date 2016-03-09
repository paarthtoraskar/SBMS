using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SBMS.Models;

namespace SBMS.Controllers
{
    public class MilestoneController : Controller
    {
        private SBMSDbContext db = new SBMSDbContext();

        // GET: /Milestone/

        public ActionResult Index()
        {
            var milestones = db.Milestones.Include(m => m.Project);
            return View(milestones.ToList());
        }

        // GET: /Milestone/Details/5

        public ActionResult Details(int id = 0)
        {
            Milestone milestone = db.Milestones.Find(id);
            milestone.Project = db.Projects.Find(milestone.ProjectId);
            if (milestone == null)
            {
                return HttpNotFound();
            }
            return View(milestone);
        }

        // GET: /Milestone/Create

        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            return View();
        }

        // POST: /Milestone/Create

        [HttpPost]
        public ActionResult Create(Milestone milestone)
        {
            if (ModelState.IsValid)
            {
                db.Milestones.Add(milestone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", milestone.ProjectId);
            return View(milestone);
        }

        // GET: /Milestone/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Milestone milestone = db.Milestones.Find(id);
            if (milestone == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", milestone.ProjectId);
            return View(milestone);
        }

        // POST: /Milestone/Edit/5

        [HttpPost]
        public ActionResult Edit(Milestone milestone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(milestone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", milestone.ProjectId);
            return View(milestone);
        }

        // GET: /Milestone/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Milestone milestone = db.Milestones.Find(id);
            milestone.Project = db.Projects.Find(milestone.ProjectId);
            if (milestone == null)
            {
                return HttpNotFound();
            }
            return View(milestone);
        }

        // POST: /Milestone/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Milestone milestone = db.Milestones.Find(id);
            db.Milestones.Remove(milestone);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}