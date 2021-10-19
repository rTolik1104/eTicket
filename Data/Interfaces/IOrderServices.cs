using eTicket_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.Data.Interfaces
{
    public interface IOrderServices
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
        string GetUniqCode(string userId);
    }
}
