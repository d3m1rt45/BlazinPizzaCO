using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.Models
{
    public class PaymentDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Address cannot be empty.")]
        [StringLength(55, MinimumLength = 10)]
        [Display(Name = "First Line of Address")]
        public string AddressFirstLine { get; set; }

        [Display(Name = "Second Line of Address")]
        public string AddressSecondLine { get; set; }


        [Required(ErrorMessage = "Postcode cannot be empty.")]
        [RegularExpression(@"^(?i)([A-PR-UWYZ](([0-9](([0-9]|[A-HJKSTUW])?)?)|([A-HK-Y][0-9]([0-9]|[ABEHMNPRVWXY])?)) [0-9][ABD-HJLNP-UW-Z]{2})|GIR 0AA$",
            ErrorMessage = "Invalid Postcode")]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^((\(?0\d{4}\)?\s?\d{3}\s?\d{3})|(\(?0\d{3}\)?\s?\d{3}\s?\d{4})|(\(?0\d{2}\)?\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Account number cannot be empty.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Account number must be 9 characters long.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter a valid Account Number.")]
        [Display(Name = "Bank Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        [RegularExpression(@"\b([0-9]{2})-?([0-9]{2})-?([0-9]{2})\b", ErrorMessage = "Please enter a valid Sort Code.")]
        [Display(Name = "Sort Code")]
        public string SortCode { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Please enter a valid sort code.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter a valid security code. (3 digits at the back of your card.)")]
        [Display(Name = "Security Code")]
        public string SecurityCode { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}