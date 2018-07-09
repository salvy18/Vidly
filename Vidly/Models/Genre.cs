using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidly.Models
{
    public class Genre
    {
        
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}