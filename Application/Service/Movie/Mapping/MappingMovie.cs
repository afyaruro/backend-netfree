
using Application.Service.Movie.Dto;
using Application.Service.Country;
using Domain.Entity.Movie;
using Application.Service.Director;
using Application.Service.Gender;
using Application.Service.MovieActor;

namespace Application.Service.Movie.Mapping
{
    public class MappingMovie
    {
        public static MovieEntity InputDtoToEntity(MovieInputDto dto)
        {
            MovieEntity entity = new MovieEntity();
            entity.updateDate = DateTime.Now;
            entity.title = dto.title;
            entity.coverImage = dto.coverImage;
            entity.review = dto.review;
            entity.codeTrailer = dto.codeTrailer;
            entity.idGenero = dto.idGender;
            entity.idDirector = dto.idDirector;
            entity.idCountry = dto.idCountry;
            return entity;
        }

        public static MovieEntity UpdateDtoToEntity(MovieUpdateDto dto)
        {
            MovieEntity entity = new MovieEntity();
            entity.updateDate = DateTime.Now;
            entity.title = dto.title;
            entity.review = dto.review;
            entity.coverImage = dto.coverImage;
            entity.codeTrailer = dto.codeTrailer;
            entity.idGenero = dto.idGender;
            entity.idDirector = dto.idDirector;
            entity.idCountry = dto.idCountry;
            entity.Id = dto.Id;
            return entity;
        }

        public static MovieOutputDto EntityToOutputDto(MovieEntity entity)
        {
            MovieOutputDto output = new MovieOutputDto();
            output.Id = entity.Id;
            output.title = entity.title;
            output.review = entity.review;
            output.codeTrailer = entity.codeTrailer;
            output.coverImage = entity.coverImage;


            return output;
        }

        public static async Task<List<MovieOutputDto>> ListEntityToListOutputDto(List<MovieEntity> entities, CountryService countryService, DirectorService directorService, GenderService genderService, MovieActorService movieActorService)
        {
            List<MovieOutputDto> outputList = new List<MovieOutputDto>();

            foreach (var entity in entities)
            {

                var outputDto = EntityToOutputDto(entity);
                outputDto.country = await countryService.GetByIdCountryName(entity.idCountry);
                outputDto.director = await directorService.GetByIdDirectorName(entity.idDirector);
                outputDto.gender = await genderService.GetByIdGenderName(entity.idGenero);
                outputDto.actors = await movieActorService.GetByActorsByIdMovie(entity.Id);
                outputList.Add(outputDto);
            }

            return outputList;
        }
    }
}