using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class DateValidation : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var movie = (Movie)validationContext.ObjectInstance;

            if (movie.DateAdded.Year == 1) 
            {
                return new ValidationResult("Date must be entered.");
            }
            else
            {
                return ValidationResult.Success;
            }


        }


    }
}