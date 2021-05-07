using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAndActorsApi.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "FirstName required.")]
        public string FirstName { get; set; }

        [MaxLength(10, ErrorMessage = "Too long.")]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
