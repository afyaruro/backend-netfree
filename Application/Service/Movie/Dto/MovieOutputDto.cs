
using Application.Service.Actor.Dto;
using Application.Service.Country.Dto;
using Application.Service.Director.Dto;
using Application.Service.Gender.Dto;

namespace Application.Service.Movie.Dto
{
    public class MovieOutputDto
    {

        public string title { get; set; }
        public string review { get; set; }
        public string coverImage { get; set; }
        public string codeTrailer { get; set; }

        public DirectorOutputDto director { get; set; }
        public CountryOutputDto country { get; set; }
        public GenderOutputDto gender { get; set; }
        public List<ActorOutputDto> actors { get; set; }
        public int Id { get; set; }

    }
}