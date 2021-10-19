using eTicket_Demo.Data.Interfaces;
using eTicket_Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieServices _services;

        public MovieController(IMovieServices services)
        {
            _services = services;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await _services.GetAllAsync(n=>n.Cinema);
            return View(allMovies);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _services.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase) || n.Description.Contains(searchString, StringComparison.InvariantCultureIgnoreCase));
                return View("Index",filteredResult);
            }
            return View("Index",allMovies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _services.GetMovieByIdAsync(id);
            return View(movieDetails);
        }



        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _services.GetNewMovieDropdownsValues();

            ViewBag.Cinemas= new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers= new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors= new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM model)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _services.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(model);
            }

            await _services.AddNewMovieAsync(model);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _services.GetMovieByIdAsync(id);
            if (movieDetails == null) return View("Not Found");

            var respons = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImgUrl,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),
            };

            var movieDropdownsData = await _services.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View(respons);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM model)
        {
            if (id != model.Id) return View("Not Found");

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _services.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Cinemas, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Cinemas, "Id", "FullName");

                return View(model);
            }

            await _services.UpdateMovieAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
