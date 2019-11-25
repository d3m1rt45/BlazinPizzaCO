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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }

        [Required(ErrorMessage = "Address cannot be empty.")]
        [StringLength(55, MinimumLength = 10)]
        public string AddressFirstLine { get; set; }
        public string AddressSecondLine { get; set; }

        [Required(ErrorMessage = "Postcode cannot be empty.")]
        [RegularExpression(@"^(?i)([A-PR-UWYZ](([0-9](([0-9]|[A-HJKSTUW])?)?)|([A-HK-Y][0-9]([0-9]|[ABEHMNPRVWXY])?)) [0-9][ABD-HJLNP-UW-Z]{2})|GIR 0AA$",
            ErrorMessage = "Invalid Postcode")]
        public string PostCode { get; set; }

        [Required]
        [Range(10000000, 99999999)]
        public int AccountNumber { get; set; }

        [Required]
        [RegularExpression(@"\b([0-9]{2})-?([0-9]{2})-?([0-9]{2})\b")]
        public int SortCode { get; set; }

        [Required]
        [Range(100, 999)]
        public string SecurityCode { get; set; }

        [Required]
        public virtual Order Order { get; set; }
    }
}