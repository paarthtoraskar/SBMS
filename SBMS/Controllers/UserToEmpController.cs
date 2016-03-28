using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using System.Web.Security;
using SBMS.Models;

namespace SBMS.Controllers
{
    public class UserToEmpController : Controller
    {
        private readonly UsersContext _usersContext = new UsersContext();
        private readonly UserToEmpViewModel _userToEmpViewModel = new UserToEmpViewModel();

        public ActionResult UserToEmp(string result = "")
        {
            _userToEmpViewModel.UserProfilesNotInEmp = FindUserProfilesNotInEmp();
            ViewBag.Message = result;
            return View("UserToEmp", _userToEmpViewModel);
        }

        private SelectList FindUserProfilesNotInEmp()
        {
            var tempUserProfiles = new List<UserProfile>();
            foreach (UserProfile userProfile in _usersContext.UserProfiles)
            {
                if (!Roles.IsUserInRole(userProfile.Username, "emp") && !Roles.IsUserInRole(userProfile.Username, "admin"))
                    tempUserProfiles.Add(userProfile);
            }
            return new SelectList(tempUserProfiles, "UserId", "Username");
        }

        [HttpPost]
        public ActionResult GiveUserEmployeeRole(UserToEmpViewModel userToEmpViewModel)
        {
            if (ModelState.IsValid)
            {
                UserProfile userProfile = _usersContext.UserProfiles.Find(userToEmpViewModel.UserId);
                if (userProfile != null)
                {
                    Roles.AddUserToRole(userProfile.Username, "emp");
                    Roles.RemoveUserFromRole(userProfile.Username, "public");
                    return RedirectToAction("UserToEmp", "UserToEmp", new { result = "Success!" });
                }
                return RedirectToAction("UserToEmp", "UserToEmp", new { result = "Failure!" });
            }
            //return RedirectToAction("UserToEmp", "UserToEmp", new { result = "!ModelState.IsValid" });
            userToEmpViewModel.UserProfilesNotInEmp = FindUserProfilesNotInEmp();
            return View("UserToEmp", userToEmpViewModel);
        }
    }
}