using System.ComponentModel.DataAnnotations;

namespace Basic.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 75)]
        [Required]
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool InTheater { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
        public List<MoviesGenres> MoviesGenres { get; set; }  // many to many relationship with Movie and Genre model
        public List<MovieTheatersMovies> MovieTheatersMovies { get; set; }
    }
}