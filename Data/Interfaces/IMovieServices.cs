using eTicket_Demo.Data.Base;
using eTicket_Demo.Models;
using eTicket_Demo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.Data.Interfaces
{
    public interface IMovieServices:IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(NewMovieVM newMovie);
        Task UpdateMovieAsync(NewMovieVM newMovie);
    }
}
