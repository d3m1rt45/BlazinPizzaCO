using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.Models
{
    public class Side
    {
        // Properties
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Relationship Field(s)
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<SidePerOrder> Sides { get; set; }
    }
}