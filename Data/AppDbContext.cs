using eTicket_Demo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            builder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.MovieId);
            builder.Entity<Actor_Movie>().HasOne(a => a.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(a => a.ActorId);
            builder.Entity<Actor>(entity => {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<Cinema>(entity => {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<Producer>(entity => {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            base.OnModelCreating(builder);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }

        //Order
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
