using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Vidly.Models
{

    [Table("Movies")]
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName ="varchar")]
        [MaxLength(50)]
        public string Name { get; set; }

      
        public Genre Genre { get; set; }

        [Required]
        [Display(Name ="Genre")]
        public int GenreId { get; set; }

        [DateValidation]
        [Display(Name ="Released date")]
        [Column("Released_Date")]
        public DateTime ReleaseDate { get; set; }

        [DateValidation]
        [Column("Date_Added")]
        [Display(Name = "Date added")]
        public DateTime DateAdded { get; set; }

        [Range(1,20,ErrorMessage = "The field Number in Stock must be between 1 - 20")]
        [Column("Number_In_Stock ")]
        [Display(Name = "Number in Stock")]
        public int NumberInStock { get; set; }

    }
}