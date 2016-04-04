using SBMS.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SBMS.Controllers
{
    public class ProjectIssueController : Controller
    {
        private readonly SBMSDbContext _db = new SBMSDbContext();

        // GET: /ProjectIssue/

        public ActionResult Index()
        {
            IQueryable<ProjectIssue> projectIssues = _db.ProjectIssues.Include(c => c.Project);
            return View(projectIssues.ToList());
        }

        // GET: /ProjectIssue/Details/5

        public ActionResult Details(int id = 0)
        {
            ProjectIssue projectIssue = _db.ProjectIssues.Find(id);
            if (projectIssue == null)
            {
                return HttpNotFound();
            }
            projectIssue.Project = _db.Projects.Find(projectIssue.ProjectId);
            return View(projectIssue);
        }

        // GET: /ProjectIssue/Create

        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(_db.Projects, "ProjectId", "ProjectTitle");
            return View();
        }

        // POST: /ProjectIssue/Create

        [HttpPost]
        public ActionResult Create(ProjectIssue projectIssue)
        {
            if (ModelState.IsValid)
            {
                _db.ProjectIssues.Add(projectIssue);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractProposalId = new SelectList(_db.Contracts, "ContractProposalId", "Title",
                projectIssue.ProjectId);
            return View(projectIssue);
        }

        // GET: /ProjectIssue/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProjectIssue projectIssue = _db.ProjectIssues.Find(id);
            if (projectIssue == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractProposalId = new SelectList(_db.Contracts, "ContractProposalId", "Title",
                projectIssue.ProjectId);
            return View(projectIssue);
        }

        // POST: /ProjectIssue/Edit/5

        [HttpPost]
        public ActionResult Edit(ProjectIssue projectIssue)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(projectIssue).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractProposalId = new SelectList(_db.Contracts, "ContractProposalId", "Title",
                projectIssue.ProjectId);
            return View(projectIssue);
        }

        // GET: /ProjectIssue/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProjectIssue projectIssue = _db.ProjectIssues.Find(id);
            if (projectIssue == null)
            {
                return HttpNotFound();
            }
            projectIssue.Project = _db.Projects.Find(projectIssue.ProjectId);
            return View(projectIssue);
        }

        // POST: /ProjectIssue/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectIssue projectIssue = _db.ProjectIssues.Find(id);
            _db.ProjectIssues.Remove(projectIssue);
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