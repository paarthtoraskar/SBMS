using SBMS.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace SBMS.Controllers
{
    public class MeetingAndEventController : Controller
    {
        private SBMSDbContext db = new SBMSDbContext();

        // GET: /MeetingAndEvent/

        public ActionResult Index()
        {
            return View(db.MeetingAndEvents.ToList());
        }

        // GET: /MeetingAndEvent/Details/5

        public ActionResult Details(int id = 0)
        {
            MeetingAndEvent meetingandevent = db.MeetingAndEvents.Find(id);
            if (meetingandevent == null)
            {
                return HttpNotFound();
            }
            return View(meetingandevent);
        }

        // GET: /MeetingAndEvent/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: /MeetingAndEvent/Create

        [HttpPost]
        public ActionResult Create(MeetingAndEvent meetingandevent)
        {
            if (ModelState.IsValid)
            {
                db.MeetingAndEvents.Add(meetingandevent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meetingandevent);
        }

        // GET: /MeetingAndEvent/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MeetingAndEvent meetingandevent = db.MeetingAndEvents.Find(id);
            if (meetingandevent == null)
            {
                return HttpNotFound();
            }
            return View(meetingandevent);
        }

        // POST: /MeetingAndEvent/Edit/5

        [HttpPost]
        public ActionResult Edit(MeetingAndEvent meetingandevent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meetingandevent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meetingandevent);
        }

        // GET: /MeetingAndEvent/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MeetingAndEvent meetingandevent = db.MeetingAndEvents.Find(id);
            if (meetingandevent == null)
            {
                return HttpNotFound();
            }
            return View(meetingandevent);
        }

        // POST: /MeetingAndEvent/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MeetingAndEvent meetingandevent = db.MeetingAndEvents.Find(id);
            db.MeetingAndEvents.Remove(meetingandevent);
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