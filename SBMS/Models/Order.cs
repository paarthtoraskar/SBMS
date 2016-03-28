using System;
using System.ComponentModel.DataAnnotations;

namespace SBMS.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public string CartId { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        [Required]
        public string Email { get; set; }

        [RegularExpression(@"\d{3}-?\d{3}-?\d{4}",
            ErrorMessage = "Invalid {0}. Please enter 10 digits without spaces or hyphens.")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:###-###-####}")]
        [Required]
        public string Phone { get; set; }

        [Display(Name = "Card Holder Name")]
        [Required]
        public string CardHolderName { get; set; }

        public int CardTypeId { get; set; }
        public CardType CardTypeName { get; set; }

        [RegularExpression(@"\d{4}-?\d{4}-?\d{4}-?\d{4}",
            ErrorMessage = "Invalid {0}. Please enter 16 digits without spaces or hyphens.")]
        [DataType(DataType.CreditCard)]
        [DisplayFormat(DataFormatString = "{0:####-####-####-####}")]
        [Display(Name = "Card Number")]
        [Required]
        public string CardNumber { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{mm/dd/yy}")]
        [Display(Name = "Expiry Date")]
        [Required]
        public DateTime ExpiryDate { get; set; }

        [RegularExpression(@"\d{3}", ErrorMessage = "Invalid {0}. Please enter 3 digits.")]
        [Display(Name = "Security Code")]
        [Required]
        public int SecurityCode { get; set; }

        [Display(Name = "Total Order Payment")]
        public decimal TotalOrderPayment { get; set; }

        public enum CardType
        {
            AmericanExpress = 0,
            Discover = 1,
            MasterCard = 2,
            Visa = 3
        }
    }
}