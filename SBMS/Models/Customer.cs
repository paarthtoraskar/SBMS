using System.ComponentModel.DataAnnotations;

namespace SBMS.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public int UserId { get; set; }
        public string Username { get; set; }
        public int ContractProposalId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }

        public void ExtractRelevantData(ContractProposal givenContract)
        {
            UserId = givenContract.UserId;
            Username = givenContract.Username;
            ContractProposalId = givenContract.ContractProposalId;
            ProjectTitle = givenContract.Title;
        }

        public void ExtractRelevantData(Project givenProject)
        {
            ProjectId = givenProject.ProjectId;
        }
    }
}