
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
                var countryResp = await countryService.GetByIdCountryName(entity.idCountry);
                outputDto.country = countryResp.entity!;
                var directorResp = await directorService.GetByIdDirectorName(entity.idDirector, countryService);
                outputDto.director = directorResp.entity!;
                var genderResp = await genderService.GetByIdGenderName(entity.idGenero);
                outputDto.gender = genderResp.entity!;
                var actors = await movieActorService.GetByActorsByIdMovie(entity.Id, countryService);
                outputDto.actors = actors!;
                outputList.Add(outputDto);
            }

            return outputList;
        }
    }
}