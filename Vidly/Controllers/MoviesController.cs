using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        private MyContext _context;


        public MoviesController()
        {
            _context = new MyContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        


        // GET: Movies
        public ActionResult Random ()
        {

            var movie = new Movie();
           
            movie.Id = 1;
            movie.Name = "Shereck!";

            var customer = new List<Customer>
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"}
            };


            var viewModel = new RandomMovieViewModel ();
            viewModel.Movie = movie;
            viewModel.Customers = customer;


            // return Content("ID:"+ movie.Id +" "+ "Name: " + movie.Name);
            return View(viewModel);
        }


        //public ActionResult Edit (int id, string name)
        //{

        //    return Content("ID =  " + id );
        //}

        //This action will be called when navegate to //movie
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{

        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;

        //    if (String.IsNullOrEmpty(sortBy))
        //        sortBy = "name";

        //    //return Content("PageIndex: "+ pageIndex + " SortBy: " + sortBy);
        //    return Content(String.Format("pageIndex= {0}&sortBy= {1}", pageIndex, sortBy));
        //}

            public ActionResult Index ()
        {

            //var movies = GetMovies();
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        public ActionResult Details (int id)
        {
            //var movie = GetMovies().FirstOrDefault(m => m.Id == id);
            var movie = _context.Movies.Include(g =>g.Genre).SingleOrDefault(m => m.Id == id);
            return View(movie);
        }


        public ActionResult New()
        {
            var genre = _context.Genres.ToList();
            var movieObj = new Movie();
          
            var viewModel = new MovieFormViewModel
            {
                Genres = genre,
                Movie = movieObj
                

            };


            return View("MovieForm", viewModel);
        }



        public ActionResult Edit( int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                HttpNotFound();
            }
            
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()

                };
                        
            return View("MovieForm", viewModel);
        }



        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save (Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }



            if (movie.Id == 0)
            {

                movie.NumberAvailable = movie.NumberAvailable;
                movie.NumberAvailable = movie.NumberAvailable == 0 ? 100: movie.NumberAvailable;
                _context.Movies.Add(movie);
            }
            else
            {
                var recordInDB = _context.Movies.First(m => m.Id == movie.Id);
                recordInDB.Name = movie.Name;
                recordInDB.GenreId = movie.GenreId;
                recordInDB.ReleaseDate = movie.ReleaseDate;
                recordInDB.DateAdded = movie.DateAdded;
                recordInDB.NumberInStock = movie.NumberInStock;
                recordInDB.NumberAvailable = movie.NumberInStock;
                recordInDB.NumberAvailable = movie.NumberAvailable == 0 ? 100 : movie.NumberAvailable;
            }

            _context.SaveChanges();

            return RedirectToAction("Index","Movies");
        }



        [Route ("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]

        public ActionResult ByReleaseYear (int year, int month)
        {
            return Content("Year: " + year+ " month: "+ month);
        }
        


        private IEnumerable<Movie> GetMovies()
        {

            return new List<Movie>
            {
                new Movie {Id = 1, Name="Shrek"},
                new Movie {Id = 2, Name="Invisible Guest"}
            };
        }


    }
}