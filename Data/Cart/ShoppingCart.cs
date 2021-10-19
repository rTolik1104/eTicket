using eTicket_Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.Data.Cart
{
    public class ShoppingCart
    {
        private readonly AppDbContext _dbContext;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public static ShoppingCart GetShoppingCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddItemToCart(Movie movie)
        {
            _dbContext.Movies.Attach(movie);
            var shoppingCartItem = _dbContext.ShoppingCartItems.SingleOrDefault(n => n.Movie.Id==movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };
                _dbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _dbContext.SaveChanges();
        }


        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = _dbContext.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _dbContext.SaveChanges();
        }


        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _dbContext.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList());
        }

        public double GetShoppingCartTotal() => _dbContext.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();

        public async Task ClearShoppingCartAsync()
        {
            var items = await _dbContext.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _dbContext.ShoppingCartItems.RemoveRange(items);
            await _dbContext.SaveChangesAsync();
        }
    }
}
