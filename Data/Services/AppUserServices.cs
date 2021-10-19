using eTicket_Demo.Data.Interfaces;
using eTicket_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.Data.Services
{
    public class AppUserServices : IAppUserServices
    {
        private readonly AppDbContext _dbContext;

        public AppUserServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<AppUser> GetAll() => _dbContext.Users;

        public AppUser GetById(string id) => _dbContext.Users.FirstOrDefault(u => u.Id == id);

        public IEnumerable<Order> GetOrders(string id) => _dbContext.Orders.Where(n => n.UserId == id);

    }
}
