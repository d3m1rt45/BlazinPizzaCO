using BlazinPizzaCO.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.Models
{
    public class Member
    {
        public Member() { }

        public Member(string memberID) { ID = memberID; }

        [Key]
        [Required]
        public string ID { get; set; }
        public int Points { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public void UpdatePoints()
        {
            using (var db = new BlazinContext())
            {
                var member = db.Members.Find(this.ID);
                int total = 0;

                foreach(var o in Orders)
                {
                    total += Convert.ToInt32(o.GetTotal() / 50m);
                }

                this.Points = total;
            }
        }
    }
}