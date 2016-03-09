using System;
using System.ComponentModel.DataAnnotations;

namespace SBMS.Models
{
    public class ContractProposal
    {
        [Key]
        public int ContractProposalId { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public bool Approved { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Tentative Start Date")]
        [Required]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Tentative End Date")]
        [Required]
        public DateTime EndDate { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Budget ($)")]
        public decimal Budget { get; set; }

        public string Issues { get; set; }
    }
}