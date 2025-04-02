using Application.Common.Exceptions;
using Application.Service.Movie.Dto;
using Application.Service.Movie.Mapping;
using Application.Service.Country;
using Domain.Entity.Response;
using Domain.Port;
using Application.Service.Gender;
using Application.Service.Director;

namespace Application.Service.Movie.Commands
{
    public static class CreatedMovieCommands
    {
        public static async Task<ResponseEntity<MovieOutputDto>> CreateMovie(IMovieRepository repository, CountryService countryService, GenderService genderService, DirectorService directorService, MovieInputDto dto)
        {

            try
            {

                ValidateMovie.ValidateMovieInputDto(dto);
                var respCountry = await countryService.GetById(dto.idCountry);
                var respGender = await genderService.GetById(dto.idGender);
                var respDirector = await directorService.GetById(dto.idDirector, countryService);


                var resp = repository.Add(MappingMovie.InputDtoToEntity(dto));


                if (resp == null)
                {
                    throw new EntityNotFoundException("No se ha creado la pelicula");
                }

                MovieOutputDto outputDto = MappingMovie.EntityToOutputDto(await resp);
                outputDto.country = respCountry.entity!;
                outputDto.director = respDirector.entity!;
                outputDto.gender = respGender.entity!;
                outputDto.actors = [];

                return new ResponseEntity<MovieOutputDto>("Pelicula Creada", outputDto);

            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}