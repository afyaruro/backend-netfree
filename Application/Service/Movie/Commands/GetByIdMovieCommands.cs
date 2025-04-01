
using Application.Common.Exceptions;
using Application.Service.Movie.Dto;
using Application.Service.Movie.Mapping;
using Application.Service.Country;
using Domain.Entity.Response;
using Domain.Port;

namespace Application.Service.Movie.Commands
{
    public static class GetByIdMovieCommands
    {

        public static async Task<ResponseEntity<MovieOutputDto>> GetById(IMovieRepository repository, CountryService service, int idMovie)
        {
            try
            {

                var resp = await repository.GetById(idMovie);

                if (resp == null)
                {
                    throw new EntityNotFoundException("No se encontro el Movie");
                }

                var output = MappingMovie.EntityToOutputDto(resp);
                output.country = await service.GetByIdCountryName(resp.idCountry);

                return new ResponseEntity<MovieOutputDto>("Pelicula Obtenido", output);

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}