
namespace Application.Service.Movie.Dto
{
    public class MovieOutputDto
    {

        public string title { get; set; }
        public string review { get; set; }
        public string coverImage { get; set; }
        public string codeTrailer { get; set; }

        public string director { get; set; }
        public string country { get; set; }
        public string gender { get; set; }
        public List<string> actors { get; set; }
        public int Id { get; set; }

    }
}