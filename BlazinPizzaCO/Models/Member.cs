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


        //If a member with the given memberID found in the database, return it. Otherwise, create a new one and return that.
        public static Member FindOrCreate(BlazinContext db, string memberID)
        {
            var member = db.Members.Find(memberID);

            if (member == null)
            {
                member = new Member(memberID);
                db.Members.Add(member);
                db.SaveChanges();
                return member;
            }
            else
            {
                return member;
            }

        }
        public decimal GetPoints()
        {
            decimal totalPoints = 0;

            foreach(var order in Orders)
            {
                totalPoints += order.Points;
            }

            return totalPoints;
        }
    }
}