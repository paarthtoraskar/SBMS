using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using SBMS.Models;
using WebMatrix.WebData;

namespace SBMS.Controllers
{
    public class ContractProposalController : Controller
    {
        private readonly SBMSDbContext _db = new SBMSDbContext();

        // GET: /ContractProposal/

        public ActionResult Index()
        {
            return View(_db.Contracts.ToList());
        }

        // GET: /ContractProposal/Details/5

        public ActionResult Details(int id = 0)
        {
            ContractProposal contract = _db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
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

                _db.Contracts.Add(contract);
                _db.SaveChanges();
                //return RedirectToAction("Index");

                return RedirectToAction("Details", "ContractProposal",
                    new { id = contract.ContractProposalId, headerMessage = "Created now!" });
            }

            return View(contract);
        }

        // GET: /ContractProposal/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ContractProposal contract = _db.Contracts.Find(id);
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
            var originalContract = Session["OriginalContract"] as ContractProposal;

            if (originalContract != null && !originalContract.Approved)
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

            if (originalContract != null)
            {
                contract.UserId = originalContract.UserId;
                contract.Username = originalContract.Username;

                if (originalContract.Approved)
                    LoadUneditableValues(originalContract, contract);
            }

            _db.Entry(contract).State = EntityState.Modified;
            _db.SaveChanges();
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
            ContractProposal contract = _db.Contracts.Find(id);
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
            ContractProposal contract = _db.Contracts.Find(id);
            _db.Contracts.Remove(contract);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        public RedirectToRouteResult SelectViewBasedOnContractStatus()
        {
            ContractProposal thisUsersContract = (from contract in _db.Contracts
                                                  where
                                                      (contract.UserId == WebSecurity.CurrentUserId)
                                                  select contract).FirstOrDefault();

            if (thisUsersContract == null)
                return RedirectToAction("WelcomeContractView", "ContractProposal");
            if (!thisUsersContract.Approved)
                return RedirectToAction("Details", "ContractProposal", new { id = thisUsersContract.ContractProposalId });
            if (thisUsersContract.Approved)
                return RedirectToAction("ContractOrProjectView", "ContractProposal",
                    new { id = thisUsersContract.ContractProposalId });
            return RedirectToAction("HttpNotFoundView", "ContractProposal");
        }

        public ActionResult WelcomeContractView()
        {
            return View();
        }

        public ActionResult ContractOrProjectView(int id = 0, string headerMessage = "")
        {
            ContractProposal contract = _db.Contracts.Find(id);
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
            ContractProposal contract = _db.Contracts.Find(id);
            if (contract == null)
                return HttpNotFound();

            // approve this contract
            contract.Approved = true;

            // create new Customer
            var newCustomer = new Customer();
            // get all properties for this Customer except ProjectId
            newCustomer.ExtractRelevantData(contract);
            _db.Customers.Add(newCustomer);
            _db.SaveChanges();

            // create new project
            var newProject = new Project();
            // get all properties for this project
            newProject.ExtractRelevantData(newCustomer);
            _db.Projects.Add(newProject);
            _db.SaveChanges();

            // get ProjectId for this Customer
            newCustomer.ExtractRelevantData(newProject);

            _db.SaveChanges();

            return View("Index", _db.Contracts.ToList());
        }
    }
}