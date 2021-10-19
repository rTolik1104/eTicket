using eTicket_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.Data.Interfaces
{
    public interface IAppUserServices
    {
        AppUser GetById(string id);
        IEnumerable<AppUser> GetAll();
        IEnumerable<Order> GetOrders(string id);
    }
}
