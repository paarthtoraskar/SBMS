using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SBMS.Models
{
    public class UserToEmpViewModel
    {
        [Display(Name = "User")]
        [Required]
        public int UserId { get; set; }

        public IEnumerable<SelectListItem> UserProfilesNotInEmp { get; set; }
    }
}