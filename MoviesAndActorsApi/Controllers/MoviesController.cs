using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAndActorsApi.Data;
using MoviesAndActorsApi.Dto;
using MoviesAndActorsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAndMoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesContext context;

        public MoviesController(MoviesContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies()
        {
            return await context.Movies.Include(ma => ma.MovieActors).ThenInclude(a => a.Actor).Select(m => new MovieDto() { 
                Name = m.MovieName,
                Year = m.Year,
                Genre = m.Genre,
                Id = m.Id,
                Actors = m.MovieActors.Select(a => new ActorDto() { 
                    FirstName = a.Actor.FirstName,
                    LastName = a.Actor.LastName,
                    BirthDate = a.Actor.BirthDate,
                    Id = a.Actor.Id
                }).ToList()
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovie(int id)
        {
            return await context.Movies.Include(ma => ma.MovieActors).ThenInclude(a => a.Actor).Select(m => new MovieDto()
            {
                Name = m.MovieName,
                Year = m.Year,
                Genre = m.Genre,
                Id = m.Id,
                Actors = m.MovieActors.Select(a => new ActorDto()
                {
                    FirstName = a.Actor.FirstName,
                    LastName = a.Actor.LastName,
                    BirthDate = a.Actor.BirthDate,
                    Id = a.Actor.Id
                }).ToList()
            }).SingleOrDefaultAsync(movie => movie.Id == id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditMovie(int id, UpdateMovieDto updateMovie)
        {
            var movieToUpdate = await context.Movies.Include(ma => ma.MovieActors).SingleOrDefaultAsync(movie => movie.Id == id);
            movieToUpdate.MovieName = updateMovie.Name;
            movieToUpdate.Year = updateMovie.Year;
            movieToUpdate.Genre = updateMovie.Genre;
            
            foreach(var actor in movieToUpdate.MovieActors)
            {
                if(!updateMovie.ActorIds.Contains(actor.ActorId))
                {
                    context.Remove(actor);
                }
                
            }

            foreach(var actorId in updateMovie.ActorIds)
            {
                if(!movieToUpdate.MovieActors.Any(a => a.ActorId == actorId))
                {
                    movieToUpdate.MovieActors.Add(new MovieActor
                    {
                        ActorId = actorId,
                        MovieId = id
                    });
                }
            }            
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            Movie Movie = new Movie() { Id = id };
            context.Entry(Movie).State = EntityState.Deleted;
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateMovie(CreateMovieDto createMovie)
        {
            var movie = new Movie { 
                MovieName = createMovie.Name,
                Year = createMovie.Year,
                Genre = createMovie.Genre,
            };
            
            if (createMovie.ActorIds != null)
            {                
                movie.MovieActors = new List<MovieActor>();
                foreach (var actor in createMovie.ActorIds)
                {
                    var actorToAdd = new MovieActor { MovieId = movie.Id, ActorId = actor };
                    movie.MovieActors.Add(actorToAdd);
                }
            }
            context.Add(movie);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }
    }
}
