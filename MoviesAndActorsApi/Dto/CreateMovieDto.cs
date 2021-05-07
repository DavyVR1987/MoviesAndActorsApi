using MoviesAndActorsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAndActorsApi.Dto
{
    public class CreateMovieDto
    {
        public string Name { get; set; }

        public DateTime Year { get; set; }

        public MovieGenre Genre { get; set; }

        public List<int> ActorIds { get; set; }
    }
}
