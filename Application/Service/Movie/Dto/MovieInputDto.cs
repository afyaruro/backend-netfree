

namespace Application.Service.Movie.Dto
{
    public class MovieInputDto
    {
        public string title { get; set; }
        public string review { get; set; }
        public string coverImage { get; set; }
        public string codeTrailer { get; set; }

        public int idGender { get; set; }
        public int idCountry { get; set; }
        public int idDirector { get; set; }
    }
}