using System.ComponentModel.DataAnnotations;

namespace SBMS.Models
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }

        public string CartId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}