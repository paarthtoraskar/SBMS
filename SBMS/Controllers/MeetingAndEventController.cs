using System.Data;
using System.Linq;
using System.Web.Mvc;
using SBMS.Models;

namespace SBMS.Controllers
{
    public class MeetingAndEventController : Controller
    {
        private readonly SBMSDbContext _db = new SBMSDbContext();

        // GET: /MeetingAndEvent/

        public ActionResult Index()
        {
            return View(_db.MeetingAndEvents.ToList());
        }

        // GET: /MeetingAndEvent/Details/5

        public ActionResult Details(int id = 0)
        {
            MeetingAndEvent meetingandevent = _db.MeetingAndEvents.Find(id);
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
                _db.MeetingAndEvents.Add(meetingandevent);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meetingandevent);
        }

        // GET: /MeetingAndEvent/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MeetingAndEvent meetingandevent = _db.MeetingAndEvents.Find(id);
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
                _db.Entry(meetingandevent).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meetingandevent);
        }

        // GET: /MeetingAndEvent/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MeetingAndEvent meetingandevent = _db.MeetingAndEvents.Find(id);
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
            MeetingAndEvent meetingandevent = _db.MeetingAndEvents.Find(id);
            _db.MeetingAndEvents.Remove(meetingandevent);
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