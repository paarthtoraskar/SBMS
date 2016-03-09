using SBMS.Models;
using System.Linq;
using System.Web.Mvc;

namespace SBMS.Controllers
{
    public class ScheduleBudgetQueryController : Controller
    {
        private SBMSDbContext dbContext = new SBMSDbContext();

        [HttpGet]
        public ActionResult Query()
        {
            return View("~/Views/ContractProposal/Query.cshtml", new ContractProposal());
        }

        [HttpPost]
        public ActionResult Result(ContractProposal contract)
        {
            ContractProposal originalContract = (from thisContract in dbContext.Contracts
                                                 where thisContract.Username == contract.Username
                                                 select thisContract).FirstOrDefault();

            if (originalContract == null)
                ViewBag.Message = "This customer does not have a contract with us!";
            else
                ViewBag.Message = "Here is the info.";

            return View("~/Views/ContractProposal/Result.cshtml", originalContract);
        }
    }
}