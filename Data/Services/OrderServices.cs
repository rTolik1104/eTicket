using eTicket_Demo.Data.Interfaces;
using eTicket_Demo.Models;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.Data.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly AppDbContext _dbContext;

        public OrderServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId) => 
            await _dbContext.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(m => m.Movie)
            .Where(n => n.UserId == userId).ToListAsync();

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,
                UniqCode=Guid.NewGuid().ToString()
            };

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            foreach(var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    Price = item.Movie.Price,
                    OrderId = order.Id
                };

                await _dbContext.OrderItems.AddAsync(orderItem);
                await _dbContext.SaveChangesAsync();

                EmailServices emailServices = new();
                await emailServices.SendEmailAsync(userEmailAddress, "Tickets", order.UniqCode);
            }
        }

        public string GetUniqCode(string userId) => _dbContext.Orders.Where(x => x.UserId == userId).LastOrDefault().UniqCode;
    }
}
