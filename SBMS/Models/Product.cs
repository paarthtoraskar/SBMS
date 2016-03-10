using System.ComponentModel.DataAnnotations;

namespace SBMS.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string Name { get; set; }

        public string Icon { get; set; }
        public string PunchLine { get; set; }
        public string Message { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}