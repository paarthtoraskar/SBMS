using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SBMS.Models;

namespace SBMS.Controllers
{
    public class ProjectIssueController : Controller
    {
        private SBMSDbContext db = new SBMSDbContext();

        // GET: /ProjectIssue/

        public ActionResult Index()
        {
            var projectIssues = db.ProjectIssues.Include(c => c.Project);
            return View(projectIssues.ToList());
        }

        // GET: /ProjectIssue/Details/5

        public ActionResult Details(int id = 0)
        {
            ProjectIssue projectIssue = db.ProjectIssues.Find(id);
            projectIssue.Project = db.Projects.Find(projectIssue.ProjectId);
            if (projectIssue == null)
            {
                return HttpNotFound();
            }
            return View(projectIssue);
        }

        // GET: /ProjectIssue/Create

        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            return View();
        }

        // POST: /ProjectIssue/Create

        [HttpPost]
        public ActionResult Create(ProjectIssue projectIssue)
        {
            if (ModelState.IsValid)
            {
                db.ProjectIssues.Add(projectIssue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractProposalId = new SelectList(db.Contracts, "ContractProposalId", "Title", projectIssue.ProjectId);
            return View(projectIssue);
        }

        // GET: /ProjectIssue/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProjectIssue projectIssue = db.ProjectIssues.Find(id);
            if (projectIssue == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractProposalId = new SelectList(db.Contracts, "ContractProposalId", "Title", projectIssue.ProjectId);
            return View(projectIssue);
        }

        // POST: /ProjectIssue/Edit/5

        [HttpPost]
        public ActionResult Edit(ProjectIssue projectIssue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectIssue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractProposalId = new SelectList(db.Contracts, "ContractProposalId", "Title", projectIssue.ProjectId);
            return View(projectIssue);
        }

        // GET: /ProjectIssue/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProjectIssue projectIssue = db.ProjectIssues.Find(id);
            projectIssue.Project = db.Projects.Find(projectIssue.ProjectId);
            if (projectIssue == null)
            {
                return HttpNotFound();
            }
            return View(projectIssue);
        }

        // POST: /ProjectIssue/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectIssue projectIssue = db.ProjectIssues.Find(id);
            db.ProjectIssues.Remove(projectIssue);
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