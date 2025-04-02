using Application.Service.MovieActor.Dto;
using Application.Service.Country;
using Domain.Entity.Response;
using Domain.Port;
using Application.Service.MovieActor.Commands;
using Application.Service.Movie;
using Application.Service.Actor;
using Application.Service.Actor.Dto;

namespace Application.Service.MovieActor
{
    public class MovieActorService
    {
        private readonly IMovieActorRepository _repository;
        public MovieActorService(IMovieActorRepository repository) => _repository = repository;

        public async Task<ResponseEntity<MovieActorOutputDto>> Create(MovieActorInputDto dto, CountryService countryService, MovieService movieService, ActorService actorService) => await CreatedMovieActorCommands.CreateMovieActor(_repository, countryService, movieService, actorService, dto);
        public async Task<bool> Delete(int id) => await DeleteMovieActorCommands.DeleteMovieActor(_repository, id);
        public async Task<List<ActorOutputDto>> GetByActorsByIdMovie(int id, CountryService countryService) => await GetMovieActorsCommands.GetAllByIdMovie(_repository, id, countryService);

    }
}