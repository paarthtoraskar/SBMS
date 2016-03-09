using System.Collections.Generic;

namespace SBMS.Models
{
    public class UserToEmpViewModel
    {
        public UserProfile userProfileToAddToEmp { get; set; }
        public List<UserProfile> userProfilesNotInEmp { get; set; }
    }
}