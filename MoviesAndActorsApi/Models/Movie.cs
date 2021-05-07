using System;
using System.Collections.Generic;

namespace MoviesAndActorsApi.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string MovieName { get; set; }

        public DateTime Year { get; set; }

        public MovieGenre Genre { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
