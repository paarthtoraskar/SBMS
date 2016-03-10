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

        public UserToEmpController()
        {
            _userToEmpViewModel.UserProfilesNotInEmp = new List<UserProfile>();
        }

        public ActionResult UserToEmp(string result = "")
        {
            FindUserProfilesNotInEmp();
            ViewBag.Message = result;
            return View("UserToEmp", _userToEmpViewModel);
        }

        private void FindUserProfilesNotInEmp()
        {
            //userProfilesNotInEmp = (from userProfile in usersContext.UserProfiles where !Roles.IsUserInRole(userProfile.Username, "emp") select userProfile).ToList();
            foreach (var userProfile in _usersContext.UserProfiles)
            {
                if (!Roles.IsUserInRole(userProfile.Username, "emp") && !Roles.IsUserInRole(userProfile.Username, "admin"))
                    _userToEmpViewModel.UserProfilesNotInEmp.Add(userProfile);
            }
        }

        [HttpPost]
        public ActionResult GiveUserEmployeeRole(int userId)
        {
            var userProfile = _usersContext.UserProfiles.Find(userId);
            if (userProfile != null)
            {
                Roles.AddUserToRole(userProfile.Username, "emp");
                Roles.RemoveUserFromRole(userProfile.Username, "public");
                return RedirectToAction("UserToEmp", "UserToEmp", new { result = "Success!" });
            }
            return RedirectToAction("UserToEmp", "UserToEmp", new { result = "Failure! Try again later!" });
        }
    }
}