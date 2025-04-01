
using Application.Common.Exceptions;
using Application.Service.Movie.Dto;
using Application.Service.Movie.Mapping;
using Application.Service.Country;
using Domain.Entity.Response;
using Domain.Port;
using Application.Service.Director;
using Application.Service.Gender;
using Application.Service.MovieActor;
using Application.Base.Dto;

namespace Application.Service.Movie.Commands
{
    public static class GetMoviesCommands
    {
        public static async Task<ResponseEntity<MovieOutputDto>> GetAll(IMovieRepository repository, CountryService countryService, DirectorService directorService, GenderService genderService, MovieActorService movieActorService, PaginationDto dto)
        {
            try
            {

                var resp = await repository.GetAll(dto.pageNumber, dto.pageSize);

                if (resp.listEntity!.Count == 0 || resp == null)
                {
                    throw new EntityNotFoundException("No se encontraron registros");
                }

                var outputResponse = new ResponseEntity<MovieOutputDto>(resp.message, await MappingMovie.ListEntityToListOutputDto(resp.listEntity, countryService, directorService, genderService, movieActorService));
                outputResponse.totalPages = resp.totalPages;
                outputResponse.totalRecords = resp.totalRecords;

                return outputResponse;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}