using System.Web.Mvc;

namespace SBMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

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
    }
}