using SBMS.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace SBMS.Controllers
{
    public class UserToEmpController : Controller
    {
        private UsersContext usersContext = new UsersContext();

        private UserToEmpViewModel userToEmpViewModel = new UserToEmpViewModel();

        public void FindUserProfilesNotInEmp()
        {
            //userProfilesNotInEmp = (from userProfile in usersContext.UserProfiles where !Roles.IsUserInRole(userProfile.Username, "emp") select userProfile).ToList();

            userToEmpViewModel.userProfileToAddToEmp = new UserProfile();
            userToEmpViewModel.userProfilesNotInEmp = new List<UserProfile>();

            foreach (var userProfile in usersContext.UserProfiles)
            {
                if (!Roles.IsUserInRole(userProfile.Username, "emp") && !Roles.IsUserInRole(userProfile.Username, "admin"))
                    userToEmpViewModel.userProfilesNotInEmp.Add(userProfile);
            }
        }

        [HttpGet]
        public ActionResult ShowUserToEmp()
        {
            FindUserProfilesNotInEmp();

            return View("UserToEmp", userToEmpViewModel);
        }

        [HttpPost]
        public ActionResult GiveUserEmployeeRole(UserToEmpViewModel editedVM)
        {
            return View();
        }
    }
}