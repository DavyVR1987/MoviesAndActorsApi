using MoviesAndActorsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAndActorsApi.Dto
{
    public class MovieDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Year { get; set; }

        public MovieGenre Genre { get; set; }

        public List<ActorDto> Actors { get; set; }
    }
}
