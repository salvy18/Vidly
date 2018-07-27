using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        public byte MembershipTypeId { get; set; }

        public MembershipDto MembershipType { get; set; }

        //following was commented because if you look at its implementation detail we are casting the validation to customer object
        //[Min18YearsifAMember]
        public DateTime? Birthday { get; set; }

        
    }
}