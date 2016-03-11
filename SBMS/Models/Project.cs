using System.ComponentModel.DataAnnotations;

namespace SBMS.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Display(Name = "Project Title")]
        public string ProjectTitle { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public int ContractProposalId { get; set; }

        public int CustomerId { get; set; }

        public void ExtractRelevantData(Customer givenCustomer)
        {
            UserId = givenCustomer.UserId;
            Username = givenCustomer.Username;
            ContractProposalId = givenCustomer.ContractProposalId;
            CustomerId = givenCustomer.CustomerId;
            ProjectTitle = givenCustomer.ProjectTitle;
        }
    }
}