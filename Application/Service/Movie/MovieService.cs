
using Application.Service.Movie.Commands;
using Application.Service.Movie.Dto;
using Application.Service.Country;
using Domain.Entity.Response;
using Domain.Port;
using Application.Service.Director;
using Application.Service.Gender;
using Application.Service.MovieActor;
using Application.Base.Dto;

namespace Application.Service.Movie
{
    public class MovieService
    {
        private readonly IMovieRepository _repository;
        public MovieService(IMovieRepository repository) => _repository = repository;

        public async Task<ResponseEntity<MovieOutputDto>> Create(MovieInputDto dto, CountryService countryService, DirectorService directorService, GenderService genderService) => await CreatedMovieCommands.CreateMovie(_repository, countryService, genderService, directorService, dto);
        public async Task<ResponseEntity<MovieOutputDto>> GetAll(CountryService countryService, DirectorService directorService, GenderService genderService, MovieActorService movieActorService, PaginationDto dto) => await GetMoviesCommands.GetAll(_repository, countryService, directorService, genderService, movieActorService, dto);
        public async Task<bool> Delete(int id) => await DeleteMovieCommands.DeleteMovie(_repository, id);
        public async Task<ResponseEntity<MovieOutputDto>> GetById(int id, CountryService service) => await GetByIdMovieCommands.GetById(_repository, service, id);
        public async Task<bool> Update(MovieUpdateDto dto, CountryService countryService, DirectorService directorService, GenderService genderService) => await UpdateMovieCommands.UpdateMovie(_repository, countryService, directorService, genderService, dto);


    }
}