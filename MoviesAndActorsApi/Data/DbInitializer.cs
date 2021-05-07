using MoviesAndActorsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAndActorsApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(MoviesContext context)
        {
            context.Database.EnsureCreated();
            if (context.Actors.Any())
            {
                return;
            }            

            List<Actor> actors = new List<Actor> {
                new Actor
                {
                    FirstName = "Davy",
                    LastName = "Van Roy",
                    BirthDate = new DateTime(1987, 06, 14)
                },
                new Actor
                {
                    FirstName = "Frederik",
                    LastName = "Prijck",
                    BirthDate = new DateTime(1988, 06, 17)
                },
                new Actor
                {
                    FirstName = "Tom",
                    LastName = "Cruise",
                    BirthDate = new DateTime(1962, 07, 02)
                },
            };

            List<Movie> movies = new List<Movie>
            {
                new Movie
                {
                    MovieName = "Top Gun",
                    Year = new DateTime(1986, 01, 01),
                    Genre = MovieGenre.Action,
                },
                new Movie
                {
                    MovieName = "Avatar",
                    Year = new DateTime(2009, 01, 01),
                    Genre = MovieGenre.Action,
                },
                new Movie
                {
                    MovieName = "The Notebook",
                    Year = new DateTime(2004, 09, 22),
                    Genre = MovieGenre.Romantic,
                }
            };
            
            actors.ForEach(actor => context.Actors.Add(actor));
            movies.ForEach(movie => context.Movies.Add(movie));
            context.SaveChanges();

            List<MovieActor> movieActors = new List<MovieActor>
            {
                new MovieActor
                {
                    MovieId = movies[0].Id,
                    ActorId = actors[0].Id,
                },
                new MovieActor
                {
                    MovieId = movies[0].Id,
                    ActorId = actors[2].Id,
                },
                new MovieActor
                {
                    MovieId = movies[1].Id,
                    ActorId = actors[0].Id,
                },
                new MovieActor
                {
                    MovieId = movies[1].Id,
                    ActorId = actors[1].Id,
                },
                new MovieActor
                {
                    MovieId = movies[2].Id,
                    ActorId = actors[0].Id,
                },
                new MovieActor
                {
                    MovieId = movies[1].Id,
                    ActorId = actors[1].Id,
                }
            };

            movieActors.ForEach(movieActor => context.MovieActors.Add(movieActor));
            context.SaveChanges();
        }
    }
}
