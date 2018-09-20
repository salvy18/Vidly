using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {

        private MyContext _myContext;

        //Initialize my DB

        public NewRentalsController()
        {
            _myContext = new MyContext();
        }

        //POST Register the customer movies

        [HttpPost]
        public IHttpActionResult RegisterMovies(NewRentalDto newRentalDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //I need to extract the Customer ID from Dto

            if (newRentalDto.MovieIds.Count == 0)
            {
                return BadRequest("No Movie Ids have been given");
            }

            var customer = _myContext.Customers.SingleOrDefault(c => c.Id == newRentalDto.CustomerID);
            
            if (customer == null)
            {
                return BadRequest("Customer is not valid");
            }

                                    
            //Now that we have our customer id we need to extract the movies ids from the list but only the one that exist in our movies
            var movies = _myContext.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id)).ToList();

            //Check if the movie IDs passsed from DTO all existed in DB

            if (movies.Count != newRentalDto.MovieIds.Count)
            {
                BadRequest("One or more MovieIds are invalid");
            }

                        
            //Spin thru all the movies
            foreach (var movie in movies)
            {

                if (movie.NumberAvailable == 0)
                {
                    return BadRequest("Movie is not available" + movie.Id.ToString().Trim());
                }

                //For each movie rented we decrement one
                movie.NumberAvailable--;


                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    //Customer = new Customer
                    //{
                    //    Id = 14
                    //},

                    //Movie = new Movie
                    //{
                    //   Id = 3

                    //},
                    DateRented = DateTime.Now
                };

                
                _myContext.Rentals.Add(rental);
                

            }

            _myContext.SaveChanges();

            return Ok();
                                   
        }



    }
}
