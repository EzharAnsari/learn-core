using AutoMapper;
using Basic.DTOs;
using Basic.Entities;
using Microsoft.VisualBasic;
using NetTopologySuite.Geometries;

namespace Basic.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            // Mapping from GenreDTO -> Genre and Genre -> GenreDTO
            CreateMap<GenreDTO, Genre>().ReverseMap();
            CreateMap<GenreCreationDTO, Genre>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreationDTO, Actor>()
                .ForMember(x => x.Picture, options => options.Ignore());

            CreateMap<MovieTheater, MovieTheaterDTO>()
                .ForMember(x => x.Latitude, options => options.MapFrom(prop => prop.Location.Y))
                .ForMember(x => x.Longitude, options => options.MapFrom(prop => prop.Location.X));

            CreateMap<MovieTheaterCreationDTO, MovieTheater>()
                .ForMember(x => x.Location, x => x.MapFrom(dto =>
                    geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));

            CreateMap<MovieCreationDTO, Movie>()
                .ForMember( x => x.Poster, options => options.Ignore())
                .ForMember( x => x.MoviesGenres, options => options.MapFrom(MapMoviesGenres))
                .ForMember( x => x.MoviesActors, options => options.MapFrom(MapMoviesActors))
                .ForMember( x => x.MovieTheatersMovies, options => options.MapFrom(MapMovieTheatersMovies));
        }

        private List<MoviesGenres> MapMoviesGenres(MovieCreationDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MoviesGenres>();

            if(movieCreationDTO.GenresIds == null) { return result; }

            foreach (var id in movieCreationDTO.GenresIds)
            {
                result.Add(new MoviesGenres() {GenreId = id});
            }

            return result;
        }

        private List<MovieTheatersMovies> MapMovieTheatersMovies(MovieCreationDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MovieTheatersMovies>();

            if(movieCreationDTO.MovieTheatersIds == null) { return result; }

            foreach (var id in movieCreationDTO.MovieTheatersIds)
            {
                result.Add(new MovieTheatersMovies() {MovieTheaterId = id});
            }

            return result;
        }
        private List<MoviesActors> MapMoviesActors(MovieCreationDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MoviesActors>();

            if(movieCreationDTO.Actors == null) { return result; }

            foreach (var actor in movieCreationDTO.Actors)
            {
                result.Add(new MoviesActors() { ActorId = actor.Id, Character = actor.Character});
            }

            return result;
        }
    }
}