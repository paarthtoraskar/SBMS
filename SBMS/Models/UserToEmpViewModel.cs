using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SBMS.Models
{
    public class UserToEmpViewModel
    {
        [Required]
        [Display(Name = "User")]
        public string UserId { get; set; }
        public List<UserProfile> UserProfilesNotInEmp { get; set; }
    }
}