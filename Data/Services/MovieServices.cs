using eTicket_Demo.Data.Base;
using eTicket_Demo.Data.Interfaces;
using eTicket_Demo.Models;
using eTicket_Demo.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTicket_Demo.Data;

namespace eTicket_Demo.Data.Services
{
    public class MovieServices : EntityBaseRepository<Movie>, IMovieServices
    {
        private readonly AppDbContext _dbContext;

        public MovieServices(AppDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddNewMovieAsync(NewMovieVM newMovie)
        {
            var data = new Movie()
            {
                Name = newMovie.Name,
                Description = newMovie.Description,
                Price = newMovie.Price,
                ImgUrl = newMovie.ImageURL,
                CinemaId = newMovie.CinemaId,
                StartDate = newMovie.StartDate,
                EndDate = newMovie.EndDate,
                MovieCategory = newMovie.MovieCategory,
                ProducerId = newMovie.ProducerId
            };
            await _dbContext.Movies.AddAsync(data);
            await _dbContext.SaveChangesAsync();

            //Add Movie Actors
            foreach(var actorId in newMovie.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _dbContext.Actors_Movies.AddAsync(newActorMovie);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDeatails = await _dbContext.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);
            return movieDeatails;
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await _dbContext.Actors.OrderBy(a => a.FullName).ToListAsync(),
                Cinemas = await _dbContext.Cinemas.OrderBy(c => c.Name).ToListAsync(),
                Producers = await _dbContext.Producers.OrderBy(p => p.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM newMovie)
        {
            var movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == newMovie.Id);

            if (movie != null)
            {
                movie.Name = newMovie.Name;
                movie.Description = newMovie.Description;
                movie.Price = newMovie.Price;
                movie.ImgUrl = newMovie.ImageURL;
                movie.CinemaId = newMovie.CinemaId;
                movie.StartDate = newMovie.StartDate;
                movie.EndDate = newMovie.EndDate;
                movie.MovieCategory = newMovie.MovieCategory;
                movie.ProducerId = newMovie.ProducerId;
                await _dbContext.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorsDb = _dbContext.Actors_Movies.Where(am => am.MovieId == newMovie.Id).ToList();
            _dbContext.Actors_Movies.RemoveRange(existingActorsDb);
            await _dbContext.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in newMovie.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _dbContext.Actors_Movies.AddAsync(newActorMovie);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
