using System.ComponentModel.DataAnnotations;

namespace SBMS.Models
{
    public class MeetingAndEvent
    {
        [Key]
        public int MeetingAndEventId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Note { get; set; }
    }
}