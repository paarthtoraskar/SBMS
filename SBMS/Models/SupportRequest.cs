using System.ComponentModel.DataAnnotations;

namespace SBMS.Models
{
    public class SupportRequest
    {
        [Key]
        public int SupportRequestId { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        [Required]
        public string Email { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Support query")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string Content { get; set; }
    }
}