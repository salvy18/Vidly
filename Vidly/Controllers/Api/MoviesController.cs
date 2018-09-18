using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {


        private MyContext _myContext;
        
        //Constructor
        public MoviesController()
        {
            _myContext = new MyContext();

        }



        //GET /api/movies

        public IHttpActionResult GetMovies(string query = null)
        {
            var movieQuery = _myContext.Movies.Include(m=> m.Genre).Where(m=> m.NumberAvailable > 0);


            if (!String.IsNullOrWhiteSpace(query))
            {
                movieQuery = movieQuery.Where(m => m.Name.Contains(query));
            }





            var moviesDTO = movieQuery.ToList();

            return Ok( moviesDTO.Select(Mapper.Map<Movie, MovieDto>));

        }



        //GET /api/movies/1
        public IHttpActionResult GetMovie( int id)
        {

            var movie = _myContext.Movies.SingleOrDefault(m => m.Id == id);

            if ( _myContext == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(movie));

        }

        
        //POST /api/movies (creating a movie)
        [HttpPost]
        public IHttpActionResult CreateMovie (MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _myContext.Movies.Add(movie);
            _myContext.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id),movieDto);

        }


        //PUT /api/movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie ( int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieInDb = _myContext.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(movieDto, movieInDb);
            _myContext.SaveChanges();

            //movieDto.Id = id;
            // return Created(new Uri(Request.RequestUri + "/" + id), movieDto);
            return Ok();

        }




    }
}
