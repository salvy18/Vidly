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
    public class RegisterMoviesController : ApiController
    {

        private MyContext _myContext;

        //Initialize my DB

        public RegisterMoviesController()
        {
            _myContext = new MyContext();
        }

        //POST Register the customer movies

        public IHttpActionResult RegisterMovies(CustomerDto customerDto, List<MovieDto> movieDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }




            return BadRequest();
        }



    }
}
