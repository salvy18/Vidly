using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }


        [Required]
        public int GenreId { get; set; }

       // [DateValidation]
        public DateTime ReleaseDate { get; set; }

        //[DateValidation]
        public DateTime DateAdded { get; set; }

        public int NumberInStock { get; set; }


    }
}