using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAndActorsApi.Data;
using MoviesAndActorsApi.Dto;
using MoviesAndActorsApi.Filters;
using MoviesAndActorsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAndActorsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class ActorsController : ControllerBase
    {
        private readonly MoviesContext context;

        public ActorsController(MoviesContext context)
        {
            this.context = context;
        }

        [HttpGet]        
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors()
        {
            return await context.Actors.Select(a => new ActorDto()
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                BirthDate = a.BirthDate,                
            }).ToListAsync();        
        }

        [HttpGet("{id}")]        
        public async Task<ActionResult<ActorDto>> GetActor(int id)
        {
            return await context.Actors.Select(a => new ActorDto()
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                BirthDate = a.BirthDate,
            }).FirstOrDefaultAsync(a => a.Id == id);
        }

        [HttpPut("{id}")]        
        public async Task<ActionResult> EditActor(int id, ActorDto actor)
        {
            Actor updateActor = new Actor()
            {
                Id = id,
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                BirthDate = actor.BirthDate,
                MovieActors = context.MovieActors.Where(ma => ma.ActorId == actor.Id).ToList()
            };            
            context.Entry(updateActor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]        
        public async Task<ActionResult> DeleteActor(int id)
        {
            Actor actor = new Actor() { Id = id };
            context.Entry(actor).State = EntityState.Deleted;
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]        
        public async Task<ActionResult> CreateActor(ActorDto actor)
        {            
            if(actor.BirthDate.Year < 2002)
            {
                Actor newActor = new Actor() {                     
                    Id = actor.Id,
                    FirstName = actor.FirstName,
                    LastName = actor.LastName,
                    BirthDate = actor.BirthDate,
                    MovieActors = context.MovieActors.Where(ma => ma.ActorId == actor.Id).ToList()
                };
                context.Actors.Add(newActor);
                await context.SaveChangesAsync();

                return CreatedAtAction("GetActor", new { id = actor.Id }, actor);
            }
            else
            {
                throw new Exception("Too young");
            }            
        }
    }
}
