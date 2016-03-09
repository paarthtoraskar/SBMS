using System.Collections.Generic;
using System.Linq;
using WebMatrix.WebData;

namespace SBMS.Models
{
    public class DetailsForCustomerViewModel
    {
        public Project Project { get; set; }
        public List<Milestone> Milestones { get; set; }

        public void PopulateFromDbContext(SBMSDbContext dbContext)
        {
            Project = (from thisUsersProject in dbContext.Projects where thisUsersProject.UserId == WebSecurity.CurrentUserId select thisUsersProject).FirstOrDefault();

            Milestones = (from thisProjectsMilestone in dbContext.Milestones where thisProjectsMilestone.ProjectId == Project.ProjectId select thisProjectsMilestone).ToList();
        }
    }
}