using eTicket_Demo.Data.Base;
using eTicket_Demo.Data.Interfaces;
using eTicket_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.Data.Services
{
    public class CinemaServices:EntityBaseRepository<Cinema>,ICinemaServices
    {
        public CinemaServices(AppDbContext dbContext):base(dbContext)
        {

        }
    }
}
