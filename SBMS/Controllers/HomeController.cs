using System.Web.Mvc;

namespace SBMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "Know more about who we are and what we do.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Get in touch with us.";

            return View();
        }

        public ActionResult EmployeeLanding()
        {
            return View();
        }
    }
}