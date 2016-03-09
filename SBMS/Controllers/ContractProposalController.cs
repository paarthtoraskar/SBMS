using SBMS.Models;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace SBMS.Controllers
{
    public class ContractProposalController : Controller
    {
        private SBMSDbContext db = new SBMSDbContext();

        // GET: /ContractProposal/

        public ActionResult Index()
        {
            return View(db.Contracts.ToList());
        }

        // GET: /ContractProposal/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //  ContractProposal contract = db.Contracts.Find(id);
        //  if (contract == null)
        //  {
        //    return HttpNotFound();
        //  }
        //  return View(contract);
        //}

        public ActionResult Details(int id = 0, string headerMessage = "")
        {
            ContractProposal contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            if (headerMessage != string.Empty)
                ViewBag.HeaderMessage = headerMessage;

            return View(contract);
        }

        // GET: /ContractProposal/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: /ContractProposal/Create

        [HttpPost]
        public ActionResult Create(ContractProposal contract)
        {
            if (ModelState.IsValid)
            {
                if (contract.StartDate < DateTime.Today)
                {
                    ModelState.AddModelError("StartDate", "The Start Date should be later than today.");
                    return View(contract);
                }
                if (contract.EndDate < DateTime.Today)
                {
                    ModelState.AddModelError("EndDate", "The End Date should be later than today.");
                    return View(contract);
                }
                if (contract.StartDate > contract.EndDate)
                {
                    ModelState.AddModelError("StartDate", "The Start Date should be earlier than the End Date.");
                    return View(contract);
                }

                contract.UserId = WebSecurity.CurrentUserId;
                contract.Username = WebSecurity.CurrentUserName;

                db.Contracts.Add(contract);
                db.SaveChanges();
                //return RedirectToAction("Index");

                return RedirectToAction("Details", "ContractProposal", new { id = contract.ContractProposalId, headerMessage = "Created now!" });
            }

            return View(contract);
        }

        // GET: /ContractProposal/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ContractProposal contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }

            Session["OriginalContract"] = contract;

            return View(contract);
        }

        // POST: /ContractProposal/Edit/5

        [HttpPost]
        public ActionResult Edit(ContractProposal contract)
        {
            ContractProposal originalContract = Session["OriginalContract"] as ContractProposal;

            if (!originalContract.Approved)
            {
                if (contract.StartDate < DateTime.Today)
                {
                    ModelState.AddModelError("StartDate", "The Start Date should be later than today.");
                    return View(contract);
                }
                if (contract.EndDate < DateTime.Today)
                {
                    ModelState.AddModelError("EndDate", "The End Date should be later than today.");
                    return View(contract);
                }
                if (contract.StartDate > contract.EndDate)
                {
                    ModelState.AddModelError("StartDate", "The Start Date should be earlier than the End Date.");
                    return View(contract);
                }
            }

            contract.UserId = originalContract.UserId;
            contract.Username = originalContract.Username;

            if (originalContract.Approved)
                LoadUneditableValues(originalContract, contract);

            db.Entry(contract).State = EntityState.Modified;
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("Details", "ContractProposal", new { id = contract.ContractProposalId });
        }

        private void LoadUneditableValues(ContractProposal originalContract, ContractProposal editedContract)
        {
            editedContract.Approved = originalContract.Approved;
            editedContract.Title = originalContract.Title;
            editedContract.Description = originalContract.Description;
            editedContract.StartDate = originalContract.StartDate;
            editedContract.EndDate = originalContract.EndDate;
            editedContract.Budget = originalContract.Budget;
        }

        // GET: /ContractProposal/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ContractProposal contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: /ContractProposal/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ContractProposal contract = db.Contracts.Find(id);
            db.Contracts.Remove(contract);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public RedirectToRouteResult SelectViewBasedOnContractStatus()
        {
            ContractProposal thisUsersContract;
            thisUsersContract = (from contract in db.Contracts
                                 where
                                   (contract.UserId == WebSecurity.CurrentUserId)
                                 select contract).FirstOrDefault();

            if (thisUsersContract == null)
                return RedirectToAction("WelcomeContractView", "ContractProposal");
            else if (thisUsersContract != null && !thisUsersContract.Approved)
                return RedirectToAction("Details", "ContractProposal", new { id = thisUsersContract.ContractProposalId });
            else if (thisUsersContract != null && thisUsersContract.Approved)
                return RedirectToAction("ContractOrProjectView", "ContractProposal", new { id = thisUsersContract.ContractProposalId });
            else
                return RedirectToAction("HttpNotFoundView", "ContractProposal");
        }

        public ActionResult WelcomeContractView()
        {
            return View();
        }

        public ActionResult ContractOrProjectView(int id = 0, string headerMessage = "")
        {
            ContractProposal contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }

            return View(contract);
        }

        public ActionResult HttpNotFoundView()
        {
            return HttpNotFound();
        }

        public ActionResult ApproveContractCreateProject(int id = 0)
        {
            ContractProposal contract = db.Contracts.Find(id);
            if (contract == null)
                return HttpNotFound();

            // approve this contract
            contract.Approved = true;

            // create new Customer
            Customer newCustomer = new Customer();
            // get all properties for this Customer except ProjectId
            newCustomer.ExtractRelevantData(contract);
            db.Customers.Add(newCustomer);
            db.SaveChanges();

            // create new project
            Project newProject = new Project();
            // get all properties for this project
            newProject.ExtractRelevantData(newCustomer);
            db.Projects.Add(newProject);
            db.SaveChanges();

            // get ProjectId for this Customer
            newCustomer.ExtractRelevantData(newProject);

            db.SaveChanges();

            return View("Index", db.Contracts.ToList());
        }
    }
}