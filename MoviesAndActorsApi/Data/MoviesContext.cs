using Microsoft.EntityFrameworkCore;
using MoviesAndActorsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAndActorsApi.Data
{
    public class MoviesContext: DbContext
    {
        public DbSet<Actor> Actors { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieActor> MovieActors { get; set; }

        public MoviesContext(DbContextOptions<MoviesContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Actor>().ToTable("Actor");
            modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<MovieActor>().ToTable("MovieActor");
        }
    }
}
