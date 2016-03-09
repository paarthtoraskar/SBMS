using System;
using System.ComponentModel.DataAnnotations;

namespace SBMS.Models
{
    public class Milestone
    {
        [Key]
        public int MilestoneId { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Status Message")]
        [Required]
        public string StatusMessage { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Completion Date")]
        [Required]
        public DateTime? CompletionDate { get; set; }
    }
}