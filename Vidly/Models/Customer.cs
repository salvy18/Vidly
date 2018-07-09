using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    [Table("Customers")]
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter custome's name.")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        [Display(Name ="Membership Type")]
        public MembershipType MembershipType { get; set; }
        //You can also implicity you can declared like this
        //[ForeignKey(nameof(MembershipType))]

        public byte MembershipTypeId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString= "{0:yyyy-MM-dd}", ApplyFormatInEditMode= true)]
        [Display(Name = "Date of Birth")]
        [Min18YearsifAMember]
        public DateTime? Birthday { get; set; }

    }
}