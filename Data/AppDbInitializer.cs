using eTicket_Demo.Data.Enums;
using eTicket_Demo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

                context.Database.EnsureCreated();

                //Cinema
                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {
                            Name = "Cinema 1",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            Description = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 2",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-2.jpeg",
                            Description = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 3",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-3.jpeg",
                            Description = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 4",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-4.jpeg",
                            Description = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 5",
                            Logo = "http://dotnethow.net/images/cinemas/cinema-5.jpeg",
                            Description = "This is the description of the first cinema"
                        },
                    });
                    context.SaveChanges();
                }

                //Actors
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            FullName = "Actor 1",
                            Bio = "This is the Bio of the first actor",
                            ProfileImgUrl = "http://dotnethow.net/images/actors/actor-1.jpeg"

                        },
                        new Actor()
                        {
                            FullName = "Actor 2",
                            Bio = "This is the Bio of the second actor",
                            ProfileImgUrl = "http://dotnethow.net/images/actors/actor-2.jpeg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 3",
                            Bio = "This is the Bio of the second actor",
                            ProfileImgUrl = "http://dotnethow.net/images/actors/actor-3.jpeg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 4",
                            Bio = "This is the Bio of the second actor",
                            ProfileImgUrl = "http://dotnethow.net/images/actors/actor-4.jpeg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 5",
                            Bio = "This is the Bio of the second actor",
                            ProfileImgUrl = "http://dotnethow.net/images/actors/actor-5.jpeg"
                        }
                    });
                    context.SaveChanges();
                }
                //Producers
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            FullName = "Producer 1",
                            Bio = "This is the Bio of the first actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-1.jpeg"

                        },
                        new Producer()
                        {
                            FullName = "Producer 2",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-2.jpeg"
                        },
                        new Producer()
                        {
                            FullName = "Producer 3",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-3.jpeg"
                        },
                        new Producer()
                        {
                            FullName = "Producer 4",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-4.jpeg"
                        },
                        new Producer()
                        {
                            FullName = "Producer 5",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-5.jpeg"
                        }
                    });
                    context.SaveChanges();
                }
                //Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Name = "Star Wars: The Bad Batch",
                            Description = "It is part of the Star Wars franchise, acting as both a sequel to, and spin-off from, the series Star Wars: The Clone Wars.",
                            Price = 39.50,
                            ImgUrl = "/images/starWars.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            CinemaId = 3,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Cartoon
                        },
                        new Movie()
                        {
                            Name = "Venom: Let There Be Carnage",
                            Description = "Is a 2021 American superhero film featuring the Marvel Comics character Venom",
                            Price = 29.50,
                            ImgUrl = "/images/venom.jpg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(3),
                            CinemaId = 1,
                            ProducerId = 1,
                            MovieCategory = MovieCategory.Action
                        },
                        new Movie()
                        {
                            Name = "The Call of the Wild",
                            Description = "Is a 2020 American adventure film based on Jack London's 1903 novel of the same name. ",
                            Price = 39.50,
                            ImgUrl = "/images/zovPredkov.jpg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(7),
                            CinemaId = 4,
                            ProducerId = 4,
                            MovieCategory = MovieCategory.Action
                        },
                        new Movie()
                        {
                            Name = "Sputnik",
                            Description = "This is the Sputnik movie description",
                            Price = 39.50,
                            ImgUrl = "/images/sputnik.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-5),
                            CinemaId = 1,
                            ProducerId = 2,
                            MovieCategory = MovieCategory.Horror
                        },
                        new Movie()
                        {
                            Name = "Soul",
                            Description = "Soul is a 2020 American computer-animated film produced by Pixar Animation Studios and released by Walt Disney Pictures",
                            Price = 39.50,
                            ImgUrl = "/images/soul.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-2),
                            CinemaId = 1,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Cartoon
                        },
                        new Movie()
                        {
                            Name = "Cold Soles",
                            Description = "This is the Cold Soles movie description",
                            Price = 39.50,
                            ImgUrl = "http://dotnethow.net/images/movies/movie-8.jpeg",
                            StartDate = DateTime.Now.AddDays(3),
                            EndDate = DateTime.Now.AddDays(20),
                            CinemaId = 1,
                            ProducerId = 5,
                            MovieCategory = MovieCategory.Drama
                        }
                    });
                    context.SaveChanges();
                }
                //Actors & Movies
                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 7
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 7
                        },

                         new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 8
                        },
                         new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 8
                        },

                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 9
                        },
                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 9
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 9
                        },


                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 10
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 10
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 10
                        },


                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 11
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 11
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 11
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 11
                        },


                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 12
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 12
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 12
                        },
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
