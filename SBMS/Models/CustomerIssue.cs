using System.ComponentModel.DataAnnotations;

namespace SBMS.Models
{
    public class CustomerIssue
    {
        [Key]
        public int CustomerIssueId { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        public string Issue { get; set; }
    }
}