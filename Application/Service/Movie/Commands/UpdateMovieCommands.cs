
using Application.Common.Exceptions;
using Application.Service.Movie.Dto;
using Application.Service.Movie.Mapping;
using Application.Service.Country;
using Domain.Entity.Movie;
using Domain.Port;
using Application.Service.Director;
using Application.Service.Gender;

namespace Application.Service.Movie.Commands
{
    public static class UpdateMovieCommands
    {
        public static async Task<bool> UpdateMovie(IMovieRepository repository, CountryService countryService, DirectorService directorService, GenderService genderService, MovieUpdateDto dto)
        {
            try
            {


                ValidateMovie.ValidateMovieUpdateDto(dto);
                await countryService.GetById(dto.idCountry);
                await genderService.GetById(dto.idGender);
                await directorService.GetById(dto.idDirector, countryService);

                MovieEntity entity = MappingMovie.UpdateDtoToEntity(dto);

                var resp = await repository.Update(entity);

                if (!resp)
                {
                    throw new EntityNotUpdateException("No se ha actualizado la pelicula");
                }

                return resp;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}