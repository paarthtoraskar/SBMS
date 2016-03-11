using System.ComponentModel.DataAnnotations;

namespace SBMS.Models
{
    public class ProjectIssue
    {
        [Key]
        public int ProjectIssueId { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        [Required]
        public string Issue { get; set; }
    }
}